///<summary>
///<para name = "Module">DragToMove</para>
///<para name = "Describe">Words</para>
///<para name = "Author">YS</para>
///<para name = "Date"> 2014-2-17 </para>
///</summary>
using UnityEngine;
using System.Collections;
using DecorationSystem.Framework;
using DecorationSystem.Undo;

namespace DecorationSystem.Gestures
{

    /// <summary>
    /// This can move gameobject by mouse or touch.
    /// </summary>
    public class DragToMove : AbstractGesture
    {
        public DragToMove() : base()
        {

        }

        protected override void RegisterCore()
        {
            _gc.OnDragStartHandle += DragingStart;
            _gc.OnDragUpdateHandle += DragingUpdate;
            _gc.OnDragEndHandle += DragingEnd;
        }

        protected override void UnregisterCore()
        {
            _gc.OnDragStartHandle -= DragingStart;
            _gc.OnDragUpdateHandle -= DragingUpdate;
            _gc.OnDragEndHandle -= DragingEnd;
        }

        void DragingStart(DragGesture gesture)
        {
            if (!GlobalManager.FurnitureOperationSwitch)
                return;
            if (gesture.Selection != GlobalManager.Selection)
                return;

            GestureRunning = IsTargetNull() ? false : true;
            if (!GestureRunning)
                return;

            StartChange();
        }

        void DragingUpdate(DragGesture gesture)
        {
            if (!GestureRunning)
                return;

            Vector3 fingerPos3d, prevFingerPos3d;

            ///calculate distance between previous finger position and new position
            /// and position of finger is in or out of screen
            if (ProjectScreenPointOnDragPlane(TargetTransform.position, gesture.Fingers [0].PreviousPosition, out prevFingerPos3d) &&
                ProjectScreenPointOnDragPlane(TargetTransform.position, gesture.Fingers [0].Position, out fingerPos3d))
            {

                Vector3 move = fingerPos3d - prevFingerPos3d;

                TargetTransform.position += move;
            }
        }

        void DragingEnd(DragGesture gesture)
        {
            if (!GestureRunning)
                return;
            RecordChange();
        }

        protected override ICommand CreateCommand()
        {
            return new SetPositionCmd(GlobalManager.Selection.transform, TargetTransform.position);
        }

        /// <summary>
        /// Projects the screen point on drag plane.
        /// converts a screen-space position to a world-space position constrained to the current drag plane type
        /// returns false if it was unable to get a valid world-space position
        /// </summary>
        /// <param name="refPos">The ref pos.</param>
        /// <param name="screenPos">The screen pos.</param>
        /// <param name="worldPos">The world pos.</param>
        /// <returns></returns>
        bool ProjectScreenPointOnDragPlane(Vector3 refPos, Vector2 screenPos, out Vector3 worldPos)
        {
            worldPos = refPos;
            Plane plane = new Plane(TargetTransform.forward, TargetTransform.position);
            Ray ray = RaycastCamera.ScreenPointToRay(screenPos);
            float t = 0;
            if (!plane.Raycast(ray, out t))
                return false;
            worldPos = ray.GetPoint(t);
            return true;
        }

    }
}
