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
using DecorationSystem.CommonUtilities;

//This manager will ensure that the messenger's eventTable will be cleaned up upon loading of a new level.
public class MessengerHelper : MonoBehaviourBase {
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    //Clean up eventTable every time a new level loads.
    public void OnDisable() {
        Messenger.Cleanup();
    }

    static MessengerHelper s_singleton;

    public static MessengerHelper CreateMessenger() {
        if (s_singleton ==null) {
            GameObject go = new GameObject("MessengerHelper");
            s_singleton = go.AddComponent<MessengerHelper>();
        }
        return s_singleton;
    }
}
