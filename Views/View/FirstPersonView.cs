///<summary>
///<para name = "Module">FirstPersonView</para>
///<para name = "Describe">First person view mode. Like shooting games.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-1-1</para>
///</summary>

namespace DecorationSystem.View {
    public class FirstPersonView : AbstractView {
        public FirstPersonView() : base() {
        }

        protected override void EnableCore() {
            _viewModeSwitch.CurrentMode = ViewModeSwitch.ViewMode.First_Person_Mode;
        }

        protected override void DisableCore() {
        }
    }
}