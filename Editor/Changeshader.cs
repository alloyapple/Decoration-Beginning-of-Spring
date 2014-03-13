using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Text;

// Batch change the shader by Shader.Find.
public class ChangeShader : MonoBehaviour {

	[MenuItem("MyEdidor/ChangeShader")]
	
    static void changeshader() 
	{
		
		Object[] selection = Selection.GetFiltered(typeof(Material), SelectionMode.DeepAssets);
		Debug.Log (selection.Length);
		foreach(Material s in selection)
		{
//			if (s.shader == Shader.Find("Mobile/Diffuse") || s.shader == Shader.Find("Nature/Tree Soft Occlusion Leaves"))
//				s.shader = Shader.Find("Mobile/VertexLit");			
//			else if (s.shader == Shader.Find("Diffuse") && s.color == Color.white )
//				s.shader = Shader.Find("Mobile/VertexLit");	
//			else if (s.shader == Shader.Find("Diffuse") && s.mainTexture != null )
//				s.shader = Shader.Find("Mobile/VertexLit");	
//			else if (s.shader == Shader.Find("Transparent/Diffuse") || s.shader == Shader.Find("Transparent/Cutout/Soft Edge Unlit"))
//				s.shader = Shader.Find("Mobile/Transparent/Vertex Color");	
			
			if (s.shader == Shader.Find("Bumped Specular") )
				s.shader = Shader.Find("Mobile/Bumped Specular");
			else if (s.shader == Shader.Find("Bumped Diffuse"))
				s.shader = Shader.Find("Mobile/Bumped Diffuse");		
			Debug.Log ("#@");
//			else if (s.shader == Shader.Find("Transparent/Cutout/Soft Edge Unlit"))
//				s.shader = Shader.Find("Mobile/Transparent/Vertex Color");	
			
		}
	}	
}
