using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [HideInInspector]
    public float xDirection = 0.0f;
    [HideInInspector]
    public float yDirection = 0.0f;
    [HideInInspector]
    public bool leftClicked = false;
    [HideInInspector]
    public bool rightClicked = false;
    private bool flipped = true;

    public float speedMod = 1.0f;

    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private SpriteRenderer spriteRend;

    private Interactable currentInteractable = null;

    public void ChangeInteractable(Interactable _interactable)
    {
        if (currentInteractable != null)
            currentInteractable.HideInteractionKey();

        currentInteractable = _interactable;

        if (currentInteractable != null)
            currentInteractable.ShowInteractionKey();
    }

    public void Interact()
    {
        if (currentInteractable != null)
            currentInteractable.Use();
    }

    public void Flip(bool _flip)
    {
        if (flipped != _flip)
        {
            spriteRend.flipX = _flip;
            flipped = _flip;
        }
    }

    public void Move(float _dir, float _speed)
    {
        Vector2 pos = transform.position;
        rigidBody.MovePosition(pos + Vector2.right * _dir * _speed * speedMod);
    }
}
