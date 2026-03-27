using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject barriers;
    public Toggle toggle1;
    public Toggle toggle2;
    public Door door;
    public TimerUI timer;
    public TimerTrigger trigger;
    private WallEyeState eyeState; 

    // the level manager is responsible for connecting the core game system events
    // notice that these events have arguments - it's not possible to pass arguments to
    // events in Unity when using the Editor (what we did in Module 1)
    // arguments make the events more flexible

    private void Start()
    {
        foreach (Transform child in barriers.transform)
        {
            Barrier barrier = child.GetComponent<Barrier>();
            toggle1.OnToggle.AddListener(barrier.Move);
            toggle2.OnToggle.AddListener(barrier.Move);
        }

        trigger.onLevelStart.AddListener(timer.startTimer);
        
    }

    public void Update()
    {
        lockDoor(eyeState);
    }
    public void lockDoor(WallEyeState eyeState)
    {
        if (eyeState == WallEyeState.Open)
        {
            door.SetLock(false);
        }
    }
}
