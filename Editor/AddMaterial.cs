//author htoo
//history

//data 20130813_1921

using UnityEngine;
using UnityEditor;
using System.Collections;

public class AddMaterial: MonoBehaviour
{

    [MenuItem("Batch/Add One Material")]

    static void AddMaterials()
    {
        Transform[] objects = Selection.transforms;
        foreach (Transform tran in objects)
        {
            Material[] mas = tran.gameObject.renderer.sharedMaterials;
            Material[] main = new Material[mas.Length+1];

            for (int i = 0; i < mas.Length; ++i)
            {
                main[i + 1] = mas[i];
            }

            main[0] = null;

            tran.gameObject.renderer.sharedMaterials= main;
        }
    }

    [MenuItem("Batch/Add One Material", true)]
    static bool ValidateSelection()
    {
        return Selection.transforms.Length != 0;
    }
}
