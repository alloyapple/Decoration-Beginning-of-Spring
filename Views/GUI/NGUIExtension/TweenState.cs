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

[RequireComponent(typeof(UIButtonThreeState))]
public class TweenState : MonoBehaviourBase {
    public UIButtonThreeState ThreeStateButton;
    public GameObject Target;
    public bool state = false;

    void Start() {
        ThreeStateButton.CurrentStateChanged += ToggleThreeStareButton;
    }

    void ToggleThreeStareButton(object sender, EventArgs e) {
        UIButtonThreeState ui = sender as UIButtonThreeState;
        if (ui.CurrentState == UIButtonThreeState.State.Activate) {
            NGUITools.SetActive(Target, state);
        }
        else if (ui.CurrentState == UIButtonThreeState.State.Deactivate) {
            NGUITools.SetActive(Target, !state);
        }
        else if (ui.CurrentState == UIButtonThreeState.State.Disable) {
            NGUITools.SetActive(Target, state);
        }
    }
}
