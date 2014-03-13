///<summary>
///<para name = "Module">UIButtonThreeState</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date">20130902_1639</para>
///</summary>

using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;

/// <summary>
/// This button have three state: disable, deactivate, active.
/// </summary>
public class UIButtonThreeState : MonoBehaviourBase {
    public UISprite Ico;
    public UIButtonScale Scale;

    public State InitialState = State.Disable;

    State currnetState = State.Unknown;

    public State CurrentState {
        get {
            return currnetState;
        }
        set {
            if (currnetState == value) return; 

            currnetState = value;
            if (currnetState == State.Disable) {
                CanceledState();
            }
            else if (currnetState == State.Activate) {
                ActivatedState();
            }
            else if (currnetState == State.Deactivate) {
                DeactivatedState();
            }

            RaiseCurrentStateChanged(); 
        }
    }

    public event EventHandler CurrentStateChanged;

    void RaiseCurrentStateChanged() {
        EventHandler handle = CurrentStateChanged;
        if (handle != null) handle(this, null);
    }

    void Start() {
        CurrentState = InitialState;
    }

    public enum State {
        Unknown,
        Disable,
        Deactivate,
        Activate,
    }

    public void CanceledState() {
        Ico.alpha = 0.5f;
        Scale.enabled = false;
    }

    public void DeactivatedState() {
        Ico.alpha = 0.5f;
        Scale.enabled = true;
    }

    public void ActivatedState() {
        Ico.alpha = 1.0f;
        Scale.enabled = true;
    }

    public void OnClick() {
        if (CurrentState == State.Disable || CurrentState == State.Unknown) return;
        if (CurrentState == State.Activate) {
            CurrentState = State.Deactivate;
        }
        else if(CurrentState == State.Deactivate){
            CurrentState = State.Activate;
        }
    }
}
