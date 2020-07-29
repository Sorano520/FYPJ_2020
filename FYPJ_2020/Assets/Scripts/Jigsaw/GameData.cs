using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    GAME_TYPES gameType;
    string date;
    TimeSpan timeStarted;
    TimeSpan timeEnded;
    [SerializeField] float time;
    [SerializeField] List<float> inactivity;
    [SerializeField] bool isInactive;
    [SerializeField] float inactivityPeriod;
    float minInactivityPeriod;
    [SerializeField] int movesTaken;
    [SerializeField] int errorsMade;

    #region Getters & Setters
    public GAME_TYPES GameType
    {
        get { return gameType; }
        set { gameType = value; }
    }
    public string Date
    {
        get { return date; }
        set { date = value; }
    }
    public TimeSpan TimeStarted
    {
        get { return timeStarted; }
        set { timeStarted = value; }
    }
    public List<float> Inactivity
    {
        get { return inactivity; }
        set { inactivity = value; }
    }
    public bool IsInactive
    {
        get { return isInactive; }
        set { isInactive = value; }
    }
    public int MovesTaken
    {
        get { return movesTaken; }
        set { movesTaken = value; }
    }
    public int ErrorsMade
    {
        get { return errorsMade; }
        set { errorsMade = value; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        date = DateTime.Now.Date.ToShortDateString();
        timeStarted = DateTime.Now.TimeOfDay;
        Debug.Log("Date: " + date);
        Debug.Log("Time: " + timeStarted);
        time = 0;
        inactivity = new List<float>();
        isInactive = true;
        inactivityPeriod = 0;
        minInactivityPeriod = 10;
        movesTaken = 0;
        errorsMade = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (isInactive) inactivityPeriod += Time.deltaTime;
    }
    
    public void CheckInactivity()
    {
        isInactive = false;
        if (inactivityPeriod < minInactivityPeriod)
        {
            inactivityPeriod = 0;
            return;
        }
        inactivity.Add(inactivityPeriod);
        inactivityPeriod = 0;
    }

    public void LogTime()
    {
        timeEnded = DateTime.Now.TimeOfDay;
        Debug.Log("Time: " + timeEnded);
    }
}
