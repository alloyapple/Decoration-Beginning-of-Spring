///<summary>
///<para name = "Module">PlaneView</para>
///<para name = "Describe">Top-down observation mode.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-1-1</para>
///</summary>

namespace DecorationSystem.View
{
    public class PlaneView : AbstractView
    {
        public PlaneView() : base()
        {
        }

        protected override void EnableCore()
        {
            _viewModeSwitch.CurrentMode = ViewModeSwitch.ViewMode.Plane_View_Mode;
        }

        protected override void DisableCore()
        {
        }

    }

}
