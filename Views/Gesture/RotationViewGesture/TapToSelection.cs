///<summary>
///<para name = "Module">TapToSelection</para>
///<para name = "Describe">Tap to select gameobject.</para>
///<para name = "Author">YS</para>
///<para name = "Date"> 2014-1-1 </para>
///</summary>
using UnityEngine;
using System;
using DecorationSystem;
using DecorationSystem.CommonUtilities;
using DecorationSystem.Framework;
using DecorationSystem.UI;

namespace DecorationSystem.Gestures
{
    public class TapToSelection : AbstractGesture
    {

        RotationViewGUI gui;

        public TapToSelection() : base()
        {
            gui = GameObjectUtilities.GetCommponent<RotationViewGUI>(GlobalManager.GetRoot());
        }

        protected override void RegisterCore()
        {
            _gc.OnTapHandle += Tap;
        }

        protected override void UnregisterCore()
        {
            _gc.OnTapHandle -= Tap;
        }
        
        void Tap(TapGesture gesture)
        {

            /// Open or close this function
            if (!GlobalManager.FurnitureOperationSwitch)
                return; 

            /// Global manager selection set
            GameObject go = gesture.Selection;

            if (!IsNull(GlobalManager.Selection))
                RemoveSihouette(GlobalManager.Selection);

            if (IsSameSelection(go))
            {
                go = null;
            } else
            {
                if (IsFurniture(go))
                {
                    AddSihouette(go);
                } else
                {
                    go = null;
                }
            }

            GlobalManager.Selection = go;
        }

        string _tag = "Furniture";

        bool IsFurniture(GameObject go)
        {
            return go.CompareTag(_tag) ? true : false;
        }

        public static void AddSihouette(GameObject go)
        {
            Material[] mas = go.renderer.materials;
            mas = GlobalManager.GetSihouetteMaterial().Adds(mas);
            go.renderer.materials = mas;
        }

        public static void RemoveSihouette(GameObject go)
        {
            Material[] mas = go.renderer.materials;
            mas = mas.Tail();
            go.renderer.materials = mas;
        }

        bool IsSameSelection(GameObject go)
        {
            return GlobalManager.Selection == go ? true : false;
        }

        bool IsNull(GameObject go)
        {
            return go == null ? true : false;
        }

        protected override Undo.ICommand CreateCommand()
        {
            throw new NotImplementedException();
        }
    }
}