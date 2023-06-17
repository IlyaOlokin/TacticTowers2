using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class DefaultSceneOnPlayLoader
{
    static DefaultSceneOnPlayLoader() 
    {
        var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
        EditorSceneManager.playModeStartScene = sceneAsset;
    }
}
