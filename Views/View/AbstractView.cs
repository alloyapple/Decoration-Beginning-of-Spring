///<summary>
///<para name = "Module">AbstractView</para>
///<para name = "Describe">The abstract character of all the view mode.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2013-12-29</para>
///</summary>
using DecorationSystem.Framework;

namespace DecorationSystem.View
{
    public abstract class AbstractView : IView
    {

        static protected ViewModeSwitch _viewModeSwitch;

        protected AbstractView()
        {
            if (_viewModeSwitch == null)
            {
                _viewModeSwitch = GlobalManager.GetRoot()
                    .GetSafeComponent<ViewModeSwitch>();
            }
        }

        public void Apply()
        {
            EnableCore();
        }

        public void Cancel()
        {
            DisableCore();
        }

        protected abstract void EnableCore();

        protected abstract void DisableCore(); 
    }

}