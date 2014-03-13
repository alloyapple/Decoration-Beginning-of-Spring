using UnityEngine;
using System.Collections;

public class OnDragging : MonoBehaviour {

    public MonoBehaviour target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrag(Vector2 delta){

    }

    void OnDrop(GameObject drag){

    }

    void OnPress(bool isDown){
        if (isDown == true)
        {
            target.enabled = false;
        } 
        else
        {
            target.enabled = true;
        }
    }
}
