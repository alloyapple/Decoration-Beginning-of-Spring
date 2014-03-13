///<summary>
///<para name = "Module">UILabelChange</para>
///<para name = "Describe">Set the value of label.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-1-2</para>
///</summary>
using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;

public class UILabelChange : MonoBehaviour
{
    public UILabel label;

    public void SetValue(string value)
    {
        label.text = value;
    }
}
