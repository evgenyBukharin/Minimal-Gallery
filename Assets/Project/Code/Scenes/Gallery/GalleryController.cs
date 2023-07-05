using UnityEngine;
using Zenject;

public class GalleryController : MonoBehaviour
{
    [Header("Links")] 
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private ImageLoader _imageLoader;
    [SerializeField] private LoadSceneTrigger _loadSceneTrigger;
    
    // В идеале нужно узнать количество изображений, а не использовать константу
    [Header("Setting")]
    [SerializeField] private int _imageCount;
    [SerializeField] private float _visibilityTrigger = 0.1f;
    [SerializeField] private string _imageNameEnding = ".jpg";
    [SerializeField] private GalleryImage _imagePrefab;
    [SerializeField] private Transform _parentTransform;

    private GalleryImage[] _galleryImages;
    private HistoryService _historyService;

    [Inject]
    public void Construct(HistoryService historyService)
    {
        _historyService = historyService;
    }
    
    private void Start()
    {
        CreateImageList();
    }

    private void Update()
    {
        foreach (GalleryImage image in _galleryImages)
        {
            if (image.isLoad)
                continue;
            
            if (_mainCamera.WorldToViewportPoint(image.transform.position).y + _visibilityTrigger > 0)
            {
                image.isLoad = true;
                _imageLoader.PrepareLoadImage(image);
            }
        }
    }

    private void CreateImageList()
    {
        _galleryImages = new GalleryImage[_imageCount];
        
        for (int i = 0; i < _imageCount; i++)
        {
            _galleryImages[i] = Instantiate(_imagePrefab);
            _galleryImages[i].Init((i+1) + _imageNameEnding);
            _galleryImages[i].transform.SetParent(_parentTransform);
            _galleryImages[i].transform.localScale = Vector3.one;

            _galleryImages[i].OnButtonClick += Texture2D =>
            {
                _historyService.selectedTexture = Texture2D;
                _loadSceneTrigger.OnTrigger();
            };
        }
    }
}
