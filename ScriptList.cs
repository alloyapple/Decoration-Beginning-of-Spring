///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">YS</para>
///<para name = "Date">20130902_1639</para>
///</summary>

using UnityEngine;

/// <summary>
/// All start scripts are initialized here.
/// </summary>
static public class ScriptList {
    static string[] _scripts ={
                              "GameController",
                              "GestureProvider"
                              };

    static public void Init(GameObject owner) {
        foreach (string script in _scripts) {
            owner.AddComponent(script);
	        }
    }

}   
