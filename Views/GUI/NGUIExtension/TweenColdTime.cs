///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date"> 20131028_1426 </para>
///</summary>

using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;

public class TweenColdTime : MonoBehaviourBase {
    public GameObject ColdTimeObject;
    public bool FinishedState = false;

    UISprite coldTimeIco;

    void Start() {
        coldTimeIco = ColdTimeObject.GetComponent<UISprite>();
    }

    public IEnumerator ColdTime() {
        while (coldTimeIco.fillAmount != 0) {
            coldTimeIco.fillAmount -= 0.01f;
            yield return null;
        }

        coldTimeIco.fillAmount = 1f;
        NGUITools.SetActive(ColdTimeObject, FinishedState);
    }
}
