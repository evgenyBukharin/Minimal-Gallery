using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingService : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private int _menuId = 0;
    [SerializeField] private int _galleryId = 1;
    [SerializeField] private int _viewId = 2;

    private void Start()
    {
        _loadingScreen.SetActive(false);
    }

    public void LoadScene(SceneType type, bool animate = false)
    {
        int sceneId = GetLevelId(type);
        
        if (!animate)
        {
            SceneManager.LoadScene(sceneId);
            return;
        }

        StartCoroutine(LoadScene(sceneId));
    }

    private IEnumerator LoadScene(int sceneId)
    {
        _loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);
        asyncLoad.allowSceneActivation = false;
        
        _loadingScreen.OnLoadAnimationComplete += () =>
        {
            asyncLoad.allowSceneActivation = true;
            _loadingScreen.SetActive(false);
        };

        while (!asyncLoad.allowSceneActivation)
        {
            _loadingScreen.UpdateLoad(asyncLoad.progress);
            yield return null;
        }
        
        yield return null;
    }

    private int GetLevelId(SceneType type)
    {
        // Default value
        int sceneId = _menuId; 
        
        switch (type)
        {
            case SceneType.Gallery:
                sceneId = _galleryId;
                break;
            
            case SceneType.View:
                sceneId = _viewId;
                break;
        }

        return sceneId;
    }
}
