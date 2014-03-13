///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Free-Controllable observation mode. Like a bird.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2013-12-29</para>
///</summary>

namespace DecorationSystem.View {
    public class BirdView : AbstractView {
        public BirdView() : base() {
        }

        protected override void EnableCore() {
            _viewModeSwitch.CurrentMode = ViewModeSwitch.ViewMode.Bird_View_Mode;
        }

        protected override void DisableCore() {

        }

    }
}
