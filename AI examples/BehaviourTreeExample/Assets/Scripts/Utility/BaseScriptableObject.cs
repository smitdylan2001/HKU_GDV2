using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[InitializeOnLoad]
#endif
public abstract class BaseScriptableObject : ScriptableObject
{
    [SerializeField] private bool autoReset = true;
#if UNITY_EDITOR
    public BaseScriptableObject()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }
    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            //AssetDatabase.SaveAssets();
            OnReset();
        }
        if (state == PlayModeStateChange.EnteredEditMode && autoReset)
        {
            //Resources.UnloadAsset(this);
            OnReset();
        }
    }
#endif

    public abstract void OnReset();
}
