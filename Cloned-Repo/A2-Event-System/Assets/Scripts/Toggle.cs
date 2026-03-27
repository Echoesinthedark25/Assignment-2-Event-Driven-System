using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Toggle : MonoBehaviour, ISwitchable
{
    public Sprite toggleOn;
    public Sprite toggleOff;
    public bool isRed = true; //if false then blue

    // we should hide this because we do not want other developers
    // attempting to connect this Unity Event in the editor
    [HideInInspector]
    public UnityEvent<bool, bool> OnToggle;

    bool toggleState = false;
    SpriteRenderer spriteRenderer;
    Animator animator;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (OnToggle == null)
        {
            OnToggle = new UnityEvent<bool, bool>();
        }
    }

    void UpdateState()
    {
        spriteRenderer.sprite = toggleState ? toggleOn : toggleOff;
        animator.SetTrigger("StartHit");
    }

    public void Switch()
    {
        toggleState = !toggleState;
        UpdateState();

        OnToggle.Invoke(toggleState, isRed);
    }
}
