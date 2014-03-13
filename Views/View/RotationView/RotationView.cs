///<summary>
///<para name = "Module">RotationView</para>
///<para name = "Describe">Rotate the view by keyboard. 
/// All the operation are implemented in this mode now. </para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-1-1</para>
///</summary>
using DecorationSystem.Gestures;
using DecorationSystem.Framework;
using DecorationSystem;

namespace DecorationSystem.View
{
    /// <summary>
    /// Some initialization and funtions in the Rotation view.
    /// </summary>
    public class RotationView : AbstractView
    {
        MacroGesture macro = new MacroGesture();

        public RotationView() : base()
        {
            macro.AddGesture(new TapToSelection());
            macro.AddGesture(new DragToMove());
            macro.AddGesture(new TwistToRotate());
            macro.AddGesture(new PinchToScale());
            macro.AddGesture(TweenDistance.Instance());
            macro.AddGesture(new TapToStartDig());
            macro.AddGesture(new TapToCreate());
        }

        protected override void EnableCore()
        {
            _viewModeSwitch.CurrentMode = ViewModeSwitch.ViewMode.Rotation_View_Mode;
            macro.Register();

            // Enable furnitureOperationSwitch
            GlobalManager.FurnitureOperationSwitch = true;
        }

        protected override void DisableCore()
        {
            macro.Unregister();

            // Disable furnitureOperationSwitch
            GlobalManager.FurnitureOperationSwitch = false;

            // selection 
            if (GlobalManager.Selection)
                TapToSelection.RemoveSihouette(GlobalManager.Selection);
            GlobalManager.Selection = null;
        }
    }
}
