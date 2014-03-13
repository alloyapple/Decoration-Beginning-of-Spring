///<summary>
///<para name = "Module">GUIController</para>
///<para name = "Describe">The controller of GUI.</para>
///<para name = "Author">YS</para>
///<para name = "Date">2014-1-1</para>
///</summary>
using UnityEngine;
using System;
using System.Collections;
using DecorationSystem;

namespace DecorationSystem.UI
{
    public class GUIController
    {               
        static GUIController _guiController;

        public static GUIController Instance()
        {
            if (_guiController == null)
            {
                _guiController = new GUIController();
            }
            return _guiController;
        }

        /// <summary>
        /// Fingersgesture's global filter.
        /// </summary>
        /// <param name="fingerIndex">Index of the finger.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static bool FingerGestureGlobalFilter(int fingerIndex, Vector2 position)
        {
            RaycastHit hit = new RaycastHit();
            return !UICamera.Raycast(new Vector3(position.x, position.y, 0), out hit);
        }

    }
}
