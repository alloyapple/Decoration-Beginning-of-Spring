using UnityEngine;
using UnityEditor;
using DecorationSystem;
using System.Reflection;
using System;
using DecorationSystem.CommonUtilities;

namespace DecorationSystem.Test {

    /// <summary>
    /// Testing the public fields that not allow null value before go into game play mode.
    /// </summary>
    public class TestNull : MonoBehaviour {
      
        [MenuItem("Test/Not assign value for public fields (for all GameObjects)")]
        static public void TestNotAllowNull() {
            GameObject[] gameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
            gameObjects.ForEach(go => {
                MonoBehaviourBase[] components = go.GetComponentsInChildren<MonoBehaviourBase>(true);
                components.ForEach(component => checker(component));

            });
        }

        [MenuItem("Test/Not assign value for public fields (for all GameObjects)", true)]
        public static bool Validate() {
            return Selection.transforms.Length == 0;
        }

        [MenuItem("Test/Not assign value for public fields (for selected GameObjects)")]
        static void YetAnotherFunction() {
            UnityEngine.Object[] components = Selection.GetFiltered(typeof(MonoBehaviourBase), SelectionMode.Deep);
            components.ForEach(component => checker((MonoBehaviourBase)component));
        }

        [MenuItem("Test/Not assign value for public fields (for selected GameObjects)", true)]
        static bool ValidateSelection() {
            return Selection.transforms.Length != 0;
        }

        public static Action<MonoBehaviourBase> checker = component => {
            FieldInfo[] types = component.GetType().GetFields();

            types.ForEach(type => {
                object[] fields = type.GetCustomAttributes(typeof(NotAllowNullAttribute), false);

                if (fields.Length > 0) {
                    object value = type.GetValue(component);

                    if (value.Equals(null)) {
                        EditorGUIUtility.PingObject(component);
                        Debug.LogError(component.ToString() + "\t(" + type.ToString() + ") [is NULL.]");
                    }
                }
            });
        };

    }
}