using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    bool timerStarted = false;
    float timerTime = 0;

    TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            timerTime += Time.deltaTime;
            timerText.text = timerTime.ToString();
        }
    }

    public void startTimer()
    {
        timerStarted = true;
    }
}
