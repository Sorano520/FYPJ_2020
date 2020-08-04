using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    #region General Data
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

    // General Data - Day
    public SortedDictionary<DateTime, float> aveDailyTime;
    public SortedDictionary<DateTime, float> aveDailyMainMenuTime;
    public SortedDictionary<DateTime, float> aveDailyCollectionTime;
    public SortedDictionary<DateTime, float> aveDailyJigsawSelectTime;
    public SortedDictionary<DateTime, float> aveDailyTangramSelectTime;
    public SortedDictionary<DateTime, float> aveDailyColouringSelectTime;
    public SortedDictionary<DateTime, float> aveDailyInactivityPeriod;

    // General Data - Month
    public SortedDictionary<DateTime, float> aveMonthlyTime;
    public SortedDictionary<DateTime, float> aveMonthlyMainMenuTime;
    public SortedDictionary<DateTime, float> aveMonthlyCollectionTime;
    public SortedDictionary<DateTime, float> aveMonthlyJigsawSelectTime;
    public SortedDictionary<DateTime, float> aveMonthlyTangramSelectTime;
    public SortedDictionary<DateTime, float> aveMonthlyColouringSelectTime;
    public SortedDictionary<DateTime, float> aveMonthlyInactivityPeriod;

    // General Data - Year
    public SortedDictionary<DateTime, float> aveYearlyTime;
    public SortedDictionary<DateTime, float> aveYearlyMainMenuTime;
    public SortedDictionary<DateTime, float> aveYearlyCollectionTime;
    public SortedDictionary<DateTime, float> aveYearlyJigsawSelectTime;
    public SortedDictionary<DateTime, float> aveYearlyTangramSelectTime;
    public SortedDictionary<DateTime, float> aveYearlyColouringSelectTime;
    public SortedDictionary<DateTime, float> aveYearlyInactivityPeriod;

    // General Data - All Time
    public SortedDictionary<DateTime, float> aveTime;
    public SortedDictionary<DateTime, float> aveMainMenuTime;
    public SortedDictionary<DateTime, float> aveCollectionTime;
    public SortedDictionary<DateTime, float> aveJigsawSelectTime;
    public SortedDictionary<DateTime, float> aveTangramSelectTime;
    public SortedDictionary<DateTime, float> aveColouringSelectTime;
    public SortedDictionary<DateTime, float> aveInactivityPeriod;
    #endregion

    #region Jigsaw Data
    // Jigsaw Data - Current
    public List<float> jigsawTime;
    public List<int> jigsawMovesTaken;
    public List<int> jigsawErrorsMade;
    public int jigsawGamesPlayed;

    // Jigsaw Data - Day
    public SortedDictionary<DateTime, float> aveDailyJigsawTime;
    public SortedDictionary<DateTime, int> aveDailyJigsawMovesTaken;
    public SortedDictionary<DateTime, int> aveDailyJigsawErrorsMade;
    public SortedDictionary<DateTime, int> aveDailyJigsawGamesPlayed;

    // Jigsaw Data - Month
    public SortedDictionary<DateTime, float> aveMonthlyJigsawTime;
    public SortedDictionary<DateTime, int> aveMonthlyJigsawMovesTaken;
    public SortedDictionary<DateTime, int> aveMonthlyJigsawErrorsMade;
    public SortedDictionary<DateTime, int> aveMonthlyJigsawGamesPlayed;

    // Jigsaw Data - Year
    public SortedDictionary<DateTime, float> aveYearlyJigsawTime;
    public SortedDictionary<DateTime, int> aveYearlyJigsawMovesTaken;
    public SortedDictionary<DateTime, int> aveYearlyJigsawErrorsMade;
    public SortedDictionary<DateTime, int> aveYearlyJigsawGamesPlayed;

    // Jigsaw Data - All Time
    public SortedDictionary<DateTime, float> aveJigsawTime;
    public SortedDictionary<DateTime, int> aveJigsawMovesTaken;
    public SortedDictionary<DateTime, int> aveJigsawErrorsMade;
    public SortedDictionary<DateTime, int> aveJigsawGamesPlayed;
    #endregion

    #region Tangram Data
    // Tangram Data - Current
    public List<float> tangramTime;
    public List<int> tangramMovesTaken;
    public List<int> tangramErrorsMade;
    public int tangramGamesPlayed;

    // Tangram Data - Day
    public SortedDictionary<DateTime, float> aveDailyTangramTime;
    public SortedDictionary<DateTime, int> aveDailyTangramMovesTaken;
    public SortedDictionary<DateTime, int> avevTangramErrorsMade;
    public SortedDictionary<DateTime, int> aveDailyTangramGamesPlayed;

    // Tangram Data - Month
    public SortedDictionary<DateTime, float> aveMonthlyTangramTime;
    public SortedDictionary<DateTime, int> aveMonthlyTangramMovesTaken;
    public SortedDictionary<DateTime, int> aveMonthlyTangramErrorsMade;
    public SortedDictionary<DateTime, int> aveMonthlyTangramGamesPlayed;

    // Tangram Data - Year
    public SortedDictionary<DateTime, float> aveYearlyTangramTime;
    public SortedDictionary<DateTime, int> aveYearlyTangramMovesTaken;
    public SortedDictionary<DateTime, int> aveYearlyTangramErrorsMade;
    public SortedDictionary<DateTime, int> aveYearlyTangramGamesPlayed;

    // Tangram Data - All Time
    public SortedDictionary<DateTime, float> aveTangramTime;
    public SortedDictionary<DateTime, int> aveTangramMovesTaken;
    public SortedDictionary<DateTime, int> aveTangramErrorsMade;
    public SortedDictionary<DateTime, int> aveTangramGamesPlayed;
    #endregion

    #region Colouring Data
    // Colouring Data - Current
    public List<float> colouringTime;
    public int colouringGamesPlayed;

    // Colouring Data - Day
    public SortedDictionary<DateTime, float> aveDailyColouringTime;
    public SortedDictionary<DateTime, int> aveDailyColouringGamesPlayed;

    // Colouring Data - Month
    public SortedDictionary<DateTime, float> aveMonthlyColouringTime;
    public SortedDictionary<DateTime, int> aveMonthlyColouringGamesPlayed;

    // Colouring Data - Year
    public SortedDictionary<DateTime, float> aveYearlyColouringTime;
    public SortedDictionary<DateTime, int> aveYearlyColouringGamesPlayed;

    // Colouring Data - All Time
    public SortedDictionary<DateTime, float> aveColouringTime;
    public SortedDictionary<DateTime, int> aveColouringGamesPlayed;
    #endregion

    private void Start()
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

        aveDailyTime =
        aveDailyMainMenuTime =
        aveDailyCollectionTime =
        aveDailyJigsawSelectTime =
        aveDailyTangramSelectTime =
        aveDailyColouringSelectTime =
        aveDailyInactivityPeriod =
        aveMonthlyTime =
        aveMonthlyMainMenuTime =
        aveMonthlyCollectionTime =
        aveMonthlyJigsawSelectTime =
        aveMonthlyTangramSelectTime =
        aveMonthlyColouringSelectTime =
        aveMonthlyInactivityPeriod =
        aveYearlyTime =
        aveYearlyMainMenuTime =
        aveYearlyCollectionTime =
        aveYearlyJigsawSelectTime =
        aveYearlyTangramSelectTime =
        aveYearlyColouringSelectTime =
        aveYearlyInactivityPeriod =
        aveTime =
        aveMainMenuTime =
        aveCollectionTime =
        aveJigsawSelectTime =
        aveTangramSelectTime =
        aveColouringSelectTime =
        aveInactivityPeriod =
        aveDailyJigsawTime =
        aveMonthlyJigsawTime =
        aveYearlyJigsawTime =
        aveJigsawTime =
        aveDailyTangramTime =
        aveMonthlyTangramTime =
        aveYearlyTangramTime =
        aveTangramTime =
        aveDailyColouringTime =
        aveMonthlyColouringTime =
        aveYearlyColouringTime =
        aveColouringTime =
        new SortedDictionary<DateTime, float>();

        aveDailyJigsawMovesTaken =
        aveDailyJigsawErrorsMade =
        aveDailyJigsawGamesPlayed =
        aveMonthlyJigsawMovesTaken =
        aveMonthlyJigsawErrorsMade =
        aveMonthlyJigsawGamesPlayed =
        aveYearlyJigsawMovesTaken =
        aveYearlyJigsawErrorsMade =
        aveYearlyJigsawGamesPlayed =
        aveJigsawMovesTaken =
        aveJigsawErrorsMade =
        aveJigsawGamesPlayed =
        aveDailyTangramMovesTaken =
        avevTangramErrorsMade =
        aveDailyTangramGamesPlayed =
        aveMonthlyTangramMovesTaken =
        aveMonthlyTangramErrorsMade =
        aveMonthlyTangramGamesPlayed =
        aveYearlyTangramMovesTaken =
        aveYearlyTangramErrorsMade =
        aveYearlyTangramGamesPlayed =
        aveTangramMovesTaken =
        aveTangramErrorsMade =
        aveTangramGamesPlayed =
        aveDailyColouringGamesPlayed =
        aveMonthlyColouringGamesPlayed =
        aveYearlyColouringGamesPlayed =
        aveColouringGamesPlayed =
        new SortedDictionary<DateTime, int>();
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
                jigsawTime[jigsawGamesPlayed - 1] += Time.deltaTime;
                break;
            case "Tangram":
                tangramTime[tangramGamesPlayed - 1] += Time.deltaTime;
                break;
            case "Colouring":
                colouringTime[colouringGamesPlayed - 1] += Time.deltaTime;
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
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    CheckInactivity();
                    break;
                case TouchPhase.Ended:
                    isInactive = true;
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

    public void LogData()
    {
        CheckInactivity();
        DateTime now = DateTime.Now;
        // General Data
        if (time > 0) aveDailyTime.Add(now, time);
        if (mainMenuTime > 0) aveDailyMainMenuTime.Add(now, mainMenuTime);
        if (collectionTime > 0) aveDailyCollectionTime.Add(now, collectionTime);
        if (jigsawSelectTime > 0) aveDailyJigsawSelectTime.Add(now, jigsawSelectTime);
        if (tangramSelectTime > 0) aveDailyTangramSelectTime.Add(now, tangramSelectTime);
        if (colouringSelectTime > 0) aveDailyColouringSelectTime.Add(now, colouringSelectTime);
        float aveGameTime = 0;
        foreach (float timespan in inactivity) aveGameTime += timespan;
        aveGameTime /= inactivity.Count;
        if (aveGameTime > 0) aveDailyInactivityPeriod.Add(now, aveGameTime);

        // Jigsaw Data
        aveGameTime = 0;
        foreach (float timespan in jigsawTime) aveGameTime += timespan;
        aveGameTime /= jigsawTime.Count;
        if (aveGameTime > 0) aveDailyJigsawTime.Add(now, aveGameTime);
        int aveGameCount = 0;
        foreach (int count in jigsawMovesTaken) aveGameCount += count;
        aveGameCount /= jigsawMovesTaken.Count;
        if (aveGameCount > 0) aveDailyJigsawMovesTaken.Add(now, aveGameCount);
        aveGameCount = 0;
        foreach (int count in jigsawErrorsMade) aveGameCount += count;
        aveGameCount /= jigsawErrorsMade.Count;
        if (aveGameCount > 0) aveDailyJigsawErrorsMade.Add(now, aveGameCount);
        if (jigsawGamesPlayed > 0) aveDailyJigsawGamesPlayed.Add(now, jigsawGamesPlayed);

        // Tangram Data
        aveGameTime = 0;
        foreach (float timespan in tangramTime) aveGameTime += timespan;
        aveGameTime /= tangramTime.Count;
        if (aveGameTime > 0) aveDailyTangramTime.Add(now, aveGameTime);
        aveGameCount = 0;
        foreach (int count in tangramMovesTaken) aveGameCount += count;
        aveGameCount /= tangramMovesTaken.Count;
        if (aveGameCount > 0) aveDailyTangramMovesTaken.Add(now, aveGameCount);
        aveGameCount = 0;
        foreach (int count in tangramErrorsMade) aveGameCount += count;
        aveGameCount /= tangramErrorsMade.Count;
        if (aveGameCount > 0) avevTangramErrorsMade.Add(now, aveGameCount);
        if (tangramGamesPlayed > 0) aveDailyTangramGamesPlayed.Add(now, tangramGamesPlayed);

        // Colouring Data
        aveGameTime = 0;
        foreach (float timespan in colouringTime) aveGameTime += timespan;
        aveGameTime /= colouringTime.Count;
        if (aveGameTime > 0) aveDailyColouringTime.Add(now, aveGameTime);
        if (colouringGamesPlayed > 0) aveDailyColouringGamesPlayed.Add(now, colouringGamesPlayed);
    }
}
