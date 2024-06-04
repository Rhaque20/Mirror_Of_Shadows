using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Priority
    // Pause/Death = 1, ,Hitstun = 2
    enum PauseCause{Pause = 1, Death = 1, PerfectDefense = 2, HitStun = 3, None = 99};

    private PauseCause activePriority = PauseCause.None;

    float currentTimeScale = 1.0f;

    public static Action<bool> invokePause{get;private set;}
    public static Action slowDown{get;private set;}
    public static Action<float> hitStop{get;private set;}

    private bool superPause = false;
    
    private Coroutine activeSlowDown = null;

    void Start()
    {
        invokePause = SetPause;
        slowDown = ActivateDefenseSlowDown;
        hitStop = ActivateHitStop;

    }

    private bool CheckPriority(PauseCause curAction)=>((int)curAction <= (int)activePriority);
    
    public void SetPause(bool isPause)
    {
        if (!CheckPriority(PauseCause.Pause))
            return;

        if (isPause)
        {
            Time.timeScale = 0.0f;
            superPause = true;
        }
        else
        {
            Time.timeScale = currentTimeScale;
            superPause = false;
        }
        
    }

    private IEnumerator ActiveSlowDown(float duration, float scale)
    {
        currentTimeScale = scale;
        Time.timeScale = currentTimeScale;
        yield return new WaitForSecondsRealtime(duration);
        currentTimeScale = 1f;
        Time.timeScale = currentTimeScale;
        activeSlowDown = null;
    }

    public void ActivateDefenseSlowDown()
    {
        // if (!CheckPriority(PauseCause.PerfectDefense))
        //     return;
        
        if (activeSlowDown == null)
        {
            Debug.Log("Trigger slowdown");
            activeSlowDown = StartCoroutine(ActiveSlowDown(0.5f,0.15f));
        }
        
    }

    public void ActivateHitStop(float duration)
    {
        if (activeSlowDown == null)
        {
            Debug.Log("Trigger slowdown");
            activeSlowDown = StartCoroutine(ActiveSlowDown(duration,0.05f));
        }
    }

    void Update()
    {
        if (Time.timeScale != currentTimeScale && !superPause)
        {
            Time.timeScale = currentTimeScale;
        }
    }
}
