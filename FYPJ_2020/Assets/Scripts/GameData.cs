using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Daily
{
    // General Data
    public List<float> aveDailyTime;
    public List<float> aveDailyMainMenuTime;
    public List<float> aveDailyCollectionTime;
    public List<float> aveDailyJigsawSelectTime;
    public List<float> aveDailyTangramSelectTime;
    public List<float> aveDailyColouringSelectTime;
    public List<float> aveDailyInactivityPeriod;

    // Jigsaw Data
    public List<float> aveDailyJigsawTime;
    public List<int> aveDailyJigsawMovesTaken;
    public List<int> aveDailyJigsawErrorsMade;
    public List<int> aveDailyJigsawGamesPlayed;

    // Tangram Data
    public List<float> aveDailyTangramTime;
    public List<int> aveDailyTangramMovesTaken;
    public List<int> aveDailyTangramGamesPlayed;

    // Colouring Data
    public List<float> aveDailyColouringTime;
    public List<int> aveDailyColouringGamesPlayed;

    public Daily()
    {
        aveDailyTime = new List<float>();
        aveDailyMainMenuTime = new List<float>();
        aveDailyCollectionTime = new List<float>();
        aveDailyJigsawSelectTime = new List<float>();
        aveDailyTangramSelectTime = new List<float>();
        aveDailyColouringSelectTime = new List<float>();
        aveDailyInactivityPeriod = new List<float>();
        aveDailyJigsawTime = new List<float>();
        aveDailyTangramTime = new List<float>();
        aveDailyColouringTime = new List<float>();

        aveDailyJigsawMovesTaken = new List<int>();
        aveDailyJigsawErrorsMade = new List<int>();
        aveDailyJigsawGamesPlayed = new List<int>();
        aveDailyTangramMovesTaken = new List<int>();
        aveDailyTangramGamesPlayed = new List<int>();
        aveDailyColouringGamesPlayed = new List<int>();
    }
}

public class Monthly
{
    // General Data
    public List<float> aveMonthlyTime;
    public List<float> aveMonthlyMainMenuTime;
    public List<float> aveMonthlyCollectionTime;
    public List<float> aveMonthlyJigsawSelectTime;
    public List<float> aveMonthlyTangramSelectTime;
    public List<float> aveMonthlyColouringSelectTime;
    public List<float> aveMonthlyInactivityPeriod;

    // Jigsaw Data
    public List<float> aveMonthlyJigsawTime;
    public List<int> aveMonthlyJigsawMovesTaken;
    public List<int> aveMonthlyJigsawErrorsMade;
    public List<int> aveMonthlyJigsawGamesPlayed;

    // Tangram Data
    public List<float> aveMonthlyTangramTime;
    public List<int> aveMonthlyTangramMovesTaken;
    public List<int> aveMonthlyTangramGamesPlayed;

    // Colouring Data
    public List<float> aveMonthlyColouringTime;
    public List<int> aveMonthlyColouringGamesPlayed;

    public Monthly()
    {
        aveMonthlyTime = new List<float>();
        aveMonthlyMainMenuTime = new List<float>();
        aveMonthlyCollectionTime = new List<float>();
        aveMonthlyJigsawSelectTime = new List<float>();
        aveMonthlyTangramSelectTime = new List<float>();
        aveMonthlyColouringSelectTime = new List<float>();
        aveMonthlyInactivityPeriod = new List<float>();
        aveMonthlyJigsawTime = new List<float>();
        aveMonthlyTangramTime = new List<float>();
        aveMonthlyColouringTime = new List<float>();

        aveMonthlyJigsawMovesTaken = new List<int>();
        aveMonthlyJigsawErrorsMade = new List<int>();
        aveMonthlyJigsawGamesPlayed = new List<int>();
        aveMonthlyTangramMovesTaken = new List<int>();
        aveMonthlyTangramGamesPlayed = new List<int>();
        aveMonthlyColouringGamesPlayed = new List<int>();
    }
}

public class Yearly
{
    // General Data
    public List<float> aveYearlyTime;
    public List<float> aveYearlyMainMenuTime;
    public List<float> aveYearlyCollectionTime;
    public List<float> aveYearlyJigsawSelectTime;
    public List<float> aveYearlyTangramSelectTime;
    public List<float> aveYearlyColouringSelectTime;
    public List<float> aveYearlyInactivityPeriod;

    // Jigsaw Data
    public List<float> aveYearlyJigsawTime;
    public List<int> aveYearlyJigsawMovesTaken;
    public List<int> aveYearlyJigsawErrorsMade;
    public List<int> aveYearlyJigsawGamesPlayed;

    // Tangram Data
    public List<float> aveYearlyTangramTime;
    public List<int> aveYearlyTangramMovesTaken;
    public List<int> aveYearlyTangramGamesPlayed;

    // Colouring Data
    public List<float> aveYearlyColouringTime;
    public List<int> aveYearlyColouringGamesPlayed;

    public Yearly()
    {
        aveYearlyColouringTime = new List<float>();
        aveYearlyTime = new List<float>();
        aveYearlyMainMenuTime = new List<float>();
        aveYearlyCollectionTime = new List<float>();
        aveYearlyJigsawSelectTime = new List<float>();
        aveYearlyTangramSelectTime = new List<float>();
        aveYearlyColouringSelectTime = new List<float>();
        aveYearlyInactivityPeriod = new List<float>();
        aveYearlyJigsawTime = new List<float>();
        aveYearlyTangramTime = new List<float>();

        aveYearlyJigsawMovesTaken = new List<int>();
        aveYearlyJigsawErrorsMade = new List<int>();
        aveYearlyJigsawGamesPlayed = new List<int>();
        aveYearlyTangramMovesTaken = new List<int>();
        aveYearlyTangramGamesPlayed = new List<int>();
        aveYearlyColouringGamesPlayed = new List<int>();
    }
}

public class AllTime
{
    // General Data
    public List<float> aveTime;
    public List<float> aveMainMenuTime;
    public List<float> aveCollectionTime;
    public List<float> aveJigsawSelectTime;
    public List<float> aveTangramSelectTime;
    public List<float> aveColouringSelectTime;
    public List<float> aveInactivityPeriod;

    // Jigsaw Data
    public Dictionary<int, int> jigsawLevels;
    public List<int> jigsawLevelsKeys;
    public List<int> jigsawLevelsValues;
    public List<float> aveJigsawTime;
    public List<int> aveJigsawMovesTaken;
    public List<int> aveJigsawErrorsMade;
    public List<int> aveJigsawGamesPlayed;

    public Dictionary<int, int> tangramLevels;
    public List<int> tangramLevelsKeys;
    public List<int> tangramLevelsValues;
    public List<float> aveTangramTime;
    public List<int> aveTangramMovesTaken;
    public List<int> aveTangramGamesPlayed;

    // Colouring Data
    public List<float> aveColouringTime;
    public List<int> aveColouringGamesPlayed;

    public AllTime()
    {
        aveTime = new List<float>();
        aveMainMenuTime = new List<float>();
        aveCollectionTime = new List<float>();
        aveJigsawSelectTime = new List<float>();
        aveTangramSelectTime = new List<float>();
        aveColouringSelectTime = new List<float>();
        aveInactivityPeriod = new List<float>();
        aveJigsawTime = new List<float>();
        aveTangramTime = new List<float>();
        aveColouringTime = new List<float>();

        aveJigsawMovesTaken = new List<int>();
        aveJigsawErrorsMade = new List<int>();
        aveJigsawGamesPlayed = new List<int>();
        aveTangramMovesTaken = new List<int>();
        aveTangramGamesPlayed = new List<int>();
        aveColouringGamesPlayed = new List<int>();

        jigsawLevels = new Dictionary<int, int>();
        jigsawLevelsKeys = new List<int>();
        jigsawLevelsValues = new List<int>();
        tangramLevels = new Dictionary<int, int>();
        tangramLevelsKeys = new List<int>();
        tangramLevelsValues = new List<int>();
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
    public bool inGame;
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
    public int tangramGamesPlayed;

    // Colouring Data - Current
    public List<float> colouringTime;
    public int colouringGamesPlayed;
    #endregion

    public void Awake()
    {
        date = DateTime.Today.Date;
        timespan = timeStarted = DateTime.Now.TimeOfDay;
        currentGame = GAME_TYPES.NONE_GAME;
        inGame = false;
        time = mainMenuTime = collectionTime = jigsawSelectTime = tangramSelectTime = colouringSelectTime = inactivityPeriod = 0;
        minInactivityPeriod = 10;
        isInactive = true;
        jigsawGamesPlayed = tangramGamesPlayed = colouringGamesPlayed = 0;
        inactivity = new List<float>();
        jigsawTime = new List<float>();
        tangramTime = new List<float>();
        colouringTime = new List<float>();
        jigsawMovesTaken = new List<int>();
        jigsawErrorsMade = new List<int>();
        tangramMovesTaken = new List<int>();

        daily = new Daily();
        monthly = new Monthly();
        yearly = new Yearly();
        allTime = new AllTime();
    }

    private void Update()
    {
        if (!inGame) return;

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
                if (jigsawTime.Count > 0) jigsawTime[jigsawTime.Count - 1] += Time.deltaTime;
                break;
            case "Tangram":
                if (tangramTime.Count > 0) tangramTime[tangramTime.Count - 1] += Time.deltaTime;
                break;
            case "Colouring":
                if (colouringTime.Count > 0) colouringTime[colouringTime.Count - 1] += Time.deltaTime;
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
    
    public void CheckLevels()
    {
        allTime.jigsawLevels = new Dictionary<int, int>();
        allTime.tangramLevels = new Dictionary<int, int>();
        if (allTime.jigsawLevelsKeys.Count > 0 && allTime.jigsawLevelsKeys.Count == allTime.jigsawLevelsValues.Count)
        {
            for (int i = 0; i < allTime.jigsawLevelsKeys.Count; ++i)
            {
                allTime.jigsawLevels.Add(allTime.jigsawLevelsKeys[i], allTime.jigsawLevelsValues[i]);
            }
        }
        if (allTime.tangramLevelsKeys.Count > 0 && allTime.tangramLevelsKeys.Count == allTime.tangramLevelsValues.Count)
        {
            for (int i = 0; i < allTime.tangramLevelsKeys.Count; ++i)
            {
                allTime.tangramLevels.Add(allTime.tangramLevelsKeys[i], allTime.tangramLevelsValues[i]);
            }
        }
    }

    public void LogDailyData()
    {
        CheckInactivity();
        DateTime now = DateTime.Now;
        // General Data
        if (time > 0) daily.aveDailyTime.Add(time);
        if (mainMenuTime > 0) daily.aveDailyMainMenuTime.Add(mainMenuTime);
        if (collectionTime > 0) daily.aveDailyCollectionTime.Add(collectionTime);
        if (jigsawSelectTime > 0) daily.aveDailyJigsawSelectTime.Add(jigsawSelectTime);
        if (tangramSelectTime > 0) daily.aveDailyTangramSelectTime.Add(tangramSelectTime);
        if (colouringSelectTime > 0) daily.aveDailyColouringSelectTime.Add(colouringSelectTime);
        float aveGameTime = 0;
        if (inactivity.Count > 0)
        {
            foreach (float timespan in inactivity) aveGameTime += timespan;
            aveGameTime /= inactivity.Count;
        }
        if (aveGameTime > 0) daily.aveDailyInactivityPeriod.Add(aveGameTime);

        // Jigsaw Data
        aveGameTime = 0;
        if (jigsawTime.Count > 0)
        {
            foreach (float timespan in jigsawTime) aveGameTime += timespan;
            aveGameTime /= jigsawTime.Count;
        }
        if (aveGameTime > 0) daily.aveDailyJigsawTime.Add(aveGameTime);
        int aveGameCount = 0;
        if (jigsawMovesTaken.Count > 0)
        {
            foreach (int count in jigsawMovesTaken) aveGameCount += count;
            aveGameCount /= jigsawMovesTaken.Count;
        }
        if (aveGameCount > 0) daily.aveDailyJigsawMovesTaken.Add(aveGameCount);
        aveGameCount = 0;
        if (jigsawErrorsMade.Count > 0)
        {
            foreach (int count in jigsawErrorsMade) aveGameCount += count;
            aveGameCount /= jigsawErrorsMade.Count;
        }
        if (aveGameCount > 0) daily.aveDailyJigsawErrorsMade.Add(aveGameCount);
        if (jigsawGamesPlayed > 0) daily.aveDailyJigsawGamesPlayed.Add(jigsawGamesPlayed);

        // Tangram Data
        aveGameTime = 0;
        if (tangramTime.Count > 0)
        {
            foreach (float timespan in tangramTime) aveGameTime += timespan;
            aveGameTime /= tangramTime.Count;
        }
        if (aveGameTime > 0) daily.aveDailyTangramTime.Add(aveGameTime);
        aveGameCount = 0;
        if (tangramMovesTaken.Count > 0)
        {
            foreach (int count in tangramMovesTaken) aveGameCount += count;
            aveGameCount /= tangramMovesTaken.Count;
        }
        if (aveGameCount > 0) daily.aveDailyTangramMovesTaken.Add(aveGameCount);
        if (tangramGamesPlayed > 0) daily.aveDailyTangramGamesPlayed.Add(tangramGamesPlayed);

        // Colouring Data
        aveGameTime = 0;
        if (colouringTime.Count > 0)
        {
            foreach (float timespan in colouringTime) aveGameTime += timespan;
            aveGameTime /= colouringTime.Count;
        }
        if (aveGameTime > 0) daily.aveDailyColouringTime.Add(aveGameTime);
        if (colouringGamesPlayed > 0) daily.aveDailyColouringGamesPlayed.Add(colouringGamesPlayed);
    }
    public void LogMonthlyData()
    {
        // General Data
        float aveGameTime = 0;
        if (daily.aveDailyTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyTime) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyTime.Count;
        }
        if (aveGameTime > 0) monthly.aveMonthlyTime.Add(aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyMainMenuTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyMainMenuTime) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyMainMenuTime.Count;
        }
        if (aveGameTime > 0) monthly.aveMonthlyMainMenuTime.Add(aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyCollectionTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyCollectionTime) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyCollectionTime.Count;
        }
        if (aveGameTime > 0) monthly.aveMonthlyCollectionTime.Add(aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyJigsawSelectTime) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyJigsawSelectTime.Count;
        }
        if (aveGameTime > 0) monthly.aveMonthlyJigsawSelectTime.Add(aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyTangramSelectTime) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyTangramSelectTime.Count;
        }
        if (aveGameTime > 0) monthly.aveMonthlyTangramSelectTime.Add(aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyColouringSelectTime) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyColouringSelectTime.Count;
        }
        if (aveGameTime > 0) monthly.aveMonthlyColouringSelectTime.Add(aveGameTime);
        aveGameTime = 0;
        if (daily.aveDailyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in daily.aveDailyInactivityPeriod) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyInactivityPeriod.Count;
        }
        if (aveGameTime > 0) monthly.aveMonthlyInactivityPeriod.Add(aveGameTime);

        // Jigsaw Data
        aveGameTime = 0;
        if (daily.aveDailyJigsawTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyJigsawTime) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyJigsawTime.Count;
        }
        if (aveGameTime > 0) monthly.aveMonthlyJigsawTime.Add(aveGameTime);
        int aveGameCount = 0;
        if (daily.aveDailyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in daily.aveDailyJigsawMovesTaken) aveGameCount += count;
            aveGameCount /= daily.aveDailyJigsawMovesTaken.Count;
        }
        if (aveGameCount > 0) monthly.aveMonthlyJigsawMovesTaken.Add(aveGameCount);
        aveGameCount = 0;
        if (daily.aveDailyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in daily.aveDailyJigsawErrorsMade) aveGameCount += count;
            aveGameCount /= daily.aveDailyJigsawErrorsMade.Count;
        }
        if (aveGameCount > 0) monthly.aveMonthlyJigsawErrorsMade.Add(aveGameCount);
        aveGameCount = 0;
        if (daily.aveDailyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in daily.aveDailyJigsawGamesPlayed) aveGameCount += count;
            aveGameCount /= daily.aveDailyJigsawGamesPlayed.Count;
        }
        if (jigsawGamesPlayed > 0) monthly.aveMonthlyJigsawGamesPlayed.Add(jigsawGamesPlayed);

        // Tangram Data
        aveGameTime = 0;
        if (daily.aveDailyTangramTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyTangramTime) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyTangramTime.Count;
        }
        if (aveGameTime > 0) monthly.aveMonthlyTangramTime.Add(aveGameTime);
        aveGameCount = 0;
        if (daily.aveDailyTangramMovesTaken.Count > 0)
        {
            foreach (int count in daily.aveDailyTangramMovesTaken) aveGameCount += count;
            aveGameCount /= daily.aveDailyTangramMovesTaken.Count;
        }
        if (aveGameCount > 0) monthly.aveMonthlyTangramMovesTaken.Add(aveGameCount);
        aveGameCount = 0;
        if (daily.aveDailyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in daily.aveDailyTangramGamesPlayed) aveGameCount += count;
            aveGameCount /= daily.aveDailyTangramGamesPlayed.Count;
        }
        if (tangramGamesPlayed > 0) monthly.aveMonthlyTangramGamesPlayed.Add(tangramGamesPlayed);

        // Colouring Data
        aveGameTime = 0;
        if (daily.aveDailyColouringTime.Count > 0)
        {
            foreach (float timespan in daily.aveDailyColouringTime) aveGameTime += timespan;
            aveGameTime /= daily.aveDailyColouringTime.Count;
        }
        if (aveGameTime > 0) monthly.aveMonthlyColouringTime.Add(aveGameTime);
        aveGameCount = 0;
        if (daily.aveDailyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in daily.aveDailyColouringGamesPlayed) aveGameCount += count;
            aveGameCount /= daily.aveDailyColouringGamesPlayed.Count;
        }
        if (colouringGamesPlayed > 0) monthly.aveMonthlyColouringGamesPlayed.Add(colouringGamesPlayed);
    }
    public void LogYearlyData()
    {
        // General Data
        float aveGameTime = 0;
        if (monthly.aveMonthlyTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyTime) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyTime.Count;
        }
        if (aveGameTime > 0) yearly.aveYearlyTime.Add(aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyMainMenuTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyMainMenuTime) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyMainMenuTime.Count;
        }
        if (aveGameTime > 0) yearly.aveYearlyMainMenuTime.Add(aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyCollectionTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyCollectionTime) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyCollectionTime.Count;
        }
        if (aveGameTime > 0) yearly.aveYearlyCollectionTime.Add(aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyJigsawSelectTime) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyJigsawSelectTime.Count;
        }
        if (aveGameTime > 0) yearly.aveYearlyJigsawSelectTime.Add(aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyTangramSelectTime) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyTangramSelectTime.Count;
        }
        if (aveGameTime > 0) yearly.aveYearlyTangramSelectTime.Add(aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyColouringSelectTime) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyColouringSelectTime.Count;
        }
        if (aveGameTime > 0) yearly.aveYearlyColouringSelectTime.Add(aveGameTime);
        aveGameTime = 0;
        if (monthly.aveMonthlyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyInactivityPeriod) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyInactivityPeriod.Count;
        }
        if (aveGameTime > 0) yearly.aveYearlyInactivityPeriod.Add(aveGameTime);

        // Jigsaw Data
        aveGameTime = 0;
        if (monthly.aveMonthlyJigsawTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyJigsawTime) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyJigsawTime.Count;
        }
        if (aveGameTime > 0) yearly.aveYearlyJigsawTime.Add(aveGameTime);
        int aveGameCount = 0;
        if (monthly.aveMonthlyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyJigsawMovesTaken) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyJigsawMovesTaken.Count;
        }
        if (aveGameCount > 0) yearly.aveYearlyJigsawMovesTaken.Add(aveGameCount);
        aveGameCount = 0;
        if (monthly.aveMonthlyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyJigsawErrorsMade) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyJigsawErrorsMade.Count;
        }
        if (aveGameCount > 0) yearly.aveYearlyJigsawErrorsMade.Add(aveGameCount);
        aveGameCount = 0;
        if (monthly.aveMonthlyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyJigsawGamesPlayed) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyJigsawGamesPlayed.Count;
        }
        if (jigsawGamesPlayed > 0) yearly.aveYearlyJigsawGamesPlayed.Add(jigsawGamesPlayed);

        // Tangram Data
        aveGameTime = 0;
        if (monthly.aveMonthlyTangramTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyTangramTime) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyTangramTime.Count;
        }
        if (aveGameTime > 0) yearly.aveYearlyTangramTime.Add(aveGameTime);
        aveGameCount = 0;
        if (monthly.aveMonthlyTangramMovesTaken.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyTangramMovesTaken) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyTangramMovesTaken.Count;
        }
        if (aveGameCount > 0) yearly.aveYearlyTangramMovesTaken.Add(aveGameCount);
        aveGameCount = 0;
        if (monthly.aveMonthlyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyTangramGamesPlayed) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyTangramGamesPlayed.Count;
        }
        if (tangramGamesPlayed > 0) yearly.aveYearlyTangramGamesPlayed.Add(tangramGamesPlayed);

        // Colouring Data
        aveGameTime = 0;
        if (monthly.aveMonthlyColouringTime.Count > 0)
        {
            foreach (float timespan in monthly.aveMonthlyColouringTime) aveGameTime += timespan;
            aveGameTime /= monthly.aveMonthlyColouringTime.Count;
        }
        if (aveGameTime > 0) yearly.aveYearlyColouringTime.Add(aveGameTime);
        aveGameCount = 0;
        if (monthly.aveMonthlyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in monthly.aveMonthlyColouringGamesPlayed) aveGameCount += count;
            aveGameCount /= monthly.aveMonthlyColouringGamesPlayed.Count;
        }
        if (colouringGamesPlayed > 0) yearly.aveYearlyColouringGamesPlayed.Add(colouringGamesPlayed);
    }
    public void LogAllTimeData()
    {
        // General Data
        float aveGameTime = 0;
        if (yearly.aveYearlyTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyTime) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyTime.Count;
        }
        if (aveGameTime > 0) allTime.aveTime.Add(aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyMainMenuTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyMainMenuTime) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyMainMenuTime.Count;
        }
        if (aveGameTime > 0) allTime.aveMainMenuTime.Add(aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyCollectionTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyCollectionTime) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyCollectionTime.Count;
        }
        if (aveGameTime > 0) allTime.aveCollectionTime.Add(aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyJigsawSelectTime) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyJigsawSelectTime.Count;
        }
        if (aveGameTime > 0) allTime.aveJigsawSelectTime.Add(aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyTangramSelectTime) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyTangramSelectTime.Count;
        }
        if (aveGameTime > 0) allTime.aveTangramSelectTime.Add(aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyColouringSelectTime) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyColouringSelectTime.Count;
        }
        if (aveGameTime > 0) allTime.aveColouringSelectTime.Add(aveGameTime);
        aveGameTime = 0;
        if (yearly.aveYearlyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyInactivityPeriod) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyInactivityPeriod.Count;
        }
        if (aveGameTime > 0) allTime.aveInactivityPeriod.Add(aveGameTime);

        // Jigsaw Data
        aveGameTime = 0;
        if (yearly.aveYearlyJigsawTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyJigsawTime) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyJigsawTime.Count;
        }
        if (aveGameTime > 0) allTime.aveJigsawTime.Add(aveGameTime);
        int aveGameCount = 0;
        if (yearly.aveYearlyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in yearly.aveYearlyJigsawMovesTaken) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyJigsawMovesTaken.Count;
        }
        if (aveGameCount > 0) allTime.aveJigsawMovesTaken.Add(aveGameCount);
        aveGameCount = 0;
        if (yearly.aveYearlyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in yearly.aveYearlyJigsawErrorsMade) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyJigsawErrorsMade.Count;
        }
        if (aveGameCount > 0) allTime.aveJigsawErrorsMade.Add(aveGameCount);
        aveGameCount = 0;
        if (yearly.aveYearlyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in yearly.aveYearlyJigsawGamesPlayed) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyJigsawGamesPlayed.Count;
        }
        if (jigsawGamesPlayed > 0) allTime.aveJigsawGamesPlayed.Add(jigsawGamesPlayed);

        // Tangram Data
        aveGameTime = 0;
        if (yearly.aveYearlyTangramTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyTangramTime) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyTangramTime.Count;
        }
        if (aveGameTime > 0) allTime.aveTangramTime.Add(aveGameTime);
        aveGameCount = 0;
        if (yearly.aveYearlyTangramMovesTaken.Count > 0)
        {
            foreach (int count in yearly.aveYearlyTangramMovesTaken) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyTangramMovesTaken.Count;
        }
        if (aveGameCount > 0) allTime.aveTangramMovesTaken.Add(aveGameCount);
        aveGameCount = 0;
        if (yearly.aveYearlyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in yearly.aveYearlyTangramGamesPlayed) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyTangramGamesPlayed.Count;
        }
        if (tangramGamesPlayed > 0) allTime.aveTangramGamesPlayed.Add(tangramGamesPlayed);

        // Colouring Data
        aveGameTime = 0;
        if (yearly.aveYearlyColouringTime.Count > 0)
        {
            foreach (float timespan in yearly.aveYearlyColouringTime) aveGameTime += timespan;
            aveGameTime /= yearly.aveYearlyColouringTime.Count;
        }
        if (aveGameTime > 0) allTime.aveColouringTime.Add(aveGameTime);
        aveGameCount = 0;
        if (yearly.aveYearlyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in yearly.aveYearlyColouringGamesPlayed) aveGameCount += count;
            aveGameCount /= yearly.aveYearlyColouringGamesPlayed.Count;
        }
        if (colouringGamesPlayed > 0) allTime.aveColouringGamesPlayed.Add(colouringGamesPlayed);

        allTime.jigsawLevelsKeys = allTime.jigsawLevels.Keys.ToList();
        allTime.jigsawLevelsValues = allTime.jigsawLevels.Values.ToList();
        allTime.tangramLevelsKeys = allTime.tangramLevels.Keys.ToList();
        allTime.tangramLevelsValues = allTime.tangramLevels.Values.ToList();
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
