///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">YS</para>
///<para name = "Date">2013-12-29</para>
///</summary>

using UnityEngine;
using System;

using DecorationSystem;
using DecorationSystem.Gestures;
using DecorationSystem.UI;

public class GestureProvider : MonoBehaviourBase {

    public GameObject MessageTarget;     // default to this game object
    public GameObject Target;
    ScreenRaycaster screenRaycaster;
    GestureController gestureController;

    public DragRecognizer Drag;
    public SwipeRecognizer Swipe;
    public TapRecognizer Tap;
    public TapRecognizer DoubleTap;

    public PinchRecognizer Pinch;
    public TwistRecognizer Twist;
    public DragRecognizer TwoFingerDrag;

    void Start() {
        if (!MessageTarget) MessageTarget = this.gameObject;
        if (!Target) Target = this.gameObject;

        screenRaycaster = gameObject.GetComponent<ScreenRaycaster>();
        if (!screenRaycaster) screenRaycaster = gameObject.AddComponent<ScreenRaycaster>();

        Init();
    }

    T AddSingleFingerGesture<T>( GameObject node ) where T : GestureRecognizer {
        T gesture = AddGesture<T>( node );
        gesture.RequiredFingerCount = 1;
        return gesture;
    }

    T AddTwoFingerGesture<T>( GameObject node ) where T : GestureRecognizer {
        T gesture = AddGesture<T>( node );
        gesture.RequiredFingerCount = 2;
        return gesture;
    }

    T AddTwoFingerGesture<T>(GameObject node, string messageName) where T : GestureRecognizer {
        T gesture = AddGesture<T>(node);
        gesture.RequiredFingerCount = 2;
        gesture.EventMessageName = messageName;
        return gesture;
    }

    T AddGesture<T>(GameObject node) where T : GestureRecognizer {
        T gesture = node.AddComponent<T>();
        gesture.Raycaster = screenRaycaster;
        gesture.EventMessageTarget = MessageTarget;
        gesture.UseSendMessage = false;
        return gesture;
    }

    TapRecognizer AddDoubleTapGesture(GameObject node) {
        TapRecognizer gesture = node.AddComponent<TapRecognizer>();
        gesture.RequiredTaps = 2;
        gesture.MaxDelayBetweenTaps = 0.5f;
        gesture.MoveTolerance = 25;
        return gesture;
    }

    void Init() {

        Tap = AddSingleFingerGesture<TapRecognizer>(Target);
        DoubleTap = AddDoubleTapGesture(Target);

        Drag = AddSingleFingerGesture<DragRecognizer>(Target);
        TwoFingerDrag = AddTwoFingerGesture<DragRecognizer>(Target,"OnTwoFingerDrag");

        Swipe = AddSingleFingerGesture<SwipeRecognizer>(Target);

        Pinch = AddTwoFingerGesture<PinchRecognizer>(Target);
        Twist = AddTwoFingerGesture<TwistRecognizer>(Target);

        Tap.OnGesture += OnTap;
        DoubleTap.OnGesture += OnDoubleTap;
        Drag.OnGesture += OnDrag;
        TwoFingerDrag.OnGesture += OnTwoFingerDrag;
        Swipe.OnGesture += OnSwipe;
        Pinch.OnGesture += OnPinch;
        Twist.OnGesture += OnTwist;

        gestureController = GestureController.Instance();

        // ignore NGUI when mouse on it;
        FingerGestures.GlobalTouchFilter = GUIController.FingerGestureGlobalFilter;
    }

    public void OnDrag(DragGesture gesture) {
        if (gesture.Phase == ContinuousGesturePhase.Started) {
            gestureController.OnDragStart(gesture);
        }
        else if (gesture.Phase == ContinuousGesturePhase.Updated) {
            gestureController.OnDragUpdate(gesture);
        }
        else if (gesture.Phase == ContinuousGesturePhase.Ended) {
            gestureController.OnDragEnd(gesture);
        }
    }

    public void OnPinch(PinchGesture gesture) {
        if (gesture.Phase == ContinuousGesturePhase.Started) {
            gestureController.OnPinchStart(gesture);
        }
        else if (gesture.Phase == ContinuousGesturePhase.Updated) {
            gestureController.OnPinchUpdate(gesture);
        }
        else if (gesture.Phase == ContinuousGesturePhase.Ended) {
            gestureController.OnPinchEnd(gesture);
        }
    }

    public void OnTwist(TwistGesture gesture) {
        if (gesture.Phase == ContinuousGesturePhase.Started) {
            gestureController.OnTwistStart(gesture);
        }
        else if (gesture.Phase == ContinuousGesturePhase.Updated) {
            gestureController.OnTwistUpdate(gesture);
        }
        else if (gesture.Phase == ContinuousGesturePhase.Ended) {
            gestureController.OnTwistEnd(gesture);
        }
    }

    public void OnTwoFingerDrag(DragGesture gesture) {
        if (gesture.Phase == ContinuousGesturePhase.Started) {
            gestureController.OnTwoFingerDragStart(gesture);
        }
        else if (gesture.Phase == ContinuousGesturePhase.Updated) {
            gestureController.OnTwoFingerDragUpdate(gesture);
        }
        else if (gesture.Phase == ContinuousGesturePhase.Ended) {
            gestureController.OnTwoFingerDragEnd(gesture);
        }
    }

    public void OnSwipe(SwipeGesture gesture) {
        gestureController.OnSwipeStart(gesture);
    }

    public void OnTap(TapGesture gesture) {
        gestureController.OnTapStart(gesture);
    }

    public void OnDoubleTap(TapGesture gesture) {
        gestureController.OnDoubleTap(gesture);
    }
}
