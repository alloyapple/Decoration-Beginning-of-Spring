///<summary>
///<para name = "Module">TapToCreate</para>
///<para name = "Describe">TapToCreate class is to use create game object when tap on some surface of game object.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-2-19</para>
///</summary>
using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;
using DecorationSystem.Framework;

namespace DecorationSystem.Gestures
{

    /// <summary>
    /// TapToCreate class is used to create game object
    /// when tap on some surface of game object.
    /// </summary>
    public class TapToCreate : AbstractGesture
    {

        public TapToCreate() : base()
        {
        
        }

        protected override void RegisterCore()
        {
            _gc.OnTapHandle += Tap;
        }

        protected override void UnregisterCore()
        {
            _gc.OnTapHandle -= Tap;
        }

        protected override Undo.ICommand CreateCommand()
        {
            throw new NotImplementedException();
        }

        void Tap(TapGesture gesture)
        {
            if (!GlobalManager.AddFurnitureFunctionSwitch)
                return;

            var furniture = FurnitureItem.CurrentFurniture;
            if (!furniture)
                return;
            GameObject.Instantiate(furniture, gesture.Hit.point, furniture.transform.rotation);

        }
    }
}
