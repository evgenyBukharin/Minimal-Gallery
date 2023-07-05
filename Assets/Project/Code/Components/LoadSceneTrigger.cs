using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneTrigger : MonoBehaviour
{
    public event Action OnTriggered;
    [SerializeField] private Button[] _triggers;

    private void Start()
    {
        foreach (Button trigger in _triggers)
        {
            trigger.onClick.AddListener(OnTrigger);
        }
    }

    public void OnTrigger()
    {
        OnTriggered?.Invoke();
    }
}
