/* using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class LockCameraToViewRuntime
{
    static bool isLocked = false;
    static Camera targetCamera;

    // Keep running both in Edit Mode and Play Mode
    static LockCameraToViewRuntime()
    {
        SceneView.duringSceneGui += OnSceneGUI;
        EditorApplication.update += OnEditorUpdate;
    }

    static void OnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(10, 10, 220, 32));

        string label = isLocked ? "🔒 Unlock Camera View" : "🔓 Lock Camera to View";
        if (GUILayout.Button(label, GUILayout.Height(24)))
        {
            if (!isLocked)
            {
                // Lock to currently selected camera
                if (Selection.activeGameObject && Selection.activeGameObject.TryGetComponent(out Camera cam))
                {
                    targetCamera = cam;
                    isLocked = true;
                    Debug.Log($"Locked to camera: {cam.name}");
                }
                else
                {
                    Debug.LogWarning("Select a Camera in the Hierarchy first before locking.");
                }
            }
            else
            {
                // Unlock
                isLocked = false;
                targetCamera = null;
                Debug.Log("Camera unlocked.");
            }
        }

        GUILayout.EndArea();
        Handles.EndGUI();
    }

    static void OnEditorUpdate()
    {
        // Don't do anything if not locked or no camera
        if (!isLocked || targetCamera == null)
            return;

        SceneView sceneView = SceneView.lastActiveSceneView;
        if (sceneView == null || sceneView.camera == null)
            return;

        // Continuously sync camera position + rotation to the SceneView camera
        targetCamera.transform.position = sceneView.camera.transform.position;
        targetCamera.transform.rotation = sceneView.camera.transform.rotation;
    }
}

*/