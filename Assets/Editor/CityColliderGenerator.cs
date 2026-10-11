using UnityEngine;
using UnityEditor;

public class CityColliderGenerator : EditorWindow
{
    [MenuItem("Tools/City Collider Generator")]
    public static void ShowWindow()
    {
        GetWindow<CityColliderGenerator>("City Colliders");
    }

    private void OnGUI()
    {
        GUILayout.Label("City Collider Generator", EditorStyles.boldLabel);

        GUILayout.Space(10);

        GUILayout.Label(
            "Select Environment in the Hierarchy."
        );

        GUILayout.Space(15);

        if (GUILayout.Button(
            "REMOVE ALL CITY MESH COLLIDERS",
            GUILayout.Height(40)))
        {
            RemoveMeshColliders();
        }
    }

    private void RemoveMeshColliders()
    {
        GameObject root = Selection.activeGameObject;

        if (root == null)
        {
            EditorUtility.DisplayDialog(
                "No Object Selected",
                "Select Environment first.",
                "OK"
            );

            return;
        }

        MeshCollider[] colliders =
            root.GetComponentsInChildren<MeshCollider>();

        int removed = 0;

        foreach (MeshCollider collider in colliders)
        {
            DestroyImmediate(collider);
            removed++;
        }

        EditorUtility.DisplayDialog(
            "Collision Cleanup Complete",
            "Mesh Colliders removed: " + removed,
            "OK"
        );
    }
}