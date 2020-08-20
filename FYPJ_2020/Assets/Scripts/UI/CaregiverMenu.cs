using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaregiverMenu : MonoBehaviour
{
    public Text header;
    public Button generalButton;
    public GameObject general;
    public Button jigsawButton;
    public GameObject jigsaw;
    public Button tangramButton;
    public GameObject tangram;
    public Button colouringButton;
    public GameObject colouring;

    #region UI
    // General Data
    public Dropdown dropdown;
    public Text time;
    public Text mainMenuTime;
    public Text collectionTime;
    public Text jigsawSelectTime;
    public Text tangramSelectTime;
    public Text colouringSelectTime;
    public Text inactivityPeriod;

    // Jigsaw Data
    public Text jigsawTime;
    public Text jigsawMovesTaken;
    public Text jigsawErrorsMade;
    public Text jigsawGamesPlayed;

    // Tangram Data
    public Text tangramTime;
    public Text tangramMovesTaken;
    public Text tangramGamesPlayed;

    // Colouring Data
    public Text colouringTime;
    public Text colouringGamesPlayed;
    #endregion
    #region Daily
    // General Data
    string aveDailyTime = "";
    string aveDailyMainMenuTime = "";
    string aveDailyCollectionTime = "";
    string aveDailyJigsawSelectTime = "";
    string aveDailyTangramSelectTime = "";
    string aveDailyColouringSelectTime = "";
    string aveDailyInactivityPeriod = "";

    // Jigsaw Data
    string aveDailyJigsawTime = "";
    int aveDailyJigsawMovesTaken = 0;
    int aveDailyJigsawErrorsMade = 0;
    int aveDailyJigsawGamesPlayed = 0;

    // Tangram Data
    string aveDailyTangramTime = "";
    int aveDailyTangramMovesTaken = 0;
    int aveDailyTangramGamesPlayed = 0;

    // Colouring Data
    string aveDailyColouringTime = "";
    int aveDailyColouringGamesPlayed = 0;
    #endregion
    #region Monthly
    // General Data
    string aveMonthlyTime = "";
    string aveMonthlyMainMenuTime = "";
    string aveMonthlyCollectionTime = "";
    string aveMonthlyJigsawSelectTime = "";
    string aveMonthlyTangramSelectTime = "";
    string aveMonthlyColouringSelectTime = "";
    string aveMonthlyInactivityPeriod = "";

    // Jigsaw Data
    string aveMonthlyJigsawTime = "";
    int aveMonthlyJigsawMovesTaken = 0;
    int aveMonthlyJigsawErrorsMade = 0;
    int aveMonthlyJigsawGamesPlayed = 0;

    // Tangram Data
    string aveMonthlyTangramTime = "";
    int aveMonthlyTangramMovesTaken = 0;
    int aveMonthlyTangramGamesPlayed = 0;

    // Colouring Data
    string aveMonthlyColouringTime = "";
    int aveMonthlyColouringGamesPlayed = 0;
    #endregion
    #region Yearly
    // General Data
    string aveYearlyTime = "";
    string aveYearlyMainMenuTime = "";
    string aveYearlyCollectionTime = "";
    string aveYearlyJigsawSelectTime = "";
    string aveYearlyTangramSelectTime = "";
    string aveYearlyColouringSelectTime = "";
    string aveYearlyInactivityPeriod = "";

    // Jigsaw Data
    string aveYearlyJigsawTime = "";
    int aveYearlyJigsawMovesTaken = 0;
    int aveYearlyJigsawErrorsMade = 0;
    int aveYearlyJigsawGamesPlayed = 0;

    // Tangram Data
    string aveYearlyTangramTime = "";
    int aveYearlyTangramMovesTaken = 0;
    int aveYearlyTangramGamesPlayed = 0;

    // Colouring Data
    string aveYearlyColouringTime = "";
    int aveYearlyColouringGamesPlayed = 0;
    #endregion
    #region AllTime
    // General Data
    string aveTime = "";
    string aveMainMenuTime = "";
    string aveCollectionTime = "";
    string aveJigsawSelectTime = "";
    string aveTangramSelectTime = "";
    string aveColouringSelectTime = "";
    string aveInactivityPeriod = "";

    // Jigsaw Data
    string aveJigsawTime = "";
    int aveJigsawMovesTaken = 0;
    int aveJigsawErrorsMade = 0;
    int aveJigsawGamesPlayed = 0;

    // Tangram Data
    string aveTangramTime = "";
    int aveTangramMovesTaken = 0;
    int aveTangramGamesPlayed = 0;

    // Colouring Data
    string aveColouringTime = "";
    int aveColouringGamesPlayed = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.Data == null) return;
        if (GameManager.instance) header.text = GameManager.instance.DisplayName;
        CalculateData();
        OnDisplayChange();
        OnClick("General");
    }

    public void OnClick(string type)
    {
        general.SetActive(false);
        jigsaw.SetActive(false);
        tangram.SetActive(false);
        colouring.SetActive(false);

        switch (type)
        {
            case "General":
                general.SetActive(true);
                break;
            case "Jigsaw":
                jigsaw.SetActive(true);
                break;
            case "Tangram":
                tangram.SetActive(true);
                break;
            case "Colouring":
                colouring.SetActive(true);
                break;
        }
    }

    void DisplayDailyData()
    {
        time.text = aveDailyTime;
        mainMenuTime.text = aveDailyMainMenuTime;
        collectionTime.text = aveDailyCollectionTime;
        jigsawSelectTime.text = aveDailyJigsawSelectTime;
        tangramSelectTime.text = aveDailyTangramSelectTime;
        colouringSelectTime.text = aveDailyColouringSelectTime;
        inactivityPeriod.text = aveDailyInactivityPeriod;

        jigsawTime.text = aveDailyJigsawTime;
        jigsawMovesTaken.text = aveDailyJigsawMovesTaken.ToString();
        jigsawErrorsMade.text = aveDailyJigsawErrorsMade.ToString();
        jigsawGamesPlayed.text = aveDailyJigsawGamesPlayed.ToString();

        tangramTime.text = aveDailyTangramTime;
        tangramMovesTaken.text = aveDailyTangramMovesTaken.ToString();
        tangramGamesPlayed.text = aveDailyTangramGamesPlayed.ToString();

        colouringTime.text = aveDailyColouringTime;
        colouringGamesPlayed.text = aveDailyColouringGamesPlayed.ToString();
    }
    void DisplayMonthlyData()
    {
        time.text = aveMonthlyTime;
        mainMenuTime.text = aveMonthlyMainMenuTime;
        collectionTime.text = aveMonthlyCollectionTime;
        jigsawSelectTime.text = aveMonthlyJigsawSelectTime;
        tangramSelectTime.text = aveMonthlyTangramSelectTime;
        colouringSelectTime.text = aveMonthlyColouringSelectTime;
        inactivityPeriod.text = aveMonthlyInactivityPeriod;

        jigsawTime.text = aveMonthlyJigsawTime;
        jigsawMovesTaken.text = aveMonthlyJigsawMovesTaken.ToString();
        jigsawErrorsMade.text = aveMonthlyJigsawErrorsMade.ToString();
        jigsawGamesPlayed.text = aveMonthlyJigsawGamesPlayed.ToString();

        tangramTime.text = aveMonthlyTangramTime;
        tangramMovesTaken.text = aveMonthlyTangramMovesTaken.ToString();
        tangramGamesPlayed.text = aveMonthlyTangramGamesPlayed.ToString();

        colouringTime.text = aveMonthlyColouringTime;
        colouringGamesPlayed.text = aveMonthlyColouringGamesPlayed.ToString();
    }
    void DisplayYearlyData()
    {
        time.text = aveYearlyTime;
        mainMenuTime.text = aveYearlyMainMenuTime;
        collectionTime.text = aveYearlyCollectionTime;
        jigsawSelectTime.text = aveYearlyJigsawSelectTime;
        tangramSelectTime.text = aveYearlyTangramSelectTime;
        colouringSelectTime.text = aveYearlyColouringSelectTime;
        inactivityPeriod.text = aveYearlyInactivityPeriod;

        jigsawTime.text = aveYearlyJigsawTime;
        jigsawMovesTaken.text = aveYearlyJigsawMovesTaken.ToString();
        jigsawErrorsMade.text = aveYearlyJigsawErrorsMade.ToString();
        jigsawGamesPlayed.text = aveYearlyJigsawGamesPlayed.ToString();

        tangramTime.text = aveYearlyTangramTime;
        tangramMovesTaken.text = aveYearlyTangramMovesTaken.ToString();
        tangramGamesPlayed.text = aveYearlyTangramGamesPlayed.ToString();

        colouringTime.text = aveYearlyColouringTime;
        colouringGamesPlayed.text = aveYearlyColouringGamesPlayed.ToString();
    }
    void DisplayAllTimeData()
    {
        time.text = aveTime;
        mainMenuTime.text = aveMainMenuTime;
        collectionTime.text = aveCollectionTime;
        jigsawSelectTime.text = aveJigsawSelectTime;
        tangramSelectTime.text = aveTangramSelectTime;
        colouringSelectTime.text = aveColouringSelectTime;
        inactivityPeriod.text = aveInactivityPeriod;

        jigsawTime.text = aveJigsawTime;
        jigsawMovesTaken.text = aveJigsawMovesTaken.ToString();
        jigsawErrorsMade.text = aveJigsawErrorsMade.ToString();
        jigsawGamesPlayed.text = aveJigsawGamesPlayed.ToString();

        tangramTime.text = aveTangramTime;
        tangramMovesTaken.text = aveTangramMovesTaken.ToString();
        tangramGamesPlayed.text = aveTangramGamesPlayed.ToString();

        colouringTime.text = aveColouringTime;
        colouringGamesPlayed.text = aveColouringGamesPlayed.ToString();
    }
    public void OnDisplayChange()
    {
        Debug.Log("Actual Dropdown.value = " + dropdown.value);
        switch (dropdown.value)
        {
            case 1:
                Debug.Log("Dropdown.value = 1");
                DisplayMonthlyData();
                break;
            case 2:
                Debug.Log("Dropdown.value = 2");
                DisplayYearlyData();
                break;
            case 3:
                Debug.Log("Dropdown.value = 3");
                DisplayAllTimeData();
                break;
            default:
                Debug.Log("Dropdown.value = default");
                DisplayDailyData();
                break;
        }
    }

    void CalculateDailyData()
    {
        float value = 0;
        // General Data
        if (GameManager.instance.Data.daily.aveDailyTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyTime) value += timespan;
            value /= GameManager.instance.Data.daily.aveDailyTime.Count;
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(value);
        aveDailyTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.daily.aveDailyMainMenuTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyMainMenuTime) value += timespan;
            value /= GameManager.instance.Data.daily.aveDailyMainMenuTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveDailyMainMenuTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.daily.aveDailyCollectionTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyCollectionTime) value += timespan;
            value /= GameManager.instance.Data.daily.aveDailyCollectionTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveDailyCollectionTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.daily.aveDailyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyJigsawSelectTime) value += timespan;
            value /= GameManager.instance.Data.daily.aveDailyJigsawSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveDailyJigsawSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.daily.aveDailyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyTangramSelectTime) value += timespan;
            value /= GameManager.instance.Data.daily.aveDailyTangramSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveDailyTangramSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.daily.aveDailyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyColouringSelectTime) value += timespan;
            value /= GameManager.instance.Data.daily.aveDailyColouringSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveDailyColouringSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.daily.aveDailyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyInactivityPeriod) value += timespan;
            value /= GameManager.instance.Data.daily.aveDailyInactivityPeriod.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveDailyInactivityPeriod = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        // Jigsaw Data
        value = 0;
        if (GameManager.instance.Data.daily.aveDailyJigsawTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyJigsawTime) value += timespan;
            value /= GameManager.instance.Data.daily.aveDailyJigsawTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveDailyJigsawTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveDailyJigsawMovesTaken = 0;
        if (GameManager.instance.Data.daily.aveDailyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyJigsawMovesTaken) aveDailyJigsawMovesTaken += count;
            aveDailyJigsawMovesTaken /= GameManager.instance.Data.daily.aveDailyJigsawMovesTaken.Count;
        }
        aveDailyJigsawErrorsMade = 0;
        if (GameManager.instance.Data.daily.aveDailyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyJigsawErrorsMade) aveDailyJigsawErrorsMade += count;
            aveDailyJigsawErrorsMade /= GameManager.instance.Data.daily.aveDailyJigsawErrorsMade.Count;
        }
        aveDailyJigsawGamesPlayed = 0;
        if (GameManager.instance.Data.daily.aveDailyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyJigsawGamesPlayed) aveDailyJigsawGamesPlayed += count;
            aveDailyJigsawGamesPlayed /= GameManager.instance.Data.daily.aveDailyJigsawGamesPlayed.Count;
        }

        // Tangram Data
        value = 0;
        if (GameManager.instance.Data.daily.aveDailyTangramTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyTangramTime) value += timespan;
            value /= GameManager.instance.Data.daily.aveDailyTangramTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveDailyTangramTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveDailyTangramMovesTaken = 0;
        if (GameManager.instance.Data.daily.aveDailyTangramMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyTangramMovesTaken) aveDailyTangramMovesTaken += count;
            aveDailyTangramMovesTaken /= GameManager.instance.Data.daily.aveDailyTangramMovesTaken.Count;
        }
        aveDailyTangramGamesPlayed = 0;
        if (GameManager.instance.Data.daily.aveDailyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyTangramGamesPlayed) aveDailyTangramGamesPlayed += count;
            aveDailyTangramGamesPlayed /= GameManager.instance.Data.daily.aveDailyTangramGamesPlayed.Count;
        }

        // Colouring Data
        value = 0;
        if (GameManager.instance.Data.daily.aveDailyColouringTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyColouringTime) value += timespan;
            value /= GameManager.instance.Data.daily.aveDailyColouringTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveDailyColouringTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveDailyColouringGamesPlayed = 0;
        if (GameManager.instance.Data.daily.aveDailyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyColouringGamesPlayed) aveDailyColouringGamesPlayed += count;
            aveDailyColouringGamesPlayed /= GameManager.instance.Data.daily.aveDailyColouringGamesPlayed.Count;
        }
    }
    void CalculateMonthlyData()
    {
        float value = 0;
        // General Data
        if (GameManager.instance.Data.monthly.aveMonthlyTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyTime) value += timespan;
            value /= GameManager.instance.Data.monthly.aveMonthlyTime.Count;
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(value);
        aveMonthlyTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyMainMenuTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyMainMenuTime) value += timespan;
            value /= GameManager.instance.Data.monthly.aveMonthlyMainMenuTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveMonthlyMainMenuTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyCollectionTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyCollectionTime) value += timespan;
            value /= GameManager.instance.Data.monthly.aveMonthlyCollectionTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveMonthlyCollectionTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyJigsawSelectTime) value += timespan;
            value /= GameManager.instance.Data.monthly.aveMonthlyJigsawSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveMonthlyJigsawSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyTangramSelectTime) value += timespan;
            value /= GameManager.instance.Data.monthly.aveMonthlyTangramSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveMonthlyTangramSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyColouringSelectTime) value += timespan;
            value /= GameManager.instance.Data.monthly.aveMonthlyColouringSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveMonthlyColouringSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyInactivityPeriod) value += timespan;
            value /= GameManager.instance.Data.monthly.aveMonthlyInactivityPeriod.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveMonthlyInactivityPeriod = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        // Jigsaw Data
        value = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyJigsawTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyJigsawTime) value += timespan;
            value /= GameManager.instance.Data.monthly.aveMonthlyJigsawTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveMonthlyJigsawTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveMonthlyJigsawMovesTaken = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyJigsawMovesTaken) aveMonthlyJigsawMovesTaken += count;
            aveMonthlyJigsawMovesTaken /= GameManager.instance.Data.monthly.aveMonthlyJigsawMovesTaken.Count;
        }
        aveMonthlyJigsawErrorsMade = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyJigsawErrorsMade) aveMonthlyJigsawErrorsMade += count;
            aveMonthlyJigsawErrorsMade /= GameManager.instance.Data.monthly.aveMonthlyJigsawErrorsMade.Count;
        }
        aveMonthlyJigsawGamesPlayed = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyJigsawGamesPlayed) aveMonthlyJigsawGamesPlayed += count;
            aveMonthlyJigsawGamesPlayed /= GameManager.instance.Data.monthly.aveMonthlyJigsawGamesPlayed.Count;
        }

        // Tangram Data
        value = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyTangramTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyTangramTime) value += timespan;
            value /= GameManager.instance.Data.monthly.aveMonthlyTangramTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveMonthlyTangramTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveMonthlyTangramMovesTaken = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyTangramMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyTangramMovesTaken) aveMonthlyTangramMovesTaken += count;
            aveMonthlyTangramMovesTaken /= GameManager.instance.Data.monthly.aveMonthlyTangramMovesTaken.Count;
        }
        aveMonthlyTangramGamesPlayed = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyTangramGamesPlayed) aveMonthlyTangramGamesPlayed += count;
            aveMonthlyTangramGamesPlayed /= GameManager.instance.Data.monthly.aveMonthlyTangramGamesPlayed.Count;
        }

        // Colouring Data
        value = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyColouringTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyColouringTime) value += timespan;
            value /= GameManager.instance.Data.monthly.aveMonthlyColouringTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveMonthlyColouringTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveMonthlyColouringGamesPlayed = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyColouringGamesPlayed) aveMonthlyColouringGamesPlayed += count;
            aveMonthlyColouringGamesPlayed /= GameManager.instance.Data.monthly.aveMonthlyColouringGamesPlayed.Count;
        }
    }
    void CalculateYearlyData()
    {
        float value = 0;
        // General Data
        if (GameManager.instance.Data.yearly.aveYearlyTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyTime) value += timespan;
            value /= GameManager.instance.Data.yearly.aveYearlyTime.Count;
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(value);
        aveYearlyTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.yearly.aveYearlyMainMenuTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyMainMenuTime) value += timespan;
            value /= GameManager.instance.Data.yearly.aveYearlyMainMenuTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveYearlyMainMenuTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.yearly.aveYearlyCollectionTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyCollectionTime) value += timespan;
            value /= GameManager.instance.Data.yearly.aveYearlyCollectionTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveYearlyCollectionTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.yearly.aveYearlyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyJigsawSelectTime) value += timespan;
            value /= GameManager.instance.Data.yearly.aveYearlyJigsawSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveYearlyJigsawSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.yearly.aveYearlyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyTangramSelectTime) value += timespan;
            value /= GameManager.instance.Data.yearly.aveYearlyTangramSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveYearlyTangramSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.yearly.aveYearlyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyColouringSelectTime) value += timespan;
            value /= GameManager.instance.Data.yearly.aveYearlyColouringSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveYearlyColouringSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.yearly.aveYearlyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyInactivityPeriod) value += timespan;
            value /= GameManager.instance.Data.yearly.aveYearlyInactivityPeriod.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveYearlyInactivityPeriod = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        // Jigsaw Data
        value = 0;
        if (GameManager.instance.Data.yearly.aveYearlyJigsawTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyJigsawTime) value += timespan;
            value /= GameManager.instance.Data.yearly.aveYearlyJigsawTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveYearlyJigsawTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveYearlyJigsawMovesTaken = 0;
        if (GameManager.instance.Data.yearly.aveYearlyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyJigsawMovesTaken) aveYearlyJigsawMovesTaken += count;
            aveYearlyJigsawMovesTaken /= GameManager.instance.Data.yearly.aveYearlyJigsawMovesTaken.Count;
        }
        aveYearlyJigsawErrorsMade = 0;
        if (GameManager.instance.Data.yearly.aveYearlyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyJigsawErrorsMade) aveYearlyJigsawErrorsMade += count;
            aveYearlyJigsawErrorsMade /= GameManager.instance.Data.yearly.aveYearlyJigsawErrorsMade.Count;
        }
        aveYearlyJigsawGamesPlayed = 0;
        if (GameManager.instance.Data.yearly.aveYearlyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyJigsawGamesPlayed) aveYearlyJigsawGamesPlayed += count;
            aveYearlyJigsawGamesPlayed /= GameManager.instance.Data.yearly.aveYearlyJigsawGamesPlayed.Count;
        }

        // Tangram Data
        value = 0;
        if (GameManager.instance.Data.yearly.aveYearlyTangramTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyTangramTime) value += timespan;
            value /= GameManager.instance.Data.yearly.aveYearlyTangramTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveYearlyTangramTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveYearlyTangramMovesTaken = 0;
        if (GameManager.instance.Data.yearly.aveYearlyTangramMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyTangramMovesTaken) aveYearlyTangramMovesTaken += count;
            aveYearlyTangramMovesTaken /= GameManager.instance.Data.yearly.aveYearlyTangramMovesTaken.Count;
        }
        aveYearlyTangramGamesPlayed = 0;
        if (GameManager.instance.Data.yearly.aveYearlyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyTangramGamesPlayed) aveYearlyTangramGamesPlayed += count;
            aveYearlyTangramGamesPlayed /= GameManager.instance.Data.yearly.aveYearlyTangramGamesPlayed.Count;
        }

        // Colouring Data
        value = 0;
        if (GameManager.instance.Data.yearly.aveYearlyColouringTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyColouringTime) value += timespan;
            value /= GameManager.instance.Data.yearly.aveYearlyColouringTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveYearlyColouringTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveYearlyColouringGamesPlayed = 0;
        if (GameManager.instance.Data.yearly.aveYearlyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyColouringGamesPlayed) aveYearlyColouringGamesPlayed += count;
            aveYearlyColouringGamesPlayed /= GameManager.instance.Data.yearly.aveYearlyColouringGamesPlayed.Count;
        }
    }
    void CalculateAllTimeData()
    {
        float value = 0;
        // General Data
        if (GameManager.instance.Data.allTime.aveTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveTime) value += timespan;
            value /= GameManager.instance.Data.allTime.aveTime.Count;
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(value);
        aveTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.allTime.aveMainMenuTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveMainMenuTime) value += timespan;
            value /= GameManager.instance.Data.allTime.aveMainMenuTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveMainMenuTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.allTime.aveCollectionTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveCollectionTime) value += timespan;
            value /= GameManager.instance.Data.allTime.aveCollectionTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveCollectionTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.allTime.aveJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveJigsawSelectTime) value += timespan;
            value /= GameManager.instance.Data.allTime.aveJigsawSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveJigsawSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.allTime.aveTangramSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveTangramSelectTime) value += timespan;
            value /= GameManager.instance.Data.allTime.aveTangramSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveTangramSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.allTime.aveColouringSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveColouringSelectTime) value += timespan;
            value /= GameManager.instance.Data.allTime.aveColouringSelectTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveColouringSelectTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        value = 0;
        if (GameManager.instance.Data.allTime.aveInactivityPeriod.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveInactivityPeriod) value += timespan;
            value /= GameManager.instance.Data.allTime.aveInactivityPeriod.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveInactivityPeriod = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        // Jigsaw Data
        value = 0;
        if (GameManager.instance.Data.allTime.aveJigsawTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveJigsawTime) value += timespan;
            value /= GameManager.instance.Data.allTime.aveJigsawTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveJigsawTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveJigsawMovesTaken = 0;
        if (GameManager.instance.Data.allTime.aveJigsawMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveJigsawMovesTaken) aveJigsawMovesTaken += count;
            aveJigsawMovesTaken /= GameManager.instance.Data.allTime.aveJigsawMovesTaken.Count;
        }
        aveJigsawErrorsMade = 0;
        if (GameManager.instance.Data.allTime.aveJigsawErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveJigsawErrorsMade) aveJigsawErrorsMade += count;
            aveJigsawErrorsMade /= GameManager.instance.Data.allTime.aveJigsawErrorsMade.Count;
        }
        aveJigsawGamesPlayed = 0;
        if (GameManager.instance.Data.allTime.aveJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveJigsawGamesPlayed) aveJigsawGamesPlayed += count;
            aveJigsawGamesPlayed /= GameManager.instance.Data.allTime.aveJigsawGamesPlayed.Count;
        }

        // Tangram Data
        value = 0;
        if (GameManager.instance.Data.allTime.aveTangramTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveTangramTime) value += timespan;
            value /= GameManager.instance.Data.allTime.aveTangramTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveTangramTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveTangramMovesTaken = 0;
        if (GameManager.instance.Data.allTime.aveTangramMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveTangramMovesTaken) aveTangramMovesTaken += count;
            aveTangramMovesTaken /= GameManager.instance.Data.allTime.aveTangramMovesTaken.Count;
        }
        aveTangramGamesPlayed = 0;
        if (GameManager.instance.Data.allTime.aveTangramGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveTangramGamesPlayed) aveTangramGamesPlayed += count;
            aveTangramGamesPlayed /= GameManager.instance.Data.allTime.aveTangramGamesPlayed.Count;
        }

        // Colouring Data
        value = 0;
        if (GameManager.instance.Data.allTime.aveColouringTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveColouringTime) value += timespan;
            value /= GameManager.instance.Data.allTime.aveColouringTime.Count;
        }
        timeSpan = TimeSpan.FromSeconds(value);
        aveColouringTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        aveColouringGamesPlayed = 0;
        if (GameManager.instance.Data.allTime.aveColouringGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveColouringGamesPlayed) aveColouringGamesPlayed += count;
            aveColouringGamesPlayed /= GameManager.instance.Data.allTime.aveColouringGamesPlayed.Count;
        }
    }
    public void CalculateData()
    {
        CalculateDailyData();
        CalculateMonthlyData();
        CalculateYearlyData();
        CalculateAllTimeData();
    }
}