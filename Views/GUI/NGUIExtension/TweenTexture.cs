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
using DecorationSystem.MaterialUtilities;

public class TweenTexture : MonoBehaviourBase{
    GameObject target;
    Texture newTexture;
    Texture oldTexture;
    Material material;

    public void Tween(){
        material = target.renderer.materials.Last();
        oldTexture = material.mainTexture;
        material.mainTexture = newTexture;
        Destroy(this);
    }

    public TweenTexture SetTween(Texture texture) {
        newTexture = texture;
        target = this.gameObject;
        return this;
    }

    public static TweenTexture Get(GameObject go) {
        var texture = go.GetComponent<TweenTexture>();
        if (texture == null) texture = go.AddComponent<TweenTexture>(); 
        return texture;
    }
}
