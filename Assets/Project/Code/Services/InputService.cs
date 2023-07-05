using UnityEngine;
using System;
using UnityEngine.Events;

public class InputService : MonoBehaviour
{
    public UnityEvent OnEscapePressed;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Push");
            OnEscapePressed?.Invoke();
        }
    }
}
