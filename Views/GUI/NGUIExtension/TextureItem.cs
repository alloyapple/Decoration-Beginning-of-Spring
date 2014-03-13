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
using DecorationSystem.Framework;

public class TextureItem : MonoBehaviourBase {

    /// <summary>
    /// When change currentTexture, invoke RaiseTextureChanged.
    /// </summary>
    static Texture currentTexture;
    public static Texture CurrentTexture {
        get { return currentTexture; }
        set {
            if (currentTexture == value) return;
            currentTexture = value;
            RaiseTextureChanged();
        }
    }

    public static event Action TextureChanged;

    static void RaiseTextureChanged(){
        Action handle = TextureChanged;
        if(handle != null){
            handle();
        }
    }

    /// <summary>
    /// This is script with texture.
    /// </summary>
    public Texture Owner;

    void OnClick() {
        CurrentTexture = Owner;
    }
}
