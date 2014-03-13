///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date">20130902_1639</para>
///</summary>

using UnityEngine;
using System;
using System.Collections;

public class UIPopedObjects : MonoBehaviour{
    public string[] Keys;
    public GameObject[] Activates;
    public GameObject[] Deactiveates;


    public void Pop() {
        if (enabled) {
            for (int i = 0; i < Keys.Length; i++) {
                if (UIPopupList.current.value == Keys[i]) {
                    NGUITools.SetActive(Activates[i], true);
                    NGUITools.SetActive(Deactiveates[i], false);
                }
            }
        }
    }

    void Set(GameObject go, bool state) {
        if (go) NGUITools.SetActive(go, state);
    }
}
