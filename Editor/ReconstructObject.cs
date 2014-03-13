using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Text;

//Reconstruct the object, make it one level child object at most. One father, many children, no grandson.
public class ReconstructObject
{

    [MenuItem("MyEdidor/ReconstructObject")]

    static void Reconstruct()
    {
        Object[] selection = Selection.GetFiltered(typeof(Component), SelectionMode.DeepAssets);
        Component[] P;

        foreach (Component s in selection)
        {

            P = s.GetComponentsInChildren(typeof(Transform), true);
           
//            foreach (Transform p in P)
//            {
//                if (p.childCount == 0 && p.name != "label")
//                {
//                    if (p.name == "bg")
//                    {
//                        p.name = "inner sprite";
//                    }
//                    if (p.name == "Outer Sprite")
//                    {
//                        p.name = "outer sprite";
//                       
//                    }
//                    if (p.name == "ico")
//                    {
//                        p.name = "icofr";
//                        
//                    }
//                    p.parent = p.parent.parent;
//                    
//                }
//
//                if (p.name == "Label")
//                {
//                    p.name = "label";
//                }
//
//
//            }

            foreach (Transform p in P)
            {
                if (p.name == "ico")
                    p.name = "icofr";

            }


        }

    }
}

