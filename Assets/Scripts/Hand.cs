using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public delegate void OnReleaseHeldObject(Transform _t);
    public OnReleaseHeldObject onReleaseHeldObject;

    public GameObject normalHand, pinchHand;
    private Camera m_cam;
    private Vector2 m_mousePos;
    private Transform heldObject;
    private Vector3 offset;

    [HideInInspector]
    public AudioClip onPickUp;
    [HideInInspector]
    public AudioClip onRelease;

    private void Awake()
    {
        Release();
        m_cam = Camera.main;
        GameSettings.Inst.controls.Player.MousePos.performed += ctx => m_mousePos = ctx.ReadValue<Vector2>();
        GameSettings.Inst.controls.Player.LeftHold.performed += _ => Grab();
        GameSettings.Inst.controls.Player.LeftHold.canceled += _ => Release();
    }

    private void Update()
    {
        var screenMousePos = m_cam.ScreenToWorldPoint(m_mousePos).NewZ(0.0f);
        transform.position = screenMousePos;

        if(heldObject != null)
        {
            heldObject.position = screenMousePos + offset;
        }
    }

    private void Grab()
    {
        pinchHand.SetActive(true);
        normalHand.SetActive(false);
        DoRaycast(m_mousePos);
    }

    private void DoRaycast(Vector2 _mousePos)
    {
        Vector2 pos = m_cam.ScreenToWorldPoint(_mousePos);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 100.0f, 1 << 8);
        if (hit.collider != null && hit.transform.CompareTag("Holdable"))
        {
            if (onPickUp != null)
                SoundManager.Inst.PlaySoundEffect(onPickUp);
            heldObject = hit.transform;
            offset = hit.transform.position.To2D() - hit.point;
        }
    }

    private void Release()
    {
        pinchHand.SetActive(false);
        normalHand.SetActive(true);

        if(heldObject != null)
        {
            if (onPickUp != null)
                SoundManager.Inst.PlaySoundEffect(onRelease);
            onReleaseHeldObject?.Invoke(heldObject);
            heldObject = null;
        }
    }
}
