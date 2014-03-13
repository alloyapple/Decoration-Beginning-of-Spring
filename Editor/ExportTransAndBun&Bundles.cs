using System;
using System.IO;
using System.Text;
using System.Collections;

using UnityEngine;
using UnityEditor;
using Object=UnityEngine.Object;
using Component=UnityEngine.Component;

//Export the transform and AssetBundle.
public class ExportTransAndBun {

    [MenuItem("MyEdidor/ExportTAB")]
	
	
    static void ExportResource () {
        // Bring up save panel
        string path = EditorUtility.SaveFilePanel ("Save Resource", Application.dataPath + "/StreamingAssets/", "New Resource", "txt");
		// Bring up save panel2
        string path3d = EditorUtility.SaveFilePanel ("Save Resource", Application.dataPath + "/StreamingAssets/", "New Resource", "unity3d");
		
		//if the two path exist,
        if (path.Length != 0 && path3d.Length != 0) {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Component), SelectionMode.DeepAssets);	
			// P is the transform array of each selection.
			Component[] P;
			//transform.position and transform,rotation
			string x, y, z, u, v, w, t;
			
			// Read file by filestream with 4 parameter
			FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
	        fs.Close();
			
			//Write data to the file.
	        StreamWriter sw = new StreamWriter(path, true, Encoding.ASCII);
			
			foreach(Component s in selection)
			{

				P = s.GetComponentsInChildren(typeof(Transform), true);
				
				// Subobject's name of the selection
				string[] assetNames = new string[P.Length];
				// Explicit asset. The children of the selection
				Object[] selection2 = new Object[P.Length];
				
				//prepare for AssetBundle export
				for(int i = 0, j = 1; j < P.Length; i++,j++)
				{
					
					selection2[i] = P[j].gameObject;
					assetNames[i] = P[j].name;					
					
					
					Debug.Log(assetNames[i]);
					
				}
				
				for(int j = 0; j < P.Length; j++)
				{
					x = string.Format("{0,10}", P[j].transform.position.x.ToString("F4"));
					u = string.Format("{0,10}", P[j].transform.rotation.x.ToString("F4"));
					
					y = string.Format("{0,10}", P[j].transform.position.y.ToString("F4"));
					v = string.Format("{0,10}", P[j].transform.rotation.y.ToString("F4"));
					
					z = string.Format("{0,10}", P[j].transform.position.z.ToString("F4"));
					w = string.Format("{0,10}", P[j].transform.rotation.z.ToString("F4"));
					
					t = string.Format("{0,10}", P[j].transform.rotation.w.ToString("F4"));					
					
					//Write data to Transform.txt    
					sw.Write(P[j].name + "\t" + x + "\t" + y + "\t" + z + "\t" + u + "\t" + v + "\t" + w + "\t" + t + "\r\n");
				}
				
				//build AssetBundles
				BuildPipeline.BuildAssetBundleExplicitAssetNames(selection2, assetNames, path3d, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
			}
			sw.Close();
		
	        Selection.objects = selection;
        }
		
		
    }
	


}
