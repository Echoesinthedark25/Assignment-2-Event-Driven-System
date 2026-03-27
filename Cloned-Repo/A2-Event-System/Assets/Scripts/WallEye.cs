using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WallEyeState { Closed, Open, Defeated }

public class WallEye : MonoBehaviour, ISwitchable
{
    public Sprite eyeClosed;
    public Sprite eyeOpened;
    public Sprite eyeDefeated;

    public Character character;
    public float characterDistance = 5.0f;
    public bool useDistance = false;

    [HideInInspector]
    public WallEyeState eyeState = WallEyeState.Closed;

    [HideInInspector]
    public UnityEvent<WallEyeState> OnEyeStateChanged;

    SpriteRenderer spriteRenderer;
    Animator animator;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (OnEyeStateChanged == null)
        {
            OnEyeStateChanged = new UnityEvent<WallEyeState>();
        }
    }
    
    void Update()
    {
        if (!useDistance) return;

        // eye can only open and close if not defeated
        if (eyeState != WallEyeState.Defeated)
        {
            // when character is close enough to the eye, open it
            if (Vector3.Distance(transform.position, character.transform.position) < characterDistance)
            {
                // we don't want to trigger this every frame,
                // only initially when the eye state is set
                //Eye will remain open
                if (eyeState != WallEyeState.Open)
                {
                    eyeState = WallEyeState.Open;
                    UpdateState();
                }
            }
           
        }
    }

    void UpdateState()
    {
        switch (eyeState)
        {
            
            case WallEyeState.Open:
                spriteRenderer.sprite = eyeOpened;
                break;
            case WallEyeState.Defeated:
                spriteRenderer.sprite = eyeDefeated;
                break;
        }
        animator.SetTrigger("StartHit");
    }

    public void Switch()
    {
        if (eyeState == WallEyeState.Open)
        {
            eyeState = WallEyeState.Defeated;
            UpdateState();
            OnEyeStateChanged.Invoke(eyeState);
        }
    }

    // this is called from the toggle Unity Event
    public void OpenClose(bool close, bool isRed)
    {
        if (eyeState != WallEyeState.Defeated)
        {
            if (close)
            {
                eyeState = WallEyeState.Closed;
                UpdateState();
                OnEyeStateChanged.Invoke(eyeState);
            }
            else
            {
                eyeState = WallEyeState.Open;
                UpdateState();
                OnEyeStateChanged.Invoke(eyeState);
            }
        }
    }
}
