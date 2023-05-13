using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestEasyTime;
    public TextMeshProUGUI bestStandartTime;
    public TextMeshProUGUI bestHardTime;

    
    private float currentTime;

    public bool go = false;

    public float bestTimeEasy;
    public float bestTimeStandart;
    public float bestTimeHard;

    void Start()
    {

        String bestTimeNum = PlayerPrefs.GetString("bestEasyTime", "-");
        if (bestTimeNum != "-")
        {
            TimeSpan time = TimeSpan.ParseExact(bestTimeNum, "hh\\:mm\\:ss", null);
            bestTimeEasy = (int)time.TotalSeconds;
        }
        bestEasyTime.text = bestTimeNum;

        bestTimeNum = PlayerPrefs.GetString("bestStandartTime", "-");
        if (bestTimeNum != "-")
        {
            TimeSpan time = TimeSpan.ParseExact(bestTimeNum, "hh\\:mm\\:ss", null);
            bestTimeStandart = (int)time.TotalSeconds;
        }
        bestStandartTime.text = bestTimeNum;

        bestTimeNum = PlayerPrefs.GetString("bestHardTime", "-");
        if (bestTimeNum != "-")
        {
            TimeSpan time = TimeSpan.ParseExact(bestTimeNum, "hh\\:mm\\:ss", null);
            bestTimeHard = (int)time.TotalSeconds;
        }
        bestHardTime.text = bestTimeNum;
    }

    
    void Update()
    {
        if (go)
        {
            currentTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            string timeString = time.ToString("hh\\:mm\\:ss");
            timerText.text = timeString;
        }
        
    }

    public void SetTime(float time)
    {
        currentTime = 0;
        TimeSpan t = TimeSpan.FromSeconds(currentTime);
        string timeString = t.ToString("hh\\:mm\\:ss");
        timerText.text = timeString;
    }

    public void CompareEasyBest()
    {
        int bestTime;
        if (bestEasyTime.text == "-")
        {
            bestTime = int.MaxValue;

        } else
        {
            TimeSpan time = TimeSpan.ParseExact(bestEasyTime.text, "hh\\:mm\\:ss", null);
            bestTime = (int)time.TotalSeconds;
        }
        
        if (Mathf.RoundToInt(currentTime) < bestTime)
        {
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            bestEasyTime.text = time.ToString("hh\\:mm\\:ss");

            PlayerPrefs.SetString("bestEasyTime", bestEasyTime.text);
            PlayerPrefs.Save();
        }
    }

    public void CompareStandartBest()
    {
        int bestTime;
        if (bestStandartTime.text == "-")
        {
            bestTime = int.MaxValue;

        }
        else
        {
            TimeSpan time = TimeSpan.ParseExact(bestStandartTime.text, "hh\\:mm\\:ss", null);
            bestTime = (int)time.TotalSeconds;
        }

        if (Mathf.RoundToInt(currentTime) < bestTime)
        {
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            bestStandartTime.text = time.ToString("hh\\:mm\\:ss");

            PlayerPrefs.SetString("bestStandartTime", bestStandartTime.text);
            PlayerPrefs.Save();
        }

    }

    public void CompareHardBest()
    {
        int bestTime;
        if (bestHardTime.text == "-")
        {
            bestTime = int.MaxValue;

        }
        else
        {
            TimeSpan time = TimeSpan.ParseExact(bestHardTime.text, "hh\\:mm\\:ss", null);
            bestTime = (int)time.TotalSeconds;
        }

        if (Mathf.RoundToInt(currentTime) < bestTime)
        {
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            bestHardTime.text = time.ToString("hh\\:mm\\:ss");

            PlayerPrefs.SetString("bestStandartTime", bestHardTime.text);
            PlayerPrefs.Save();
        }
    }
}
