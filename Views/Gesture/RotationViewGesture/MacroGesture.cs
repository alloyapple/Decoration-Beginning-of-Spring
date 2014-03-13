///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">YS</para>
///<para name = "Date">20130902_1639</para>
///</summary>
using UnityEngine;
using System;
using System.Collections.Generic;
using DecorationSystem;
using DecorationSystem.CommonUtilities;

namespace DecorationSystem.Gestures
{
    public class MacroGesture : AbstractGesture
    {
        public MacroGesture() : base()
        {
        }

        List<IGesture> _gestures = new List<IGesture>();

        public void AddGesture(IGesture gesture)
        {
            _gestures.Add(gesture);
        }

        public void RemoveGesture(IGesture gesture)
        {
            _gestures.Remove(gesture);
        }

        protected override void RegisterCore()
        {
            _gestures.ForEach(item => item.Register());
        }

        protected override void UnregisterCore()
        {
            _gestures.ForEach(item => item.Unregister());
        }

        protected override Undo.ICommand CreateCommand()
        {
            throw new NotImplementedException();
        }
    }

}
