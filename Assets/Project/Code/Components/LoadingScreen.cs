using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public event Action OnLoadAnimationComplete;
    
    [Header("Links")]
    [SerializeField] private GameObject _loadScreenCanvas;
    [SerializeField] private GameObject _loadContent;
    [SerializeField] private Image _loadBackground;
    [SerializeField] private TMP_Text _percentageTMP;
    [SerializeField] private Slider _progressBar;
    
    [Header("Settings")]
    [SerializeField] private float _loadSpeed = 1f;
    [SerializeField] private float _fadeSpeed = 1f;
    [SerializeField] private float _afterLoadDelay = 0.2f;
    private float _loadProgress;

    public void UpdateLoad(float progress)
    {
        _loadProgress = progress;
    }

    public void SetActive(bool active)
    {
        switch (active)
        {
            case true:
                _loadContent.SetActive(true);
                _loadScreenCanvas.SetActive(true);
                StartCoroutine(ProgressBarAnimation());
                break;
            
            case false:
                _loadContent.SetActive(false);
                StartCoroutine(FadeAnimation());
                break;
        }
    }

    private IEnumerator ProgressBarAnimation()
    {
        float fill = 0f;

        while (fill < 1f)
        {
            fill += Time.deltaTime * _loadSpeed;
            
            if (fill > _loadProgress)
                yield return null;

            if (fill > 1f)
                fill = 1f;
                
            _progressBar.value = fill;
            _percentageTMP.text = (int)(fill * 100) + "%";
            yield return new WaitForEndOfFrame();
        }

        OnLoadAnimationComplete?.Invoke();
    }

    private IEnumerator FadeAnimation()
    {
        yield return new WaitForSeconds(_afterLoadDelay);
        Color color = _loadBackground.color;
        float alpha = 1f;

        while (alpha > 0)
        {
            _loadBackground.color = new Color(color.r, color.g, color.b, alpha);
            alpha -= Time.deltaTime * _fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        
        _loadScreenCanvas.SetActive(false);
        _loadBackground.color = color;
    }
}
