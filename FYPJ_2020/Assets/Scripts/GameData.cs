using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Daily
{
    // General Data
    public SortedDictionary<TimeSpan, float> aveDailyTime;
    public SortedDictionary<TimeSpan, float> aveDailyMainMenuTime;
    public SortedDictionary<TimeSpan, float> aveDailyCollectionTime;
    public SortedDictionary<TimeSpan, float> aveDailyJigsawSelectTime;
    public SortedDictionary<TimeSpan, float> aveDailyTangramSelectTime;
    public SortedDictionary<TimeSpan, float> aveDailyColouringSelectTime;
    public SortedDictionary<TimeSpan, float> aveDailyInactivityPeriod;

    // Jigsaw Data
    public SortedDictionary<TimeSpan, float> aveDailyJigsawTime;
    public SortedDictionary<TimeSpan, int> aveDailyJigsawMovesTaken;
    public SortedDictionary<TimeSpan, int> aveDailyJigsawErrorsMade;
    public SortedDictionary<TimeSpan, int> aveDailyJigsawGamesPlayed;

    // Tangram Data
    public SortedDictionary<TimeSpan, float> aveDailyTangramTime;
    public SortedDictionary<TimeSpan, int> aveDailyTangramMovesTaken;
    public SortedDictionary<TimeSpan, int> aveDailyTangramErrorsMade;
    public SortedDictionary<TimeSpan, int> aveDailyTangramGamesPlayed;

    // Colouring Data
    public SortedDictionary<TimeSpan, float> aveDailyColouringTime;
    public SortedDictionary<TimeSpan, int> aveDailyColouringGamesPlayed;

    public Daily()
    {
        aveDailyTime = new SortedDictionary<TimeSpan, float>();
        aveDailyMainMenuTime = new SortedDictionary<TimeSpan, float>();
        aveDailyCollectionTime = new SortedDictionary<TimeSpan, float>();
        aveDailyJigsawSelectTime = new SortedDictionary<TimeSpan, float>();
        aveDailyTangramSelectTime = new SortedDictionary<TimeSpan, float>();
        aveDailyColouringSelectTime = new SortedDictionary<TimeSpan, float>();
        aveDailyInactivityPeriod = new SortedDictionary<TimeSpan, float>();
        aveDailyJigsawTime = new SortedDictionary<TimeSpan, float>();
        aveDailyTangramTime = new SortedDictionary<TimeSpan, float>();
        aveDailyColouringTime = new SortedDictionary<TimeSpan, float>();

        aveDailyJigsawMovesTaken = new SortedDictionary<TimeSpan, int>();
        aveDailyJigsawErrorsMade = new SortedDictionary<TimeSpan, int>();
        aveDailyJigsawGamesPlayed = new SortedDictionary<TimeSpan, int>();
        aveDailyTangramMovesTaken = new SortedDictionary<TimeSpan, int>();
        aveDailyTangramErrorsMade = new SortedDictionary<TimeSpan, int>();
        aveDailyTangramGamesPlayed = new SortedDictionary<TimeSpan, int>();
        aveDailyColouringGamesPlayed = new SortedDictionary<TimeSpan, int>();
    }
}

public class Monthly
{
    // General Data
    public SortedDictionary<int, float> aveMonthlyTime;
    public SortedDictionary<int, float> aveMonthlyMainMenuTime;
    public SortedDictionary<int, float> aveMonthlyCollectionTime;
    public SortedDictionary<int, float> aveMonthlyJigsawSelectTime;
    public SortedDictionary<int, float> aveMonthlyTangramSelectTime;
    public SortedDictionary<int, float> aveMonthlyColouringSelectTime;
    public SortedDictionary<int, float> aveMonthlyInactivityPeriod;

    // Jigsaw Data
    public SortedDictionary<int, float> aveMonthlyJigsawTime;
    public SortedDictionary<int, int> aveMonthlyJigsawMovesTaken;
    public SortedDictionary<int, int> aveMonthlyJigsawErrorsMade;
    public SortedDictionary<int, int> aveMonthlyJigsawGamesPlayed;

    // Tangram Data
    public SortedDictionary<int, float> aveMonthlyTangramTime;
    public SortedDictionary<int, int> aveMonthlyTangramMovesTaken;
    public SortedDictionary<int, int> aveMonthlyTangramErrorsMade;
    public SortedDictionary<int, int> aveMonthlyTangramGamesPlayed;

    // Colouring Data
    public SortedDictionary<int, float> aveMonthlyColouringTime;
    public SortedDictionary<int, int> aveMonthlyColouringGamesPlayed;

    public Monthly()
    {
        aveMonthlyTime = new SortedDictionary<int, float>();
        aveMonthlyMainMenuTime = new SortedDictionary<int, float>();
        aveMonthlyCollectionTime = new SortedDictionary<int, float>();
        aveMonthlyJigsawSelectTime = new SortedDictionary<int, float>();
        aveMonthlyTangramSelectTime = new SortedDictionary<int, float>();
        aveMonthlyColouringSelectTime = new SortedDictionary<int, float>();
        aveMonthlyInactivityPeriod = new SortedDictionary<int, float>();
        aveMonthlyJigsawTime = new SortedDictionary<int, float>();
        aveMonthlyTangramTime = new SortedDictionary<int, float>();
        aveMonthlyColouringTime = new SortedDictionary<int, float>();

        aveMonthlyJigsawMovesTaken = new SortedDictionary<int, int>();
        aveMonthlyJigsawErrorsMade = new SortedDictionary<int, int>();
        aveMonthlyJigsawGamesPlayed = new SortedDictionary<int, int>();
        aveMonthlyTangramMovesTaken = new SortedDictionary<int, int>();
        aveMonthlyTangramErrorsMade = new SortedDictionary<int, int>();
        aveMonthlyTangramGamesPlayed = new SortedDictionary<int, int>();
        aveMonthlyColouringGamesPlayed = new SortedDictionary<int, int>();
    }
}

public class Yearly
{
    // General Data
    public SortedDictionary<int, float> aveYearlyTime;
    public SortedDictionary<int, float> aveYearlyMainMenuTime;
    public SortedDictionary<int, float> aveYearlyCollectionTime;
    public SortedDictionary<int, float> aveYearlyJigsawSelectTime;
    public SortedDictionary<int, float> aveYearlyTangramSelectTime;
    public SortedDictionary<int, float> aveYearlyColouringSelectTime;
    public SortedDictionary<int, float> aveYearlyInactivityPeriod;

    // Jigsaw Data
    public SortedDictionary<int, float> aveYearlyJigsawTime;
    public SortedDictionary<int, int> aveYearlyJigsawMovesTaken;
    public SortedDictionary<int, int> aveYearlyJigsawErrorsMade;
    public SortedDictionary<int, int> aveYearlyJigsawGamesPlayed;

    // Tangram Data
    public SortedDictionary<int, float> aveYearlyTangramTime;
    public SortedDictionary<int, int> aveYearlyTangramMovesTaken;
    public SortedDictionary<int, int> aveYearlyTangramErrorsMade;
    public SortedDictionary<int, int> aveYearlyTangramGamesPlayed;

    // Colouring Data
    public SortedDictionary<int, float> aveYearlyColouringTime;
    public SortedDictionary<int, int> aveYearlyColouringGamesPlayed;

    public Yearly()
    {
        aveYearlyColouringTime = new SortedDictionary<int, float>();
        aveYearlyTime = new SortedDictionary<int, float>();
        aveYearlyMainMenuTime = new SortedDictionary<int, float>();
        aveYearlyCollectionTime = new SortedDictionary<int, float>();
        aveYearlyJigsawSelectTime = new SortedDictionary<int, float>();
        aveYearlyTangramSelectTime = new SortedDictionary<int, float>();
        aveYearlyColouringSelectTime = new SortedDictionary<int, float>();
        aveYearlyInactivityPeriod = new SortedDictionary<int, float>();
        aveYearlyJigsawTime = new SortedDictionary<int, float>();
        aveYearlyTangramTime = new SortedDictionary<int, float>();

        aveYearlyJigsawMovesTaken = new SortedDictionary<int, int>();
        aveYearlyJigsawErrorsMade = new SortedDictionary<int, int>();
        aveYearlyJigsawGamesPlayed = new SortedDictionary<int, int>();
        aveYearlyTangramMovesTaken = new SortedDictionary<int, int>();
        aveYearlyTangramErrorsMade = new SortedDictionary<int, int>();
        aveYearlyTangramGamesPlayed = new SortedDictionary<int, int>();
        aveYearlyColouringGamesPlayed = new SortedDictionary<int, int>();
    }
}

public class AllTime
{
    // General Data
    public SortedDictionary<int, float> aveTime;
    public SortedDictionary<int, float> aveMainMenuTime;
    public SortedDictionary<int, float> aveCollectionTime;
    public SortedDictionary<int, float> aveJigsawSelectTime;
    public SortedDictionary<int, float> aveTangramSelectTime;
    public SortedDictionary<int, float> aveColouringSelectTime;
    public SortedDictionary<int, float> aveInactivityPeriod;

    // Jigsaw Data
    public SortedDictionary<int, int> jigsawLevels;
    public SortedDictionary<int, float> aveJigsawTime;
    public SortedDictionary<int, int> aveJigsawMovesTaken;
    public SortedDictionary<int, int> aveJigsawErrorsMade;
    public SortedDictionary<int, int> aveJigsawGamesPlayed;

    // Tangram Data
    public SortedDictionary<int, int> tangramLevels;
    public SortedDictionary<int, float> aveTangramTime;
    public SortedDictionary<int, int> aveTangramMovesTaken;
    public SortedDictionary<int, int> aveTangramErrorsMade;
    public SortedDictionary<int, int> aveTangramGamesPlayed;
    public SortedDictionary<int, int> tangramLevels;

    // Colouring Data
    public SortedDictionary<int, float> aveColouringTime;
    public SortedDictionary<int, int> aveColouringGamesPlayed;

    public AllTime()
    {
        aveTime = new SortedDictionary<int, float>();
        aveMainMenuTime = new SortedDictionary<int, float>();
        aveCollectionTime = new SortedDictionary<int, float>();
        aveJigsawSelectTime = new SortedDictionary<int, float>();
        aveTangramSelectTime = new SortedDictionary<int, float>();
        aveColouringSelectTime = new SortedDictionary<int, float>();
        aveInactivityPeriod = new SortedDictionary<int, float>();
        aveJigsawTime = new SortedDictionary<int, float>();
        aveTangramTime = new SortedDictionary<int, float>();
        aveColouringTime = new SortedDictionary<int, float>();

        aveJigsawMovesTaken = new SortedDictionary<int, int>();
        aveJigsawErrorsMade = new SortedDictionary<int, int>();
        aveJigsawGamesPlayed = new SortedDictionary<int, int>();
        aveTangramMovesTaken = new SortedDictionary<int, int>();
        aveTangramErrorsMade = new SortedDictionary<int, int>();
        aveTangramGamesPlayed = new SortedDictionary<int, int>();
        aveColouringGamesPlayed = new SortedDictionary<int, int>();

        jigsawLevels = new SortedDictionary<int, int>();
        tangramLevels = new SortedDictionary<int, int>();
    }
}

public class GameData : MonoBehaviour
{
    public Daily daily;
    public Monthly monthly;
    public Yearly yearly;
    public AllTime allTime;

    #region Current Data
    // General Data - Current
    public DateTime date;
    public TimeSpan timespan;
    public GAME_TYPES currentGame;
    public float time;
    public float mainMenuTime;
    public float collectionTime;
    public float jigsawSelectTime;
    public float tangramSelectTime;
    public float colouringSelectTime;
    public List<float> inactivity;
    public bool isInactive;
    public float inactivityPeriod;
    public float minInactivityPeriod;
    public TimeSpan timeStarted;

    // Jigsaw Data - Current
    public List<float> jigsawTime;
    public List<int> jigsawMovesTaken;
    public List<int> jigsawErrorsMade;
    public int jigsawGamesPlayed;

    // Tangram Data - Current
    public List<float> tangramTime;
    public List<int> tangramMovesTaken;
    public List<int> tangramErrorsMade;
    public int tangramGamesPlayed;

    // Colouring Data - Current
    public List<float> colouringTime;
    public int colouringGamesPlayed;
    #endregion

    private void Awake()
    {
        date = DateTime.Today.Date;
        timespan = timeStarted = DateTime.Now.TimeOfDay;
        currentGame = GAME_TYPES.NONE_GAME;
        time = mainMenuTime = collectionTime = jigsawSelectTime = tangramSelectTime = colouringSelectTime = inactivityPeriod = 0;
        minInactivityPeriod = 10;
        isInactive = true;
        jigsawGamesPlayed = tangramGamesPlayed = colouringGamesPlayed = 0;
        inactivity = jigsawTime = tangramTime = colouringTime = new List<float>();
        jigsawMovesTaken = jigsawErrorsMade = tangramMovesTaken = tangramErrorsMade = new List<int>();

        daily = new Daily();
        monthly = new Monthly();
        yearly = new Yearly();
        allTime = new AllTime();
    }

    private void Update()
    {
        time += Time.deltaTime;

        switch (SceneManager.GetActiveScene().name)
        {
            case "Game Select Scene":
                mainMenuTime += Time.deltaTime;
                break;
            case "Collection":
                collectionTime += Time.deltaTime;
                break;
            case "Jigsaw":
                if (jigsawTime.Contains(jigsawGamesPlayed - 1)) jigsawTime[jigsawGamesPlayed - 1] += Time.deltaTime;
                break;
            case "Tangram":
                if (tangramTime.Contains(tangramGamesPlayed - 1)) tangramTime[tangramGamesPlayed - 1] += Time.deltaTime;
                break;
            case "Colouring":
                if (colouringTime.Contains(colouringGamesPlayed - 1)) colouringTime[colouringGamesPlayed - 1] += Time.deltaTime;
                break;
            case "Jigsaw Level Select":
                jigsawSelectTime += Time.deltaTime;
                break;
            case "Tangram Level Select":
                tangramSelectTime += Time.deltaTime;
                break;
            case "Colouring Level Select":
                colouringSelectTime += Time.deltaTime;
                break;
        }

        if (isInactive) inactivityPeriod += Time.deltaTime;

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    CheckInactivity();
                    break;
                case TouchPhase.Ended:
                    isInactive = true;
                    break;
            }
        }
#else
        if (Input.GetMouseButtonDown(0)) CheckInactivity();
        else if (Input.GetMouseButtonUp(0)) isInactive = true;
#endif
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
    
    public void AddData(SortedDictionary<TimeSpan, int> dict, TimeSpan key, int value)
    {
        if (dict == null) dict = new SortedDictionary<TimeSpan, int>();
        if (dict.ContainsKey(key)) dict[key] = value;
        else dict.Add(key, value);
    }
    public void AddData(SortedDictionary<TimeSpan, float> dict, TimeSpan key, float value)
    {
        if (dict == null) dict = new SortedDictionary<TimeSpan, float>();
        if (dict.ContainsKey(key)) dict[key] = value;
        else dict.Add(key, value);
    }
    public void AddData(SortedDictionary<int, int> dict, int key, int value)
    {
        if (dict == null) dict = new SortedDictionary<int, int>();
        if (dict.ContainsKey(key)) dict[key] = value;
        else dict.Add(key, value);
    }
    public void AddData(SortedDictionary<int, float> dict, int key, float value)
    {
        if (dict == null) dict = new SortedDictionary<int, float>();
        if (dict.ContainsKey(key)) dict[key] = value;
        else dict.Add(key, value);
    }

    public void LogDailyData()
    {
        CheckInactivity();
        DateTime now = DateTime.Now;
        // General Data
        if (time > 0) AddData(daily.aveDailyTime, now.TimeOfDay, time);
        if (mainMenuTime > 0) AddData(daily.aveDailyMainMenuTime, now.TimeOfDay, mainMenuTime);
        if (collectionTime > 0) AddData(daily.aveDailyCollectionTime, now.TimeOfDay, collectionTime);
        if (jigsawSelectTime > 0) AddData(daily.aveDailyJigsawSelectTime, now.TimeOfDay, jigsawSelectTime);
        if (tangramSelectTime > 0) AddData(daily.aveDailyTangramSelectTime, now.TimeOfDay, tangramSelectTime);
        if (colouringSelectTime > 0) AddData(daily.aveDailyColouringSelectTime, now.TimeOfDay, colouringSelectTime);
        float aveGameTime = 0;
        if (inactivity.Count > 0)
        {
            foreach (float timespan in inactivity) aveGameTime += timespan;
            aveGameTime /= inactivity.Count;
        }
        if (aveGameTime > 0) AddData(daily.aveDailyInactivityPeriod, now.TimeOfDay, aveGameTime);

        // Jigsaw Data
        aveGameTime = 0;
        if (jigsawTime.Count > 0)
        {
            foreach (float timespan in jigsawTime) aveGameTime += timespan;
            aveGameTime /= jigsawTime.Count;
        }
        if (aveGameTime > 0) AddData(daily.aveDailyJigsawTime, now.TimeOfDay, aveGameTime);
        int aveGameCount = 0;
        if (jigsawMovesTaken.Count > 0)
        {
            foreach (int count in jigsawMovesTaken) aveGameCount += count;
            aveGameCount /= jigsawMovesTaken.Count;
        }
        if (aveGameCount > 0) AddData(daily.aveDailyJigsawMovesTaken, now.TimeOfDay, aveGameCount);
        aveGameCount = 0;
        if (jigsawErrorsMade.Count > 0)
        {
            foreach (int count in jigsawErrorsMade) aveGameCount += count;
            aveGameCount /= jigsawErrorsMade.Count;
        }
        if (aveGameCount > 0) AddData(daily.aveDailyJigsawErrorsMade, now.TimeOfDay, aveGameCount);
        if (jigsawGamesPlayed > 0) AddData(daily.aveDailyJigsawGamesPlayed, now.TimeOfDay, jigsawGamesPlayed);

        // Tangram Data
        aveGameTime = 0;
        if (tangramTime.Count > 0)
        {
            foreach (float timespan in tangramTime) aveGameTime += timespan;
            aveGameTime /= tangramTime.Count;
        }
        if (aveGameTime > 0) AddData(daily.aveDailyTangramTime, now.TimeOfDay, aveGameTime);
        aveGameCount = 0;
        if (tangramMovesTaken.Count > 0)
        {
            foreach (int count in tangramMovesTaken) aveGameCount += count;
            aveGameCount /= tangramMovesTaken.Count;
        }
        if (aveGameCount > 0) AddData(daily.aveDailyTangramMovesTaken, now.TimeOfDay, aveGameCount);
        aveGameCount = 0;
        if (tangramErrorsMade.Count > 0)
        {
            foreach (int count in tangramErrorsMade) aveGameCount += count;
            aveGameCount /= tangramErrorsMade.Count;
        }
        if (aveGameCount > 0) AddData(daily.aveDailyTangramErrorsMade, now.TimeOfDay, aveGameCount);
        if (tangramGamesPlayed > 0) AddData(daily.aveDailyTangramGamesPlayed, now.TimeOfDay, tangramGamesPlayed);

        // Colouring Data
        aveGameTime = 0;
        if (colouringTime.Count > 0)
        {
            foreach (float timespan in colouringTime) aveGameTime += timespan;
            aveGameTime /= colouringTime.Count;
        }
        if (aveGameTime > 0) AddData(daily.aveDailyColouringTime, now.TimeOfDay, aveGameTime);
        if (colouringGamesPlayed > 0) AddData(daily.aveDailyColouringGamesPlayed, now.TimeOfDay, colouringGamesPlayed);
    }
    public void LogMonthlyData()
    {
        // General Data
        float aveGameTime = 0;
        if (daily.aveDailyTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyTime.Values) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyTime.Count;
        }
        if (aveGameTime > 0) AddData(monthly.aveMonthlyTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyMainMenuTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyMainMenuTime.Values) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyMainMenuTime.Count;
        }
        if (aveGameTime > 0) AddData(monthly.aveMonthlyMainMenuTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyCollectionTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyCollectionTime.Values) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyCollectionTime.Count;
        }
        if (aveGameTime > 0) AddData(monthly.aveMonthlyCollectionTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyJigsawSelectTime.Values) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyJigsawSelectTime.Count;
        }
        if (aveGameTime > 0) AddData(monthly.aveMonthlyJigsawSelectTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyTangramSelectTime.Values) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyTangramSelectTime.Count;
        }
        if (aveGameTime > 0) AddData(monthly.aveMonthlyTangramSelectTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyColouringSelectTime.Values) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyColouringSelectTime.Count;
        }
        if (aveGameTime > 0) AddData(monthly.aveMonthlyColouringSelectTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in daily.aveDailyInactivityPeriod.Values) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyInactivityPeriod.Count;
        }
        if (aveGameTime > 0) AddData(monthly.aveMonthlyInactivityPeriod, date.Month, aveGameTime);

        // Jigsaw Data
        aveGameTime = 0;
        if (daily.aveDailyJigsawTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyJigsawTime.Values) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyJigsawTime.Count;
        }
        if (aveGameTime > 0) AddData(monthly.aveMonthlyJigsawTime, date.Month, aveGameTime);
        int aveGameCount = 0;
        if (daily.aveDailyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in daily.aveDailyJigsawMovesTaken.Values) aveGameCount += count;
            aveGameCount /= daily.aveDailyJigsawMovesTaken.Count;
        }
        if (aveGameCount > 0) AddData(monthly.aveMonthlyJigsawMovesTaken, date.Month, aveGameCount);
        aveGameCount = 0;
        if (daily.aveDailyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in daily.aveDailyJigsawErrorsMade.Values) aveGameCount += count;
            aveGameCount /= daily.aveDailyJigsawErrorsMade.Count;
        }
        if (aveGameCount > 0) AddData(monthly.aveMonthlyJigsawErrorsMade, date.Month, aveGameCount);
        aveGameCount = 0;
        if (daily.aveDailyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in daily.aveDailyJigsawGamesPlayed.Values) aveGameCount += count;
            aveGameCount /= daily.aveDailyJigsawGamesPlayed.Count;
        }
        if (jigsawGamesPlayed > 0) AddData(monthly.aveMonthlyJigsawGamesPlayed, date.Month, jigsawGamesPlayed);

        // Tangram Data
        aveGameTime = 0;
        if (daily.aveDailyTangramTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyTangramTime.Values) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyTangramTime.Count;
        }
        if (aveGameTime > 0) AddData(monthly.aveMonthlyTangramTime, date.Month, aveGameTime);
        aveGameCount = 0;
        if (daily.aveDailyTangramMovesTaken.Count > 0)
        {
            foreach (int count in daily.aveDailyTangramMovesTaken.Values) aveGameCount += count;
            aveGameCount /= daily.aveDailyTangramMovesTaken.Count;
        }
        if (aveGameCount > 0) AddData(monthly.aveMonthlyTangramMovesTaken, date.Month, aveGameCount);
        aveGameCount = 0;
        if (daily.aveDailyTangramErrorsMade.Count > 0)
        {
            foreach (int count in daily.aveDailyTangramErrorsMade.Values) aveGameCount += count;
            aveGameCount /= daily.aveDailyTangramErrorsMade.Count;
        }
        if (aveGameCount > 0) AddData(monthly.aveMonthlyTangramErrorsMade, date.Month, aveGameCount);
        aveGameCount = 0;
        if (daily.aveDailyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in daily.aveDailyTangramGamesPlayed.Values) aveGameCount += count;
            aveGameCount /= daily.aveDailyTangramGamesPlayed.Count;
        }
        if (tangramGamesPlayed > 0) AddData(monthly.aveMonthlyTangramGamesPlayed, date.Month, tangramGamesPlayed);

        // Colouring Data
        aveGameTime = 0;
        if (daily.aveDailyColouringTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyColouringTime.Values) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyColouringTime.Count;
        }
        if (aveGameTime > 0) AddData(monthly.aveMonthlyColouringTime, date.Month, aveGameTime);
        aveGameCount = 0;
        if (daily.aveDailyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in daily.aveDailyColouringGamesPlayed.Values) aveGameCount += count;
            aveGameCount /= daily.aveDailyColouringGamesPlayed.Count;
        }
        if (colouringGamesPlayed > 0) AddData(monthly.aveMonthlyColouringGamesPlayed, date.Month, colouringGamesPlayed);
    }
    public void LogYearlyData()
    {
        // General Data
        float aveGameTime = 0;
        if (monthly.aveMonthlyTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyTime.Values) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyTime.Count;
        }
        if (aveGameTime > 0) AddData(yearly.aveYearlyTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyMainMenuTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyMainMenuTime.Values) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyMainMenuTime.Count;
        }
        if (aveGameTime > 0) AddData(yearly.aveYearlyMainMenuTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyCollectionTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyCollectionTime.Values) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyCollectionTime.Count;
        }
        if (aveGameTime > 0) AddData(yearly.aveYearlyCollectionTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyJigsawSelectTime.Values) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyJigsawSelectTime.Count;
        }
        if (aveGameTime > 0) AddData(yearly.aveYearlyJigsawSelectTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyTangramSelectTime.Values) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyTangramSelectTime.Count;
        }
        if (aveGameTime > 0) AddData(yearly.aveYearlyTangramSelectTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyColouringSelectTime.Values) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyColouringSelectTime.Count;
        }
        if (aveGameTime > 0) AddData(yearly.aveYearlyColouringSelectTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyInactivityPeriod.Values) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyInactivityPeriod.Count;
        }
        if (aveGameTime > 0) AddData(yearly.aveYearlyInactivityPeriod, date.Month, aveGameTime);

        // Jigsaw Data
        aveGameTime = 0;
        if (monthly.aveMonthlyJigsawTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyJigsawTime.Values) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyJigsawTime.Count;
        }
        if (aveGameTime > 0) AddData(yearly.aveYearlyJigsawTime, date.Month, aveGameTime);
        int aveGameCount = 0;
        if (monthly.aveMonthlyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyJigsawMovesTaken.Values) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyJigsawMovesTaken.Count;
        }
        if (aveGameCount > 0) AddData(yearly.aveYearlyJigsawMovesTaken, date.Month, aveGameCount);
        aveGameCount = 0;
        if (monthly.aveMonthlyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyJigsawErrorsMade.Values) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyJigsawErrorsMade.Count;
        }
        if (aveGameCount > 0) AddData(yearly.aveYearlyJigsawErrorsMade, date.Month, aveGameCount);
        aveGameCount = 0;
        if (monthly.aveMonthlyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyJigsawGamesPlayed.Values) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyJigsawGamesPlayed.Count;
        }
        if (jigsawGamesPlayed > 0) AddData(yearly.aveYearlyJigsawGamesPlayed, date.Month, jigsawGamesPlayed);

        // Tangram Data
        aveGameTime = 0;
        if (monthly.aveMonthlyTangramTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyTangramTime.Values) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyTangramTime.Count;
        }
        if (aveGameTime > 0) AddData(yearly.aveYearlyTangramTime, date.Month, aveGameTime);
        aveGameCount = 0;
        if (monthly.aveMonthlyTangramMovesTaken.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyTangramMovesTaken.Values) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyTangramMovesTaken.Count;
        }
        if (aveGameCount > 0) AddData(yearly.aveYearlyTangramMovesTaken, date.Month, aveGameCount);
        aveGameCount = 0;
        if (monthly.aveMonthlyTangramErrorsMade.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyTangramErrorsMade.Values) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyTangramErrorsMade.Count;
        }
        if (aveGameCount > 0) AddData(yearly.aveYearlyTangramErrorsMade, date.Month, aveGameCount);
        aveGameCount = 0;
        if (monthly.aveMonthlyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyTangramGamesPlayed.Values) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyTangramGamesPlayed.Count;
        }
        if (tangramGamesPlayed > 0) AddData(yearly.aveYearlyTangramGamesPlayed, date.Month, tangramGamesPlayed);

        // Colouring Data
        aveGameTime = 0;
        if (monthly.aveMonthlyColouringTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyColouringTime.Values) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyColouringTime.Count;
        }
        if (aveGameTime > 0) AddData(yearly.aveYearlyColouringTime, date.Month, aveGameTime);
        aveGameCount = 0;
        if (monthly.aveMonthlyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyColouringGamesPlayed.Values) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyColouringGamesPlayed.Count;
        }
        if (colouringGamesPlayed > 0) AddData(yearly.aveYearlyColouringGamesPlayed, date.Month, colouringGamesPlayed);
    }
    public void LogAllTimeData()
    {
        // General Data
        float aveGameTime = 0;
        if (yearly.aveYearlyTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyTime.Values) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyTime.Count;
        }
        if (aveGameTime > 0) AddData(allTime.aveTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyMainMenuTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyMainMenuTime.Values) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyMainMenuTime.Count;
        }
        if (aveGameTime > 0) AddData(allTime.aveMainMenuTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyCollectionTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyCollectionTime.Values) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyCollectionTime.Count;
        }
        if (aveGameTime > 0) AddData(allTime.aveCollectionTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyJigsawSelectTime.Values) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyJigsawSelectTime.Count;
        }
        if (aveGameTime > 0) AddData(allTime.aveJigsawSelectTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyTangramSelectTime.Values) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyTangramSelectTime.Count;
        }
        if (aveGameTime > 0) AddData(allTime.aveTangramSelectTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyColouringSelectTime.Values) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyColouringSelectTime.Count;
        }
        if (aveGameTime > 0) AddData(allTime.aveColouringSelectTime, date.Month, aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyInactivityPeriod.Values) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyInactivityPeriod.Count;
        }
        if (aveGameTime > 0) AddData(allTime.aveInactivityPeriod, date.Month, aveGameTime);

        // Jigsaw Data
        aveGameTime = 0;
        if (yearly.aveYearlyJigsawTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyJigsawTime.Values) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyJigsawTime.Count;
        }
        if (aveGameTime > 0) AddData(allTime.aveJigsawTime, date.Month, aveGameTime);
        int aveGameCount = 0;
        if (yearly.aveYearlyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in yearly.aveYearlyJigsawMovesTaken.Values) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyJigsawMovesTaken.Count;
        }
        if (aveGameCount > 0) AddData(allTime.aveJigsawMovesTaken, date.Month, aveGameCount);
        aveGameCount = 0;
        if (yearly.aveYearlyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in yearly.aveYearlyJigsawErrorsMade.Values) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyJigsawErrorsMade.Count;
        }
        if (aveGameCount > 0) AddData(allTime.aveJigsawErrorsMade, date.Month, aveGameCount);
        aveGameCount = 0;
        if (yearly.aveYearlyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in yearly.aveYearlyJigsawGamesPlayed.Values) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyJigsawGamesPlayed.Count;
        }
        if (jigsawGamesPlayed > 0) AddData(allTime.aveJigsawGamesPlayed, date.Month, jigsawGamesPlayed);

        // Tangram Data
        aveGameTime = 0;
        if (yearly.aveYearlyTangramTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyTangramTime.Values) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyTangramTime.Count;
        }
        if (aveGameTime > 0) AddData(allTime.aveTangramTime, date.Month, aveGameTime);
        aveGameCount = 0;
        if (yearly.aveYearlyTangramMovesTaken.Count > 0)
        {
            foreach (int count in yearly.aveYearlyTangramMovesTaken.Values) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyTangramMovesTaken.Count;
        }
        if (aveGameCount > 0) AddData(allTime.aveTangramMovesTaken, date.Month, aveGameCount);
        aveGameCount = 0;
        if (yearly.aveYearlyTangramErrorsMade.Count > 0)
        {
            foreach (int count in yearly.aveYearlyTangramErrorsMade.Values) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyTangramErrorsMade.Count;
        }
        if (aveGameCount > 0) AddData(allTime.aveTangramErrorsMade, date.Month, aveGameCount);
        aveGameCount = 0;
        if (yearly.aveYearlyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in yearly.aveYearlyTangramGamesPlayed.Values) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyTangramGamesPlayed.Count;
        }
        if (tangramGamesPlayed > 0) AddData(allTime.aveTangramGamesPlayed, date.Month, tangramGamesPlayed);

        // Colouring Data
        aveGameTime = 0;
        if (yearly.aveYearlyColouringTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyColouringTime.Values) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyColouringTime.Count;
        }
        if (aveGameTime > 0) AddData(allTime.aveColouringTime, date.Month, aveGameTime);
        aveGameCount = 0;
        if (yearly.aveYearlyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in yearly.aveYearlyColouringGamesPlayed.Values) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyColouringGamesPlayed.Count;
        }
        if (colouringGamesPlayed > 0) AddData(allTime.aveColouringGamesPlayed, date.Month, colouringGamesPlayed);
    }

    public void TangramStars(int i)
    {
        if(!allTime.tangramLevels.ContainsKey(i))
        {
            allTime.tangramLevels.Add(i, 0);
        }
        if (tangramTime[tangramTime.Count - 1] <= 60) allTime.tangramLevels[i] = 3;
        else if (tangramTime[tangramTime.Count - 1] <= 120) allTime.tangramLevels[i] = 2;
        else allTime.tangramLevels[i] = 1;
    }

    public void LogData()
    {
        LogDailyData();
        LogMonthlyData();
        LogYearlyData();
        LogAllTimeData();
    }
}
