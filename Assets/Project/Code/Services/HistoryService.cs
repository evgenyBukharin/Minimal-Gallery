using System.Collections.Generic;
using UnityEngine;

public class HistoryService : MonoBehaviour
{
    public Texture2D selectedTexture { get; set; }
    public Dictionary<string, Texture2D> loadedTextures = new Dictionary<string, Texture2D>();
    private Stack<SceneType> _sceneHistory = new Stack<SceneType>();

    public void SaveCurrentScene(SceneType sceneType)
    {
        _sceneHistory.Push(sceneType);
    }

    public SceneType GetPreviousScene()
    {
        SceneType sceneType = _sceneHistory.Pop();
        Debug.Log(sceneType);
        return sceneType;
    }

    public bool IsHistoryEmpty()
    {
        if (_sceneHistory.Count == 0)
        {
            return true;
        }

        return false;
    }
}
