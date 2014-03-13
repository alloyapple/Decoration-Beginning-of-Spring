//author htoo
//history

//data 20130813_1921

using UnityEngine;
using UnityEditor;
using System.Collections;

public class RemoveNullAnimation : MonoBehaviour
{
    [MenuItem("Batch/Remove Null Animation")]
	
    static void removeNullAnimation()
    {
        Object[] comps = Selection.GetFiltered(typeof(Animation), SelectionMode.Deep);
		foreach (Animation obj in comps)
        {
			if(obj.clip == null)
            	DestroyImmediate(obj);
        }
    }

    [MenuItem("Batch/Remove Null Animation",true)]

    static bool ValidateSelection()
    {
        return Selection.transforms.Length != 0; 
    }
}
