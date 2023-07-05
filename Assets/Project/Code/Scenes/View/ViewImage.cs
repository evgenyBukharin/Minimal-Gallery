using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ViewImage : MonoBehaviour
{
    [SerializeField] private RawImage _rawImage;
    
    [Inject]
    public void Construct(HistoryService _historyService)
    {
        if (_historyService.selectedTexture != null)
            _rawImage.texture = _historyService.selectedTexture;
    }
}
