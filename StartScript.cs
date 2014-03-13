///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">YS</para>
///<para name = "Date">20130902_1639</para>
///</summary>

using UnityEngine;
using DecorationSystem;

/// <summary>
/// This script must add on the "root" gameobject.
/// </summary>
public class StartScript : MonoBehaviour {
    void Awake() {
        ScriptList.Init(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}


