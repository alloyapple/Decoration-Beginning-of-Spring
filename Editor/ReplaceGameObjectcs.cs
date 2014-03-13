using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System;

public class ReplaceGameObjectcs {

	[MenuItem("MyEdidor/Instantiate Selected cs")]
	
    static void CreatePrefabcs() 
	{
		int size, i;
		string[] line, words =  new string[11];
		string[] stringSeparators = new string[] {"\r\n"};	
		float[] x, y, z, u, v, w, t, a, b, c;
		
		
		
//		string url = "jar:file://" + Application.dataPath + "!/assets/" + "transform.txt";   //android
		
		string url = Application.dataPath + "/StreamingAssets/" + "transform";   //This two lines 
		url = "file://" + url.Replace("/", "\\") + ".txt";						//are combined together.
		
		
//		using (StreamReader sr = new StreamReader("D:\\sss\\New Unity Project\\New Resource.txt")) //PC
		
		WWW www = new WWW(url);
	      
//			yield return www;		//I think, Equals to if(!www.isdone) Return
		while(!www.isDone)
		{
			Debug.Log("1");
		}
		
			
			if (www.error != null)
				throw new Exception("WWW download had an error:" + www.error);
				
			line = www.text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
			
			size = line.Length;
			
//			Debug.Log(line[0]);
		
		
		Vector3[] viewPos = new Vector3[size];
		Vector3[] scale = new Vector3[size];
		Quaternion[] drot = new Quaternion[size];	
		GameObject[] clone = new GameObject[size];
		
		x = new float[size]; 
		y = new float[size]; 
		z = new float[size];
		u = new float[size]; 
		v = new float[size]; 
		w = new float[size]; 
		t = new float[size];
		a = new float[size];
		b = new float[size];
		c = new float[size];
		
		for(i = 0; i < size; i++)
		{
			words = line[i].Split('\t');
			x[i] = Convert.ToSingle(words[1]);
			y[i] = Convert.ToSingle(words[2]);
			z[i] = Convert.ToSingle(words[3]);
			u[i] = Convert.ToSingle(words[4]);
			v[i] = Convert.ToSingle(words[5]);
			w[i] = Convert.ToSingle(words[6]);
			t[i] = Convert.ToSingle(words[7]);	
			a[i] = Convert.ToSingle(words[8]);	
			b[i] = Convert.ToSingle(words[9]);	
			c[i] = Convert.ToSingle(words[10]);	
			
			viewPos[i] = new Vector3(x[i], y[i], z[i]);	
			drot[i] = new Quaternion(u[i], v[i], w[i], t[i]);
			scale[i] = new Vector3(a[i], b[i], c[i]);	
			
			clone[i] = PrefabUtility.InstantiatePrefab(Selection.activeObject as GameObject) as GameObject; 
        	clone[i].transform.position = viewPos[i];
			clone[i].transform.rotation = drot[i];
			clone[i].transform.localScale = scale[i];
			clone[i].transform.parent = GameObject.Find ("House").transform;
			
        	Debug.Log("Instantiate Done!" + clone[i].transform.position + viewPos[i] + drot[i]);
		}
		Debug.Log (i);
		
	}	
}
