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

    public class PinchToScale : AbstractGesture{
        public PinchToScale()
            : base() {

        }

        protected override void RegisterCore() {
            _gc.OnPinchStartHandle += PinchStart;
            _gc.OnPinchUpdateHandle += PinchUpdate;
            _gc.OnPinchEndHandle += PinchEnd;
        }

        protected override void UnregisterCore() {
            _gc.OnPinchStartHandle -= PinchStart;
            _gc.OnPinchUpdateHandle -= PinchUpdate;
            _gc.OnPinchEndHandle -= PinchEnd;
        }

        public Vector3 scaleWeights = Vector3.one;
        public float minScaleAmount = 0.5f;
        public float maxScaleAmount = 1.5f;

        float idealScaleAmount = 1.0f;
        float scaleAmount = 1.0f;
        Vector3 baseScale = Vector3.one;

        public float ScaleAmount {
            get { return scaleAmount; }

            set {
                value = Mathf.Clamp(value, minScaleAmount, maxScaleAmount);

                if (value != scaleAmount) {
                    scaleAmount = value;

                    Vector3 s = scaleAmount * baseScale;
                    s.x *= scaleWeights.x;
                    s.y *= scaleWeights.y;
                    s.z *= scaleWeights.z;

                    TargetTransform.localScale = s;
                }
            }
        }

        public float IdealScaleAmount {
            get { return idealScaleAmount; }
            set { idealScaleAmount = Mathf.Clamp(value, minScaleAmount, maxScaleAmount); }
        }

        bool pinch;
        void PinchStart(PinchGesture gesture) {
            if (!GlobalManager.FurnitureOperationSwitch) return;
            if (gesture.Selection != GlobalManager.Selection) return;

            GestureRunning = IsTargetNull() ? false : true;
            if (!GestureRunning) return;

            StartChange();

            baseScale = TargetTransform.localScale;
            idealScaleAmount = scaleAmount = 1.0f;
        }

        void PinchUpdate(PinchGesture gesture) {
            if (!GestureRunning) return;

            IdealScaleAmount += 0.01f * gesture.Delta;
            ScaleAmount = idealScaleAmount;
        }

        void PinchEnd(PinchGesture gesture) {
            if (!GestureRunning) return;
            RecordChange();
        }

        protected override ICommand CreateCommand() {
            return new SetScaleCmd(GlobalManager.Selection.transform, TargetTransform.localScale);
        }
    }
}
