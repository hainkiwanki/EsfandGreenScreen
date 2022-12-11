using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AiController : MonoBehaviour
{
    private CharacterControl control;
    
    private void Awake()
    {
        control = GetComponent<CharacterControl>();
    }

    public void MoveLeft(float _time, UnityAction _oncomplete = null)
    {
        control.xDirection = -1;
        StartCoroutine(ExecuteAfterSeconds(_time, () =>
        {
            control.xDirection = 0.0f;
            _oncomplete?.Invoke();
        }));
    }

    IEnumerator ExecuteAfterSeconds(float _time, UnityAction _callback)
    {
        yield return new WaitForSecondsRealtime(_time);
        _callback.Invoke();
    }
}
