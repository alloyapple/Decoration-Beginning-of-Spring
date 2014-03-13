
// Builds an asset bundle from the selected objects in the project view.
// Once compiled go to "Menu" -> "Assets" and select one of the choices
// to build the Asset Bundle
using UnityEngine;
using UnityEditor;
public class ExportAssetBundles {
    [MenuItem("MyEdidor/Build AssetBundle From Selection - Track dependencies")]
    static void ExportResource () {
        // Bring up save panel
        string path = EditorUtility.SaveFilePanel ("Save Resource", Application.dataPath + "/StreamingAssets/", "New Resource", "unity3d");
        if (path.Length != 0) {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
            Selection.objects = selection;
        }
    }
    
	[MenuItem("MyEdidor/Build AssetBundle From Selection - No dependency tracking")]
    static void ExportResourceNoTrack () {
        // Bring up save panel
        string path = EditorUtility.SaveFilePanel ("Save Resource", "", "New Resource", "unity3d");
        if (path.Length != 0) {
            // Build the resource file from the active selection.
            BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
        }
    }
	

	[MenuItem("MyEdidor/Build AssetBundle From Selection - Explicit")]
	static void ExportResourceExplicit () {
		// Bring up save panel2
        string path3d = EditorUtility.SaveFilePanel ("Save Resource", Application.dataPath + "/StreamingAssets/", "New Resource", "unity3d");		

		if ( path3d.Length != 0) {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Component), SelectionMode.DeepAssets);	
			// P is the transform array of each selection.
			Component[] P;
			//transform.position and transform,rotation
		
			foreach(Component s in selection)
			{

				P = s.GetComponentsInChildren(typeof(Transform), true);
				
				// Subobject's name of the selection
				string[] assetNames = new string[P.Length];
				// Explicit asset. The children of the selection
				Object[] selection2 = new Object[P.Length];
				
				for(int i = 0, j = 1; j < P.Length; i++,j++)
				{
					
					selection2[i] = P[j].gameObject;
					assetNames[i] = P[j].name;			
					
					
					Debug.Log(assetNames[i]);
					
				}
				

				BuildPipeline.BuildAssetBundleExplicitAssetNames(selection2, assetNames, path3d, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
			}
		
	        Selection.objects = selection;
        }
		
		
    }
	

}
