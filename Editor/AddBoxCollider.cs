//author htoo
//history

//data 20130813_1921

using UnityEngine;
using UnityEditor;
using System.Collections;

public class AddBoxCollider : MonoBehaviour {

    [MenuItem("Batch/Add Box Collider")]

    static void addRigidBody()
    {
        Transform[] objects = Selection.transforms;
        foreach (Transform tran in objects)
        {
            tran.gameObject.AddComponent<BoxCollider>();
        }
    }

    [MenuItem("Batch/Add Box Collider", true)]
    static bool ValidateSelection()
    {
		return Selection.activeTransform != null;
    }
}
