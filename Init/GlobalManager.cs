///<summary>
///<para name = "Module">GlobalManager</para>
///<para name = "Describe">All global variables must be list here.</para>
///<para name = "Author">YS</para>
///<para name = "Date"> 2014-1-1</para>
///</summary>
using System;
using System.Collections.Generic;

using UnityEngine;
using DecorationSystem.CommonUtilities;

namespace DecorationSystem.Framework
{
    /// <summary>
    /// All global variables must be list here.
    /// </summary>
    static public class GlobalManager
    {
        # region function switch

        // furniture tap, move, drag, twist, pinch.
        public static bool FurnitureOperationSwitch = false;
        public static bool MeasureFunctionSwitch = false;
        public static bool DigHoleFunctionSwitch = false;
        public static bool AddFurnitureFunctionSwitch = false;

        #endregion

        #region selection gameobject using tap gesture

        /// <summary>
        /// The _taping is true if selection is true, else false.
        /// </summary>
        static bool _taping = false;

        /// <summary>
        /// Gets or sets a value indicating whether this 
        /// <see cref="DecorationSystem.Framework.GlobalManager"/> is taping.
        /// </summary>
        /// <value><c>true</c> if taping; otherwise, <c>false</c>.</value>
        static public bool Taping
        {
            get { return _taping; }
            set
            {
                if (value != _taping)
                {
                    _taping = value;
                    RaiseTapSelectionChanged();
                }
            }
        }

        static public event Action<GameObject, bool> TapSelectionChanged;

        static void RaiseTapSelectionChanged()
        {
            var handle = TapSelectionChanged;
            if (handle != null)
                handle(Selection, Taping);
        }

        /// <summary>
        /// The _selection is the gameobject to be used when mouse or
        /// touch is on gameobject in play mode.
        /// </summary>
        static GameObject _selection;

        /// <summary>
        /// Gets or sets the selection.
        /// </summary>
        /// <value>The selection.</value>
        static public GameObject Selection
        {
            get { return _selection; }
            set
            {
                // temp game object 
                if (TempGameObject != null)
                    DestroyTempGameObject(); 
                
                _selection = value;

                // create temp game object to use
                if (_selection)
                {
                    CreateTempGameObjectFrom(_selection);
                    SetActiveTempGameObjectToFalse();
                }

                // toggle taping
                if (_selection)
                {
                    Taping = true;
                } else
                {
                    Taping = false;
                }
            }
        }

        #endregion

        #region get current camera in different view mode
        /// <summary>
        /// Gets or sets the current camera.
        /// </summary>
        /// <value>The current camera.</value>
        static public Camera CurrentCamera { get; set; }

        #endregion

        #region root gameobject get

        static GameObject _root;

        static public GameObject GetRoot()
        {
            if (_root == null)
            {
                _root = GameObject.Find("root");
            }

            if (_root == null)
            {
                Debug.LogError("no find root gameobject.");
            }
            return _root;
        }
        #endregion

        #region sihoutteMaterial setting

        static Material _sihouetteMaterial;

        static public Material GetSihouetteMaterial()
        {
            if (_sihouetteMaterial == null)
            {
                _sihouetteMaterial = Resources.Load("SihouetteOutline") as Material;
            }

            return _sihouetteMaterial;
        }
        #endregion

        #region temp gameobject setting

        static public GameObject TempGameObject { get; private set; }

        static GameObject CopyFrom(GameObject go)
        {
            Transform parent = go.transform.parent;
            GameObject tempGo = GameObject.Instantiate(go) as GameObject;
            tempGo.transform.parent = parent;
            return tempGo;
        }

        static void DestoryGameObject(GameObject go)
        {
            GameObject.DestroyImmediate(go);
        }

        static public void CreateTempGameObjectFrom(GameObject go)
        {
            TempGameObject = CopyFrom(go);
        }

        static public void DestroyTempGameObject()
        {
            DestoryGameObject(TempGameObject);
            TempGameObject = null;
        }

        static void SetActiveTempGameObjectTo(bool state)
        {
            if (IsTempGameObjectNotNull())
                TempGameObject.SetActive(state);
        }

        static public void SetActiveTempGameObjectToTrue()
        {
            SetActiveTempGameObjectTo(true);
        }

        static public void SetActiveTempGameObjectToFalse()
        {
            SetActiveTempGameObjectTo(false);
        }

        static public void SetTempGameObjectToNull()
        {
            TempGameObject = null;
        }

        static public bool IsTempGameObjectNull()
        {
            return TempGameObject == null ? true : false;
        }

        static public bool IsTempGameObjectNotNull()
        {
            return TempGameObject != null ? true : false;
        }

        #endregion

        #region gameobjects which state changed

        static List<GameObject> s_changedGameObjects = new List<GameObject>();

        static public void MarkChanged(GameObject go)
        {
            if (s_changedGameObjects.Contains(go))
                return;
            s_changedGameObjects.Add(go);
        }

        static public void ClearChanged(GameObject go)
        {
            s_changedGameObjects.Remove(go);
        }

        static public List<GameObject> GetChangedGameObjects()
        {
            return s_changedGameObjects;
        }

        #endregion

        #region gameobjects which are created from others

        static List<GameObject> s_createdGameObjects = new List<GameObject>();

        static public void MarkCreated(GameObject go)
        {
            if (s_createdGameObjects.Contains(go))
                return;
            s_createdGameObjects.Add(go);
        }

        static public void ClearCreated(GameObject go)
        {
            s_createdGameObjects.Remove(go);
        }

        static public List<GameObject> GetCreatedGameObjects()
        {
            return s_createdGameObjects;
        }

        #endregion

        #region gameobjects which are destroyed

        static List<GameObject> s_destroyedGameObjects = new List<GameObject>();

        static public void MarkDestroyed(GameObject go)
        {
            s_destroyedGameObjects.Add(go);
        }

        static public void ClearDestroyed(GameObject go)
        {
            s_destroyedGameObjects.Remove(go);
        }

        static public List<GameObject> GetDestroyedGameObjects()
        {
            return s_destroyedGameObjects;
        }

        #endregion

        #region gameobjects which changed material

        static List<GameObject> s_materialGameObjects = new List<GameObject>();

        static public void MarkMaterial(GameObject go)
        {
            if (s_materialGameObjects.Contains(go))
                return;
            s_materialGameObjects.Add(go);
        }

        static public void ClearMaterial(GameObject go)
        {
            s_materialGameObjects.Remove(go);
        }

        static public List<GameObject> GetMaterailGameObjects()
        {
            return s_materialGameObjects;
        }

        #endregion

        /// <summary>
        /// clear game objects in all lists above;
        /// </summary>
        static public void ClearAllLists()
        {
            s_changedGameObjects.Clear();
            s_createdGameObjects.Clear();
            s_destroyedGameObjects.Clear();
            s_materialGameObjects.Clear();
        }
    }

}

