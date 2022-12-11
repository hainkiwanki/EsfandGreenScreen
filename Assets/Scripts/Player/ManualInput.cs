using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterControl))]
public class ManualInput : MonoBehaviour
{
    private Camera cam;
    private CharacterControl control;
    private float inputX;
    private float inputY;
    private bool hasLeftClicked = false;
    private bool hasRightClicked = false;

    private void Awake()
    {
        control = GetComponent<CharacterControl>();
        BindPlayerInput();
    }

    private void BindPlayerInput()
    {
        GameSettings.Inst.controls.Player.MoveHor.performed += ctx => inputX = ctx.ReadValue<float>();
        GameSettings.Inst.controls.Player.MoveHor.canceled += _ => inputX = 0f;

        GameSettings.Inst.controls.Player.MoveVer.performed += ctx => inputY = ctx.ReadValue<float>();
        GameSettings.Inst.controls.Player.MoveVer.canceled += _ => inputY = 0f;

        GameSettings.Inst.controls.Player.LeftClick.performed += _ => hasLeftClicked = true;
        GameSettings.Inst.controls.Player.RightClick.performed += _ => hasRightClicked = true;

        GameSettings.Inst.controls.Player.Interact.performed += _ => control.Interact();
    }

    private void Update()
    {
        control.xDirection = inputX;
        control.yDirection = inputY;
        control.leftClicked = hasLeftClicked;
        control.rightClicked = hasRightClicked;

        if (hasLeftClicked)
            hasLeftClicked = false;

        if (hasRightClicked)
            hasRightClicked = false;
    }
}
