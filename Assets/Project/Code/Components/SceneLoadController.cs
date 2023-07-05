using UnityEngine;
using Zenject;

public class SceneLoadController : MonoBehaviour
{
    [SerializeField] private LoadSceneTrigger _loadNextSceneTrigger;
    [SerializeField] private LoadSceneTrigger _loadPreviousSceneTrigger;
    [SerializeField] private SceneType _currentSceneType;
    [SerializeField] private SceneType _nextSceneType;
    
    private LoadingService _loadingService;
    private HistoryService _historyService;
    
    [Inject]
    public void Construct(LoadingService loadingService, HistoryService historyService, InputService inputService)
    {
        _loadingService = loadingService;
        _historyService = historyService;
        
        inputService.OnEscapePressed.AddListener(LoadPreviousScene);
        
        if (_loadNextSceneTrigger != null)
            _loadNextSceneTrigger.OnTriggered += LoadNextScene;
        
        if (_loadPreviousSceneTrigger != null)
            _loadPreviousSceneTrigger.OnTriggered += LoadPreviousScene;
    }
    
    public void LoadNextScene()
    {
        _historyService.SaveCurrentScene(_currentSceneType);
        _loadingService.LoadScene(_nextSceneType, true);
    }

    private void LoadPreviousScene()
    {
        if (_historyService.IsHistoryEmpty())
            return;
        
        SceneType previousScene = _historyService.GetPreviousScene();
        _loadingService.LoadScene(previousScene, true);
    }
}
