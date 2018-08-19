using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EmojiContainer))]
public class EmojiControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EmojiContainer myScript = (EmojiContainer)target;
        if (GUILayout.Button("Create Prefabs"))
        {
            myScript.CreateEmojisButtons();
        }
    }
}
