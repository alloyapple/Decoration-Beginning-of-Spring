//author htoo
//history

//date 20130817_1621

using UnityEngine;
using System.Collections;
using UnityEditor;

public class PingObject : MonoBehaviour
{
    [MenuItem("Batch/Ping Object %w")]
    static void Ping()
    {
        if (!Selection.activeObject)
        {
            Debug.LogError("Select an object to ping");
            return;
        }
        EditorGUIUtility.PingObject(Selection.activeObject);
    }
}

