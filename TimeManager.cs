using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool speedingUp = false;
    public bool slowingDown = false;

    [SerializeField] float timeChangeInSeconds = 0.5f;
    [HideInInspector] public float targetSlowDownFactor;
    [HideInInspector] public float targetSpeedUpFactor;
    public float timeScaleMeter;

    private void Update()
    {
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0f, 0.01f);

        if (speedingUp && !slowingDown && Time.timeScale < 1f) 
        {
            Time.timeScale += (1f / timeChangeInSeconds) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime += (0.01f / timeChangeInSeconds) * Time.unscaledDeltaTime;
            if (Time.timeScale >= targetSpeedUpFactor) 
            {
                speedingUp = false;
            }
        }
        else if (!speedingUp && slowingDown && Time.timeScale >= 1f) 
        {
            if (Time.timeScale >= targetSlowDownFactor)
            {
                Time.timeScale = targetSlowDownFactor;
                Time.fixedDeltaTime = 0.02f * targetSlowDownFactor;
            }
            else
            {
                slowingDown = false;
            }
        }
        else if(!speedingUp && !slowingDown && Time.timeScale >= 1f)
        {
            speedingUp = false;
            slowingDown = false;
        }

        timeScaleMeter = Time.timeScale;
    }

    public void SlowMo(float _targetSlowDownFactor = 0.02f) 
    {
        targetSlowDownFactor = _targetSlowDownFactor;
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
        slowingDown = true;
        speedingUp = false;
    }

    public void SpeedUp(float _targetSpeedUpFactor = 1f) 
    {
        targetSpeedUpFactor = _targetSpeedUpFactor;
        speedingUp = true;
        slowingDown = false;
    }
}
