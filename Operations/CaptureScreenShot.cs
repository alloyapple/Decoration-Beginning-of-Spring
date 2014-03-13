//designed by chen 2013-8-29
//edit by htoo at 20131024_1515
//edit by htoo at 20131025_1030

using UnityEngine;
using System.Collections;
using System.IO;

public class CaptureScreenShot {
	
	//capture screen start position
    int _captureStart = 0;
    int _captureEnd = 0;
	
	//capture screen width,height
    int _captureWidth = 10;
    int _captureHeight = 10;
	
	//picture pathj
    string _path;
	
	//picture name;
	string _name;
	
	// Use this for initialization
	public CaptureScreenShot() 
        :this(Screen.width,Screen.height) {
	}

    public CaptureScreenShot(int width, int height) {
        CaptureWidth = width;
        CaptureHeight = height;
        Path = Application.dataPath + "/CapturePicture";
        FileName = "mirro";
    }

	public int CaptureStart {
		get {return _captureStart;}
		set{_captureStart=value;}
	}
	
	public int CaptureEnd {
		get {return _captureEnd;}
		set{_captureStart=value;}
	}
	
	public int CaptureWidth {
		get {return _captureWidth;}
		set{_captureWidth=value;}
	}
	
	public int CaptureHeight {
		get {return _captureHeight;}
		set{_captureHeight=value;}
	}
	
	public string Path {
		get {return _path;}
		set {_path=value;}
	}
	
	public string FileName {
		get {return _name;}
		set {_name=value;}
	}
	
	//fuction get screen 
    public IEnumerator GetCapture() {  
        yield return new WaitForEndOfFrame();  
        Texture2D texture = new Texture2D(CaptureWidth,CaptureHeight,TextureFormat.RGB24, true);
        texture.ReadPixels(new Rect(CaptureStart,CaptureEnd,CaptureWidth,CaptureHeight),CaptureStart,CaptureEnd);  
        texture.Apply();
        byte[] textureByte = texture.EncodeToPNG();
        File.WriteAllBytes(Path + "/" + FileName + ".png", textureByte);
	}
}
