///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date"> 20131028_1427</para>
///</summary>

using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;

public class UIButtonColdTime : MonoBehaviourBase{
    public TweenColdTime ColdTime;

    void OnClick() {
        Task task = new Task(ColdTime.ColdTime());
    }
}
