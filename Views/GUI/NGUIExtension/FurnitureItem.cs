///<summary>
///<para name = "Module">FurnitureItem</para>
///<para name = "Describe">When change currnetFurniture, invoke RaiseFurnitureChanged.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-2-19</para>
///</summary>

using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;

public class FurnitureItem : MonoBehaviour {
    /// <summary>
    /// When change currnetFurniture, invoke RaiseFurnitureChanged.
    /// </summary>
    static GameObject currnetFurniture = null;
    public static GameObject CurrentFurniture{
        get { return currnetFurniture; }
        set {
            if ( currnetFurniture == value) return;
            currnetFurniture = value;
            RaiseFurnitureChanged();
        }
    }

    public static event Action FurnitureChanged;

    static void RaiseFurnitureChanged() {
        Action handle = FurnitureChanged;
        if (handle != null)  handle(); 
    }

    /// <summary>
    /// This is game objects that use instance. 
    /// </summary>
    public GameObject Furniture;

    void OnClick() {
        CurrentFurniture = Furniture;
    }


}
