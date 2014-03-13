///<summary>
///<para name = "Module">UIButtonTwoState</para>
///<para name = "Describe">Enable button click or disable button click</para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-2-18</para>
///</summary>

using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;

/// <summary>
/// enable button click or disable button click;
/// </summary>
public class UIButtonTwoState : MonoBehaviour {
    public UISprite Ico;
    public UIButtonScale Scale;

    public enum State {
        Unknown,
        Disable,
        Enable
    }

    public State InitialState = State.Disable;

    State currentState = State.Unknown;

    public State CurrentState {
        get {
            return currentState;
        }
        set {
            if (currentState == value) return;
            currentState = value;

            if (currentState == State.Disable) {
                SetDisableState();
            }
            else if (currentState == State.Enable) {
                SetEnableState();
            }

            RaiseCurrentStateChanged();
        }
    }

    void Start() {
        CurrentState = InitialState;
    }

    void SetDisableState() {
        Ico.alpha = 0.5f;
        Scale.enabled = false;
    }

    void SetEnableState() {
        Ico.alpha = 1.0f;
        Scale.enabled = true;
    }

    public event EventHandler CurrentStateChanged;

    void RaiseCurrentStateChanged() {
        var handle = CurrentStateChanged;
        if (handle != null) handle(this, null);
    }
}
