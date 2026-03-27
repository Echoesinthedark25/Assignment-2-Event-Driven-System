using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerTrigger : MonoBehaviour
{
    public UnityEvent onLevelStart;

    private void Start()
    {
        onLevelStart = new UnityEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        onLevelStart.Invoke();
    }
}
