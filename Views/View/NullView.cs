///<summary>
///<para name = "Module">NullView</para>
///<para name = "Describe">Words</para>
///<para name = "Author">YS</para>
///<para name = "Date">20130902_1639</para>
///</summary>
using System;

namespace DecorationSystem.View
{
    public class NullView : AbstractView
    {
        public NullView() : base()
        {
        }

        protected override void DisableCore()
        {
        }

        protected override void EnableCore()
        {
        }
    }
}
