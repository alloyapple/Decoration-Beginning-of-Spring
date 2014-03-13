///<summary>
///<para name = "Module">GestureController</para>
///<para name = "Describe">Words</para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-2-19</para>
///</summary>
using System;

namespace DecorationSystem.Gestures
{
    public class GestureController
    {
        /// <summary>
        /// The _gesture manager
        /// </summary>
        static GestureController _gestureManager;

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns></returns>
        public static GestureController Instance()
        {
            if (_gestureManager == null)
            {
                _gestureManager = new GestureController();
            }
            return _gestureManager;
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DecorationSystem.Gestures.GestureController"/> class.
        /// </summary>
        GestureController()
        {
        }

        /// <summary>
        /// Called when [drag start].
        /// </summary>
        /// <param name="gesture"> The gesture.</param>
        public void OnDragStart(DragGesture gesture)
        {
            /// NOTE: Make thread safe
            GestureHandler<DragGesture> handle = OnDragStartHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [drag update].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnDragUpdate(DragGesture gesture)
        {
            GestureHandler<DragGesture> handle = OnDragUpdateHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [drag end].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnDragEnd(DragGesture gesture)
        {
            GestureHandler<DragGesture> handle = OnDragEndHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [pinch start].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnPinchStart(PinchGesture gesture)
        {
            GestureHandler<PinchGesture> handle = OnPinchStartHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [pinch update].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnPinchUpdate(PinchGesture gesture)
        {
            GestureHandler<PinchGesture> handle = OnPinchUpdateHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [pinch end].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnPinchEnd(PinchGesture gesture)
        {
            GestureHandler<PinchGesture> handle = OnPinchEndHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [twist start].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnTwistStart(TwistGesture gesture)
        {
            GestureHandler<TwistGesture> handle = OnTwistStartHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [twist update].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnTwistUpdate(TwistGesture gesture)
        {
            GestureHandler<TwistGesture> handle = OnTwistUpdateHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [twist end].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnTwistEnd(TwistGesture gesture)
        {
            GestureHandler<TwistGesture> handle = OnTwistEndHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [tap start].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnTapStart(TapGesture gesture)
        {
            // save tap object;
            GestureHandler<TapGesture> handle = OnTapHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [double tap].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnDoubleTap(TapGesture gesture)
        {
            GestureHandler<TapGesture> handle = OnDoubleTapHandle;
            if (handle != null)
                handle(gesture);
        }
        /// <summary>
        /// Called when [swipe start].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnSwipeStart(SwipeGesture gesture)
        {
            GestureHandler<SwipeGesture> handle = OnSwipeHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [two finger drag start].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnTwoFingerDragStart(DragGesture gesture)
        {
            GestureHandler<DragGesture> handle = OnTwoFingerDragStartHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [two finger drag update].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnTwoFingerDragUpdate(DragGesture gesture)
        {
            GestureHandler<DragGesture> handle = OnTwoFingerDragUpdatetHandle;
            if (handle != null)
                handle(gesture);
        }

        /// <summary>
        /// Called when [two finger drag end].
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        public void OnTwoFingerDragEnd(DragGesture gesture)
        {
            GestureHandler<DragGesture> handle = OnTwoFingerDragEndHandle;
            if (handle != null)
                handle(gesture);
        }

        public delegate void GestureHandler<T>(T gesture) where T : Gesture;

        /// <summary>
        /// Occurs when on drag handle.
        /// </summary>
        public event GestureHandler<DragGesture> OnDragStartHandle;
        public event GestureHandler<DragGesture> OnDragUpdateHandle;
        public event GestureHandler<DragGesture> OnDragEndHandle;

        /// <summary>
        /// Occurs when on pinch handle.
        /// </summary>
        public event GestureHandler<PinchGesture> OnPinchStartHandle;
        public event GestureHandler<PinchGesture> OnPinchUpdateHandle;
        public event GestureHandler<PinchGesture> OnPinchEndHandle;

        /// <summary>
        /// Occurs when on twist handle.
        /// </summary>
        public event GestureHandler<TwistGesture> OnTwistStartHandle;
        public event GestureHandler<TwistGesture> OnTwistUpdateHandle;
        public event GestureHandler<TwistGesture> OnTwistEndHandle;

        /// <summary>
        /// Occurs when on tap handle.
        /// </summary>
        public event GestureHandler<TapGesture> OnTapHandle;
        public event GestureHandler<TapGesture> OnDoubleTapHandle;

        /// <summary>
        /// Occurs when on swipe handle.
        /// </summary>
        public event GestureHandler<SwipeGesture> OnSwipeHandle;

        /// <summary>
        /// Occurs when on two finger drag handle.
        /// </summary>
        public event GestureHandler<DragGesture> OnTwoFingerDragStartHandle;
        public event GestureHandler<DragGesture> OnTwoFingerDragUpdatetHandle;
        public event GestureHandler<DragGesture> OnTwoFingerDragEndHandle;
    }
}
