using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Sphere))]
public class SphereEditor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        Sphere mySphere = (Sphere)target;
        EditorGUILayout.HelpBox("This is the Sphere Builder", MessageType.Info);
        DrawDefaultInspector();
        if (GUILayout.Button("Create Spheres"))
        {
            mySphere.UpdateMeshNow();
        }
        if (GUILayout.Button("Delete Sphere Points"))
        {
            mySphere.DestroyAllObjectsOfTag("spheres");
        }
    }
}
