///<summary>
///<para name = "Module">Name</para>
///<para name = "Describe">Words</para>
///<para name = "Author">htoo</para>
///<para name = "Date">20130902_1639</para>
///</summary>

// last change: 20131010_0920 by htoo.
// last change: 20131010_1852 by htoo.
using UnityEngine;
using System;
using System.Collections.Generic;
using DecorationSystem;
using DecorationSystem.Framework;
using DecorationSystem.CommonUtilities;

namespace DecorationSystem.Save
{

    /// <summary>
    /// This class will handle the saving and loading of data.
    /// </summary>
    public class SaveGameManager
    {

        # region initialize
        /// <summary>
        /// The changed object file is saving changed objects.
        /// </summary>
        readonly public string changedObjectFile ;

        /// <summary>
        /// The created object file is saving changed objects.
        /// </summary>
        readonly public string createdObjectFile ;

        /// <summary>
        /// The destroyed object file is saving changed objects.
        /// </summary>
        readonly public string destroyedObjectFile ;

        /// <summary>
        /// The material object file is saving changed objects.
        /// </summary>
        readonly public string materialObjectFile ;

        public SaveGameManager() :
            this("changedObjectsFile.dat" , "createdObjectsFile.dat"
         , "destroyedObjectsFile.dat" , "materialObjectFile.dat")
        {
        }

        public SaveGameManager(string changedFile, string createdFile, string destroyedFile, string materialFile)
        {
            changedObjectFile = changedFile;
            createdObjectFile = createdFile;
            destroyedObjectFile = destroyedFile;
            materialObjectFile = materialFile;
        }

        #endregion

        #region save tags

        readonly string _tagName = "?tag=name";
        readonly string _tagTransform = "?tag=transform";
        readonly string _tagMaterial = "?tag=materials";
        readonly string _tagChangedCount = "?tag=changedObjectsCount";
        readonly string _tagCreatedCount = "?tag=createdObjectsCount";
        readonly string _tagDestroyedCount = "?tag=destroyedObjectsCount";
        readonly string _tagMaterialCount = "?tag=materialObjectsCount";

        #endregion

        #region save objects

        /// <summary>
        /// Saves the changed object.
        /// </summary>
        /// <param name="id">The id is the number of the object wo are saving.</param>
        /// <param name="go">The game ojbect.</param>
        /// <param name="file">The file.</param>
        void SaveChangedObject(int id, GameObject go, string file)
        {
            ES2.Save(go.name, file + _tagName + id);
            ES2.Save(go.transform, file + _tagTransform + id);
        }

        /// <summary>
        /// Saves the created object.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="go">The go.</param>
        /// <param name="file">The file.</param>
        void SaveCreatedObject(int id, GameObject go, string file)
        {
            ES2.Save(go.name, file + _tagName + id);
            ES2.Save(go.transform, file + _tagTransform + id);
        }

        /// <summary>
        /// Saves the destroyed object.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="go">The go.</param>
        /// <param name="file">The file.</param>
        void SaveDestroyedObject(int id, GameObject go, string file)
        {
            ES2.Save(go.name, file + _tagName + id);
        }

        /// <summary>
        /// Saves the material object.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="materialID">The material ID.</param>
        /// <param name="go">The go.</param>
        /// <param name="file">The file.</param>
        void SaveMaterialObject(int id, GameObject go, string file)
        {
            ES2.Save(go.name, file + _tagName + id);
            ES2.Save(go.renderer.material, file + _tagMaterial + id);
        }

        #endregion

        #region load ojbects

        /// <summary>
        /// Loads the changed objects.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="file">The file.</param>
        void LoadChangedObjects(int id, string file)
        {
            string gameObjectName = ES2.Load<string>(file + _tagName + id);
            GameObject go = FindGameObjectWithName(gameObjectName);
            Transform tfm = go.transform;
            ES2.Load<Transform>(file + _tagTransform + id, tfm);

            //NOTE: Must add changed object to changed object list.
            GlobalManager.MarkChanged(go);
        }

        /// <summary>
        /// Loads the created objects.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="file">The file.</param>
        void LoadCreatedObjects(int id, string file)
        {
            string gameObjectName = ES2.Load<string>(file + _tagName + id);
            GameObject go = CreateGameObjectWithName(gameObjectName);
            Transform tfm = go.transform;
            ES2.Load<Transform>(file + _tagTransform + id, tfm);

            //NOTE: Must add created object to created object list.
            GlobalManager.MarkCreated(go);
        }

        /// <summary>
        /// Loads the destroy objects.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="file">The file.</param>
        void LoadDestroyObjects(int id, string file)
        {
            string gameObjectName = ES2.Load<string>(file + _tagName + id);
            GameObject go = FindGameObjectWithName(gameObjectName);
            go.SetActive(false);

            //NOTE: Must add destroyed object to destroyed object list.
            GlobalManager.MarkDestroyed(go);
        }

        /// <summary>
        /// Loads the material objects.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="file">The file.</param>

        [Obsolete("this function have bug.")]
        void LoadMaterialObjects(int id, string file)
        {
            string gameObjectName = ES2.Load<string>(file + _tagName + id);
            GameObject go = FindGameObjectWithName(gameObjectName);
            Material material = ES2.Load<Material>(file + _tagMaterial + id);
            go.renderer.material = material;

            //NOTE: Must add material changed object to material object list.
            GlobalManager.MarkMaterial(go);
        }

        #endregion

        #region utilities

        /// <summary>
        /// This is where we save certain aspects of our interested game ojbects;
        /// </summary>
        public void SaveToFile()
        {
            GameObject[] changedObjects = GlobalManager.GetChangedGameObjects().ToArray();
            // Save how many changed objects we're saving so we know how many to load.
            ES2.Save(changedObjects.Length, changedObjectFile + _tagChangedCount);
            // Iterate over each changed object
            for (int i = 0; i < changedObjects.Length; i++)
            {
                SaveChangedObject(i, changedObjects [i], changedObjectFile);
            }

            GameObject[] createdObjects = GlobalManager.GetCreatedGameObjects().ToArray();
            // Save how many created objects we're saving so we know how many to load.
            ES2.Save(createdObjects.Length, createdObjectFile + _tagCreatedCount);
            // Iterate over each created object
            for (int i = 0; i < createdObjects.Length; i++)
            {
                SaveCreatedObject(i, createdObjects [i], createdObjectFile);
            }

            GameObject[] destroyedObjects = GlobalManager.GetDestroyedGameObjects().ToArray();
            // Save how many destroyed objects we're saving so we know how many to load.
            ES2.Save(destroyedObjects.Length, destroyedObjectFile + _tagDestroyedCount);
            // Iterate over each destroyed object
            for (int i = 0; i < destroyedObjects.Length; i++)
            {
                SaveDestroyedObject(i, destroyedObjects [i], destroyedObjectFile);
            }

            GameObject[] materialObjects = GlobalManager.GetMaterailGameObjects().ToArray();
            // Save how many material changed objects we're saving so we know how many to load.
            ES2.Save(materialObjects.Length, materialObjectFile + _tagMaterialCount);
            // Iterate over each material changed object
            for (int i = 0; i < materialObjects.Length; i++)
            {
                SaveMaterialObject(i, materialObjects [i], materialObjectFile);
            }
        }

        /// <summary>
        /// Loads all game objects from file.
        /// </summary>
        public void LoadFromFile()
        {

            // If there's changed objects data to load
            if (ES2.Exists(changedObjectFile))
            {
                // Get how many changed objects we need to load, and then try to load each.
                int changedObjectsCount = ES2.Load<int>(changedObjectFile + _tagChangedCount);
                for (int i = 0; i < changedObjectsCount; i++)
                {
                    LoadChangedObjects(i, changedObjectFile);
                }
            }

            //If there's created objects data to load
            if (ES2.Exists(createdObjectFile))
            {
                //Get how many created objects we need to load, and then try to load each.
                int createdObjectsCount = ES2.Load<int>(createdObjectFile + _tagCreatedCount);
                for (int i = 0; i < createdObjectsCount; i++)
                {
                    LoadCreatedObjects(i, createdObjectFile);
                }
            }

            //If there's destroyed objects data to load
            if (ES2.Exists(destroyedObjectFile))
            {
                //Get how many destroyed objects we need to load, and then try to load each.
                int destroyedObjectsCount = ES2.Load<int>(destroyedObjectFile + _tagDestroyedCount);
                for (int i = 0; i < destroyedObjectsCount; i++)
                {
                    LoadDestroyObjects(i, destroyedObjectFile);
                }
            }

            //If there's materail changed objects data to load
            if (ES2.Exists(materialObjectFile))
            {
                //Get how many material changed objects we need to load, and then try to load each.
                int materailObjectCount = ES2.Load<int>(materialObjectFile + _tagMaterialCount);
                for (int i = 0; i < materailObjectCount; i++)
                {
                    LoadMaterialObjects(i, materialObjectFile);
                }
            }
        }

        /// <summary>
        /// delete all files.
        /// </summary>
        public void ClearSaves()
        {
            if (ES2.Exists(changedObjectFile))
                ES2.Delete(changedObjectFile);
            if (ES2.Exists(createdObjectFile))
                ES2.Delete(createdObjectFile);
            if (ES2.Exists(destroyedObjectFile))
                ES2.Delete(destroyedObjectFile);
            if (ES2.Exists(materialObjectFile))
                ES2.Delete(materialObjectFile);
        }

        #endregion

        #region misc

        /// <summary>
        /// Finds a game object of a given name in the scene.
        /// </summary>
        /// <param name="gameObjectName">Name of the game object.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">cannot find gameobject  + gameObjectName</exception>
        GameObject FindGameObjectWithName(string gameObjectName)
        {
            GameObject go = GameObject.Find(gameObjectName) as GameObject;
            if (go == null)
                throw new System.Exception("cannot find gameobject " + gameObjectName);
            return go;
        }

        /// <summary>
        /// Creates a game object of a given name in the prefabs.
        /// </summary>
        /// <param name="gameObjectName">Name of the game object.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">cannot create gameobject + gameObjectName</exception>
        GameObject CreateGameObjectWithName(string gameObjectName)
        {
            // TODO: add prefab game objects in order to instance game objects in scene.
            GameObject go = new GameObject(gameObjectName);
            if (go = null)
                throw new System.Exception("cannot create gameobject" + gameObjectName);
            return go;
        }

        #endregion
    }
}
