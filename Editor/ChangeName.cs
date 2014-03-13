using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Text;

//Batch rename of the selection objects.	
public class ChangeName {

    [MenuItem("MyEdidor/Change Children GameObject Name")]
    static void ChangeNames() {
        Object[] selection = Selection.GetFiltered(typeof(Component), SelectionMode.DeepAssets);
        Component[] P;
        foreach (Component s in selection) {
            P = s.GetComponentsInChildren(typeof(Transform), true);

            foreach (Transform p in P) {
                if(p.name == "Sprite")
                    p.name = "Outer " + p.name;
            }
        }
    }
}
