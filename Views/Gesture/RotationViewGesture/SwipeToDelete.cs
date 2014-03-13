///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date"> 20131009_1103 </para>
///</summary>
///
using UnityEngine;
using System.Collections;
using DecorationSystem.Framework;

namespace DecorationSystem.Gestures {
    public class SwipeToDelete : AbstractGesture {
        public SwipeToDelete()
            : base() {

        }

        protected override void RegisterCore() {
            _gc.OnSwipeHandle += Swipe;
        }

        protected override void UnregisterCore() {
            _gc.OnSwipeHandle -= Swipe;
        }

        void Swipe(SwipeGesture gesture) {
            if (!GlobalManager.FurnitureOperationSwitch) return;

            if (Target == null) return;
        }

        protected override Undo.ICommand CreateCommand() {
            throw new System.NotImplementedException();
        }
    }
}