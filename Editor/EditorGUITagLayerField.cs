//author htoo
//history

//date 20130819_1049

using UnityEngine;
using UnityEditor;

public class EditorGUITagLayerField : EditorWindow {

    string selectedTag = "";
    int selectedLayer = 0;
    StaticEditorFlags selectedFlags = 0;

    [MenuItem("Batch/Add Tag - Layer - Mask for Selection")]

    static void Init() {
        EditorGUITagLayerField window = EditorWindow.CreateInstance<EditorGUITagLayerField>() as EditorGUITagLayerField;
        window.position = new Rect(0, 0, 550, 70);
        window.Show();
    }

    void OnGUI() {
        selectedTag = EditorGUI.TagField(new Rect(3, 3, 300, 20),
            "New Tag:",
            selectedTag);

        selectedLayer = EditorGUI.LayerField(new Rect(3, 30,300, 20),
            "New Layer:",
            selectedLayer);

        selectedFlags = (StaticEditorFlags)EditorGUI.EnumMaskField(new Rect(3, 60 ,300,20),
            "Select Mask:",
            selectedFlags);

        if (Selection.transforms.Length != 0)
        {
            if (GUI.Button(new Rect(400, 3, 150, 20), "Change Tags"))
                SetTag(selectedTag);

            if (GUI.Button(new Rect(400, 30, 150, 20), "Change Layers"))
            {
                SetLayer(selectedLayer);
            }

            if (GUI.Button(new Rect(400, 60, 150, 20), "Change Flags"))
            {
                SetMask(selectedFlags);
            }
        }
    }

    void SetTag(string value) {
        foreach (var go in Selection.gameObjects) {
            go.tag = value;
        }
    }

    void SetLayer(int value) {
        foreach (var go in Selection.gameObjects) {
            go.layer = value;
        }
    }

    void SetMask(StaticEditorFlags value) {
        foreach (var go in Selection.gameObjects) {
            GameObjectUtility.SetStaticEditorFlags(go, value);
        }
    }

    [MenuItem("Batch/Add Tag - Layer - Mask for Selection", true)]
    static bool ValidateSelection() {
        return Selection.transforms.Length != 0;
    }
}
