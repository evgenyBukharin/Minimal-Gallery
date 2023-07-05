using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using Zenject;

public class ImageLoader : MonoBehaviour
{
    [SerializeField] private string _urlPath;
    private HistoryService _historyService;
    
    [Inject]
    public void Construct(HistoryService historyService)
    {
        _historyService = historyService;
    }

    public void PrepareLoadImage(GalleryImage image)
    {
        string completeUrlPath = _urlPath + image.imageName;
        
        if (_historyService.loadedTextures.TryGetValue(image.imageName, out Texture2D texture))
        {
            image.SetTexture(texture);
            return;
        }

        StartCoroutine(LoadImage(image, completeUrlPath));
    }

    private IEnumerator LoadImage(GalleryImage image, string urlPath)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(urlPath))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Error loading the image: {webRequest.error}");
                yield return null;
            }

            Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
            _historyService.loadedTextures.Add(image.imageName, texture);
            image.SetTexture(texture);
        }
    }
}
