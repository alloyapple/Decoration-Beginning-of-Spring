using UnityEngine;
using System;
using DecorationSystem;
using DecorationSystem.MaterialUtilities;

public class ChangeTextureScaleView : MonoBehaviourBase {

    public bool IsMouseDown{get;set;}
    public Vector3 currentMousePosition { get; set; }
    public Material SelectMaterial { get; set; }

    [NotAllowNull]
    public Texture texture;

    void start() {
        IsMouseDown = false;
        currentMousePosition = Vector3.zero;
    }

    void Update(){
        //if (Input.GetMouseButton(0)) {
        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
        //        Material selectMaterial = hit.transform.gameObject.GetMaterialUsingTriangleIndexOfMeshCollider(hit.triangleIndex);
        //        if (selectMaterial != null) selectMaterial.SetMainColor(Color.red);

        //        // other function
        //        //Action setMaterialColor = () => selectMaterial.SetMainColor(Color.red);
        //        //setMaterialColor.Unless(() => selectMaterial == null);
        //    }
        //}
    }

    void OnGUI() {
        if (IsMouseDown) {
            RaycastHit hit;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(currentMousePosition), out hit)) return;
            SelectMaterial = hit.transform.gameObject.GetMaterialUsingTriangleIndexOfMeshCollider(hit.triangleIndex);
            if (SelectMaterial == null) return;

            int centerX = Screen.height / 2;
            int centerY = Screen.width / 2;

            int width = 50;
            int height = 50;
            int space = 50;

            string offsetX =  GUI.TextArea(new Rect(centerX - width - space,centerY - height - space,width,height),"2");
            string offsetY = GUI.TextArea(new Rect(centerX, centerY - height - space, width, height), "2");
            string scaleX = GUI.TextArea(new Rect(centerX - width - space,centerY,width,height), "2");
            string scaleY = GUI.TextArea(new Rect(centerX,centerY,width,height), "2");
            
            if (GUI.Button(new Rect(centerX + width+ space,centerY +height+space,width,height),"ok")) {
                SelectMaterial
                    .SetMainColor(Color.red)
                    .SetMainOffset(new Vector2(int.Parse(offsetX), int.Parse(offsetY)))
                    .SetMainScale(new Vector2(int.Parse(scaleX), int.Parse(scaleY)))
                    .SetMainTexture(texture);
            }
        }
    }

    void OnMouseDown() {
        if (IsMouseDown) {
            IsMouseDown = false;
        }
        else {
            IsMouseDown = true;
            currentMousePosition = Input.mousePosition;
        }

    }
}