using UnityEngine;
using System.Collections;

namespace DecorationSystem.Gestures {

    public class TwoDragToMove : AbstractGesture {
        public TwoDragToMove()
            : base() {

        }

        protected override void RegisterCore() {
            throw new System.NotImplementedException();
        }

        protected override void UnregisterCore() { 
            throw new System.NotImplementedException();
        }

        protected override Undo.ICommand CreateCommand() {
            throw new System.NotImplementedException();
        }
    }
}