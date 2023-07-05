using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SceneType _nextSceneType;
    
    [Inject]
    private void Construct(LoadingService loadingService)
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        loadingService.LoadScene(_nextSceneType, false);
    }
}
