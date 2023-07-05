using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class GalleryImage : MonoBehaviour
{
    public event Action<Texture2D> OnButtonClick; 
    public bool isLoad { get; set; }
    public string imageName => _imageName;

    [SerializeField] private Button _button;
    [SerializeField] private RawImage _image;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeSpeed = 1f;

    private Texture2D _imageTexture;
    private string _imageName;
    
    public void Init(string imageName)
    {
        _imageName = imageName;
        _button.onClick.AddListener(() => { OnButtonClick?.Invoke(_imageTexture); });
    }

    public void SetTexture(Texture2D texture2D)
    {
        // Start Animation
        _imageTexture = texture2D;
        _image.texture = _imageTexture;
        StartCoroutine(FadeAnimation());
    }

    private IEnumerator FadeAnimation()
    {
        float alpha = 1f;
        Color startColor = _fadeImage.color;
        
        while (alpha > 0)
        {
            _fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            alpha -= Time.deltaTime * _fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        
        _fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, 0);
        yield return null;
    }
}
