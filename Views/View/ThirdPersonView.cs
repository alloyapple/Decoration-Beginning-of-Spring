///<summary>
///<para name = "Module">ThirdPersonView</para>
///<para name = "Describe">Control the view with a person inside your eye shot.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-1-1</para>
///</summary>

namespace DecorationSystem.View
{
    public class ThirdPersonView : AbstractView
    {
        public ThirdPersonView() : base()
        {
        }

        protected override void EnableCore()
        {
            _viewModeSwitch.CurrentMode = ViewModeSwitch.ViewMode.Third_Person_Mode;
        }

        protected override void DisableCore()
        {
        }
    }
}
