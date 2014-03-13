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
using DecorationSystem.Undo;

namespace DecorationSystem.Gestures {

    public class TwistToRotate : AbstractGesture{

        public TwistToRotate(Vector3 axis,float sensitivity)
            : base() {
                _rotationAxis = axis;
                _sensitivity = sensitivity;
        }

        public TwistToRotate()
            : this(Vector3.up,0.5f) {
        }

        protected override void RegisterCore() {
            _gc.OnTwistStartHandle += TwistStart;
            _gc.OnTwistUpdateHandle += TwistUpdate;
            _gc.OnTwistEndHandle += TwistEnd;
        }

        protected override void UnregisterCore() {
            _gc.OnTwistStartHandle -= TwistStart;
            _gc.OnTwistUpdateHandle -= TwistUpdate;
            _gc.OnTwistEndHandle -= TwistEnd;
        }

        float _sensitivity = 0.01f;
        Vector3 _rotationAxis = Vector3.zero;

        bool m_twisting;
        void TwistStart(TwistGesture gesture) {
            
            if (!GlobalManager.FurnitureOperationSwitch) return;
            if(gesture.Selection != GlobalManager.Selection) return;

            GestureRunning = IsTargetNull() ? false : true;
            if (!GestureRunning) return;
            StartChange();
        }

        // event message sent by FingerGestures
        void TwistUpdate(TwistGesture gesture) {
            if (!GestureRunning) return;
            // rotate around current rotation axis by amount proportional to rotation gesture's angle delta
            Quaternion qRot = Quaternion.AngleAxis(_sensitivity * gesture.DeltaRotation,_rotationAxis);

            // apply rotation to current object
            TargetTransform.rotation = qRot * TargetTransform.rotation;
        }

        void TwistEnd(TwistGesture gesture) {
            if (!GestureRunning) return;
            RecordChange();
        }

        protected override ICommand CreateCommand() {
            return new SetRotaionCmd(GlobalManager.Selection.transform, TargetTransform.rotation);
        }
    }
}
