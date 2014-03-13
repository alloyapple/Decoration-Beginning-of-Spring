//author htoo
//history

//data 20130813_1921

using UnityEngine;
using UnityEditor;
using System.Collections;

public class AddRigidBody : MonoBehaviour {
    [MenuItem("Batch/Add Rigidbody")]
    static void addRigidBody() {
        Transform[] objects = Selection.transforms;
        foreach (Transform tran in objects) {
            tran.gameObject.AddComponent<Rigidbody>();
        }
    }

    [MenuItem("Batch/Add Rigidbody", true)]
    static bool ValidateSelection() {
        return Selection.transforms.Length != 0;
    }
}
