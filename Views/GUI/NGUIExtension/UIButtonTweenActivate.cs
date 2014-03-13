///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date">20130902_1639</para>
///</summary>

using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;

/// <summary>
/// This button have switch state between true and false when click on it.
/// If selfstate is true, invoke Activate function
/// else invoke Deactivate function
/// </summary>
public class UIButtonTweenActivate : MonoBehaviourBase {
    public UISprite ActivateIco;
    public bool SelfState = true; 

    public GameObject Target;
    public bool TargetInitailState = false;

    public void Activate() {
        ActivateIco.alpha = 1.0f;
        // turn state to false;
        SelfState = false;
    }

    public void Deactivate() {
        ActivateIco.alpha = 0.5f;
        // turn state to true
        SelfState = true;
    }

    void Start() {
        if (Target != null) NGUITools.SetActive(Target, TargetInitailState);

        if (SelfState) {
            Activate();
        }
        else {
            Deactivate();
        }
    }

    void OnClick() {
        TargetInitailState = !TargetInitailState;

        if (Target != null) {
            NGUITools.SetActive(Target, TargetInitailState);

            if (SelfState) {
                Activate();
            }
            else {
                Deactivate();
            }
        }
    }
}
