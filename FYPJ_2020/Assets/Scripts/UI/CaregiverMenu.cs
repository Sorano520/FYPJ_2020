using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaregiverMenu : MonoBehaviour
{
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
    public Text tangramErrorsMade;
    public Text tangramGamesPlayed;

    // Colouring Data
    public Text colouringTime;
    public Text colouringGamesPlayed;
    #endregion
    #region Daily
    // General Data
    float aveDailyTime = 0;
    float aveDailyMainMenuTime = 0;
    float aveDailyCollectionTime = 0;
    float aveDailyJigsawSelectTime = 0;
    float aveDailyTangramSelectTime = 0;
    float aveDailyColouringSelectTime = 0;
    float aveDailyInactivityPeriod = 0;

    // Jigsaw Data
    float aveDailyJigsawTime = 0;
    int aveDailyJigsawMovesTaken = 0;
    int aveDailyJigsawErrorsMade = 0;
    int aveDailyJigsawGamesPlayed = 0;

    // Tangram Data
    float aveDailyTangramTime = 0;
    int aveDailyTangramMovesTaken = 0;
    int aveDailyTangramErrorsMade = 0;
    int aveDailyTangramGamesPlayed = 0;

    // Colouring Data
    float aveDailyColouringTime = 0;
    int aveDailyColouringGamesPlayed = 0;
    #endregion
    #region Monthly
    // General Data
    float aveMonthlyTime = 0;
    float aveMonthlyMainMenuTime = 0;
    float aveMonthlyCollectionTime = 0;
    float aveMonthlyJigsawSelectTime = 0;
    float aveMonthlyTangramSelectTime = 0;
    float aveMonthlyColouringSelectTime = 0;
    float aveMonthlyInactivityPeriod = 0;

    // Jigsaw Data
    float aveMonthlyJigsawTime = 0;
    int aveMonthlyJigsawMovesTaken = 0;
    int aveMonthlyJigsawErrorsMade = 0;
    int aveMonthlyJigsawGamesPlayed = 0;

    // Tangram Data
    float aveMonthlyTangramTime = 0;
    int aveMonthlyTangramMovesTaken = 0;
    int aveMonthlyTangramErrorsMade = 0;
    int aveMonthlyTangramGamesPlayed = 0;

    // Colouring Data
    float aveMonthlyColouringTime = 0;
    int aveMonthlyColouringGamesPlayed = 0;
    #endregion
    #region Yearly
    // General Data
    float aveYearlyTime = 0;
    float aveYearlyMainMenuTime = 0;
    float aveYearlyCollectionTime = 0;
    float aveYearlyJigsawSelectTime = 0;
    float aveYearlyTangramSelectTime = 0;
    float aveYearlyColouringSelectTime = 0;
    float aveYearlyInactivityPeriod = 0;

    // Jigsaw Data
    float aveYearlyJigsawTime = 0;
    int aveYearlyJigsawMovesTaken = 0;
    int aveYearlyJigsawErrorsMade = 0;
    int aveYearlyJigsawGamesPlayed = 0;

    // Tangram Data
    float aveYearlyTangramTime = 0;
    int aveYearlyTangramMovesTaken = 0;
    int aveYearlyTangramErrorsMade = 0;
    int aveYearlyTangramGamesPlayed = 0;

    // Colouring Data
    float aveYearlyColouringTime = 0;
    int aveYearlyColouringGamesPlayed = 0;
    #endregion
    #region AllTime
    // General Data
    float aveTime = 0;
    float aveMainMenuTime = 0;
    float aveCollectionTime = 0;
    float aveJigsawSelectTime = 0;
    float aveTangramSelectTime = 0;
    float aveColouringSelectTime = 0;
    float aveInactivityPeriod = 0;

    // Jigsaw Data
    float aveJigsawTime = 0;
    int aveJigsawMovesTaken = 0;
    int aveJigsawErrorsMade = 0;
    int aveJigsawGamesPlayed = 0;

    // Tangram Data
    float aveTangramTime = 0;
    int aveTangramMovesTaken = 0;
    int aveTangramErrorsMade = 0;
    int aveTangramGamesPlayed = 0;

    // Colouring Data
    float aveColouringTime = 0;
    int aveColouringGamesPlayed = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.Data == null) return;
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
        time.text = TimeSpan.FromSeconds(aveDailyTime).ToString();
        mainMenuTime.text = TimeSpan.FromSeconds(aveDailyMainMenuTime).ToString();
        collectionTime.text = TimeSpan.FromSeconds(aveDailyCollectionTime).ToString();
        jigsawSelectTime.text = TimeSpan.FromSeconds(aveDailyJigsawSelectTime).ToString();
        tangramSelectTime.text = TimeSpan.FromSeconds(aveDailyTangramSelectTime).ToString();
        colouringSelectTime.text = TimeSpan.FromSeconds(aveDailyColouringSelectTime).ToString();
        inactivityPeriod.text = TimeSpan.FromSeconds(aveDailyInactivityPeriod).ToString();

        jigsawTime.text = TimeSpan.FromSeconds(aveDailyJigsawTime).ToString();
        jigsawMovesTaken.text = aveDailyJigsawMovesTaken.ToString();
        jigsawErrorsMade.text = aveDailyJigsawErrorsMade.ToString();
        jigsawGamesPlayed.text = aveDailyJigsawGamesPlayed.ToString();

        tangramTime.text = TimeSpan.FromSeconds(aveDailyTangramTime).ToString();
        tangramMovesTaken.text = aveDailyTangramMovesTaken.ToString();
        tangramErrorsMade.text = aveDailyTangramErrorsMade.ToString();
        tangramGamesPlayed.text = aveDailyTangramGamesPlayed.ToString();

        colouringTime.text = TimeSpan.FromSeconds(aveDailyColouringTime).ToString();
        colouringGamesPlayed.text = aveDailyColouringGamesPlayed.ToString();
    }
    void DisplayMonthlyData()
    {   
        time.text = TimeSpan.FromSeconds(aveMonthlyTime).ToString();
        mainMenuTime.text = TimeSpan.FromSeconds(aveMonthlyMainMenuTime).ToString();
        collectionTime.text = TimeSpan.FromSeconds(aveMonthlyCollectionTime).ToString();
        jigsawSelectTime.text = TimeSpan.FromSeconds(aveMonthlyJigsawSelectTime).ToString();
        tangramSelectTime.text = TimeSpan.FromSeconds(aveMonthlyTangramSelectTime).ToString();
        colouringSelectTime.text = TimeSpan.FromSeconds(aveMonthlyColouringSelectTime).ToString();
        inactivityPeriod.text = TimeSpan.FromSeconds(aveMonthlyInactivityPeriod).ToString();

        jigsawTime.text = TimeSpan.FromSeconds(aveMonthlyJigsawTime).ToString();
        jigsawMovesTaken.text = aveMonthlyJigsawMovesTaken.ToString();
        jigsawErrorsMade.text = aveMonthlyJigsawErrorsMade.ToString();
        jigsawGamesPlayed.text = aveMonthlyJigsawGamesPlayed.ToString();

        tangramTime.text = TimeSpan.FromSeconds(aveMonthlyTangramTime).ToString();
        tangramMovesTaken.text = aveMonthlyTangramMovesTaken.ToString();
        tangramErrorsMade.text = aveMonthlyTangramErrorsMade.ToString();
        tangramGamesPlayed.text = aveMonthlyTangramGamesPlayed.ToString();

        colouringTime.text = TimeSpan.FromSeconds(aveMonthlyColouringTime).ToString();
        colouringGamesPlayed.text = aveMonthlyColouringGamesPlayed.ToString();
    }
    void DisplayYearlyData()
    {   
        time.text = TimeSpan.FromSeconds(aveYearlyTime).ToString();
        mainMenuTime.text = TimeSpan.FromSeconds(aveYearlyMainMenuTime).ToString();
        collectionTime.text = TimeSpan.FromSeconds(aveYearlyCollectionTime).ToString();
        jigsawSelectTime.text = TimeSpan.FromSeconds(aveYearlyJigsawSelectTime).ToString();
        tangramSelectTime.text = TimeSpan.FromSeconds(aveYearlyTangramSelectTime).ToString();
        colouringSelectTime.text = TimeSpan.FromSeconds(aveYearlyColouringSelectTime).ToString();
        inactivityPeriod.text = TimeSpan.FromSeconds(aveYearlyInactivityPeriod).ToString();

        jigsawTime.text = TimeSpan.FromSeconds(aveYearlyJigsawTime).ToString();
        jigsawMovesTaken.text = aveYearlyJigsawMovesTaken.ToString();
        jigsawErrorsMade.text = aveYearlyJigsawErrorsMade.ToString();
        jigsawGamesPlayed.text = aveYearlyJigsawGamesPlayed.ToString();

        tangramTime.text = TimeSpan.FromSeconds(aveYearlyTangramTime).ToString();
        tangramMovesTaken.text = aveYearlyTangramMovesTaken.ToString();
        tangramErrorsMade.text = aveYearlyTangramErrorsMade.ToString();
        tangramGamesPlayed.text = aveYearlyTangramGamesPlayed.ToString();

        colouringTime.text = TimeSpan.FromSeconds(aveYearlyColouringTime).ToString();
        colouringGamesPlayed.text = aveYearlyColouringGamesPlayed.ToString();
    }
    void DisplayAllTimeData()
    {   
        time.text = TimeSpan.FromSeconds(aveTime).ToString();
        mainMenuTime.text = TimeSpan.FromSeconds(aveMainMenuTime).ToString();
        collectionTime.text = TimeSpan.FromSeconds(aveCollectionTime).ToString();
        jigsawSelectTime.text = TimeSpan.FromSeconds(aveJigsawSelectTime).ToString();
        tangramSelectTime.text = TimeSpan.FromSeconds(aveTangramSelectTime).ToString();
        colouringSelectTime.text = TimeSpan.FromSeconds(aveColouringSelectTime).ToString();
        inactivityPeriod.text = TimeSpan.FromSeconds(aveInactivityPeriod).ToString();

        jigsawTime.text = TimeSpan.FromSeconds(aveJigsawTime).ToString();
        jigsawMovesTaken.text = aveJigsawMovesTaken.ToString();
        jigsawErrorsMade.text = aveJigsawErrorsMade.ToString();
        jigsawGamesPlayed.text = aveJigsawGamesPlayed.ToString();

        tangramTime.text = TimeSpan.FromSeconds(aveTangramTime).ToString();
        tangramMovesTaken.text = aveTangramMovesTaken.ToString();
        tangramErrorsMade.text = aveTangramErrorsMade.ToString();
        tangramGamesPlayed.text = aveTangramGamesPlayed.ToString();

        colouringTime.text = TimeSpan.FromSeconds(aveColouringTime).ToString();
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
        // General Data
        aveDailyTime = 0;
        if (GameManager.instance.Data.daily.aveDailyTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyTime.Values) aveDailyTime += timespan;
            aveDailyTime /= GameManager.instance.Data.daily.aveDailyTime.Count;
        }
        aveDailyMainMenuTime = 0;
        if (GameManager.instance.Data.daily.aveDailyMainMenuTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyMainMenuTime.Values) aveDailyMainMenuTime += timespan;
            aveDailyMainMenuTime /= GameManager.instance.Data.daily.aveDailyMainMenuTime.Count;
        }
        aveDailyCollectionTime = 0;
        if (GameManager.instance.Data.daily.aveDailyCollectionTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyCollectionTime.Values) aveDailyCollectionTime += timespan;
            aveDailyCollectionTime /= GameManager.instance.Data.daily.aveDailyCollectionTime.Count;
        }
        aveDailyJigsawSelectTime = 0;
        if (GameManager.instance.Data.daily.aveDailyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyJigsawSelectTime.Values) aveDailyJigsawSelectTime += timespan;
            aveDailyJigsawSelectTime /= GameManager.instance.Data.daily.aveDailyJigsawSelectTime.Count;
        }
        aveDailyTangramSelectTime = 0;
        if (GameManager.instance.Data.daily.aveDailyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyTangramSelectTime.Values) aveDailyTangramSelectTime += timespan;
            aveDailyTangramSelectTime /= GameManager.instance.Data.daily.aveDailyTangramSelectTime.Count;
        }
        aveDailyColouringSelectTime = 0;
        if (GameManager.instance.Data.daily.aveDailyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyColouringSelectTime.Values) aveDailyColouringSelectTime += timespan;
            aveDailyColouringSelectTime /= GameManager.instance.Data.daily.aveDailyColouringSelectTime.Count;
        }
        aveDailyInactivityPeriod = 0;
        if (GameManager.instance.Data.daily.aveDailyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyInactivityPeriod.Values) aveDailyInactivityPeriod += timespan;
            aveDailyInactivityPeriod /= GameManager.instance.Data.daily.aveDailyInactivityPeriod.Count;
        }

        // Jigsaw Data
        aveDailyJigsawTime = 0;
        if (GameManager.instance.Data.daily.aveDailyJigsawTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyJigsawTime.Values) aveDailyJigsawTime += timespan;
            aveDailyJigsawTime /= GameManager.instance.Data.daily.aveDailyJigsawTime.Count;
        }
        aveDailyJigsawMovesTaken = 0;
        if (GameManager.instance.Data.daily.aveDailyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyJigsawMovesTaken.Values) aveDailyJigsawMovesTaken += count;
            aveDailyJigsawMovesTaken /= GameManager.instance.Data.daily.aveDailyJigsawMovesTaken.Count;
        }
        aveDailyJigsawErrorsMade = 0;
        if (GameManager.instance.Data.daily.aveDailyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyJigsawErrorsMade.Values) aveDailyJigsawErrorsMade += count;
            aveDailyJigsawErrorsMade /= GameManager.instance.Data.daily.aveDailyJigsawErrorsMade.Count;
        }
        aveDailyJigsawGamesPlayed = 0;
        if (GameManager.instance.Data.daily.aveDailyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyJigsawGamesPlayed.Values) aveDailyJigsawGamesPlayed += count;
            aveDailyJigsawGamesPlayed /= GameManager.instance.Data.daily.aveDailyJigsawGamesPlayed.Count;
        }

        // Tangram Data
        aveDailyTangramTime = 0;
        if (GameManager.instance.Data.daily.aveDailyTangramTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyTangramTime.Values) aveDailyTangramTime += timespan;
            aveDailyTangramTime /= GameManager.instance.Data.daily.aveDailyTangramTime.Count;
        }
        aveDailyTangramMovesTaken = 0;
        if (GameManager.instance.Data.daily.aveDailyTangramMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyTangramMovesTaken.Values) aveDailyTangramMovesTaken += count;
            aveDailyTangramMovesTaken /= GameManager.instance.Data.daily.aveDailyTangramMovesTaken.Count;
        }
        aveDailyTangramErrorsMade = 0;
        if (GameManager.instance.Data.daily.aveDailyTangramErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyTangramErrorsMade.Values) aveDailyTangramErrorsMade += count;
            aveDailyTangramErrorsMade /= GameManager.instance.Data.daily.aveDailyTangramErrorsMade.Count;
        }
        aveDailyTangramGamesPlayed = 0;
        if (GameManager.instance.Data.daily.aveDailyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyTangramGamesPlayed.Values) aveDailyTangramGamesPlayed += count;
            aveDailyTangramGamesPlayed /= GameManager.instance.Data.daily.aveDailyTangramGamesPlayed.Count;
        }

        // Colouring Data
        aveDailyColouringTime = 0;
        if (GameManager.instance.Data.daily.aveDailyColouringTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.daily.aveDailyColouringTime.Values) aveDailyColouringTime += timespan;
            aveDailyColouringTime /= GameManager.instance.Data.daily.aveDailyColouringTime.Count;
        }
        aveDailyColouringGamesPlayed = 0;
        if (GameManager.instance.Data.daily.aveDailyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.daily.aveDailyColouringGamesPlayed.Values) aveDailyColouringGamesPlayed += count;
            aveDailyColouringGamesPlayed /= GameManager.instance.Data.daily.aveDailyColouringGamesPlayed.Count;
        }
    }
    public void CalculateMonthlyData()
    {
        // General Data
        aveMonthlyTime = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyTime.Values) aveMonthlyTime += timespan;
            aveMonthlyTime /= GameManager.instance.Data.monthly.aveMonthlyTime.Count;
        }
        aveMonthlyMainMenuTime = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyMainMenuTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyMainMenuTime.Values) aveMonthlyMainMenuTime += timespan;
            aveMonthlyMainMenuTime /= GameManager.instance.Data.monthly.aveMonthlyMainMenuTime.Count;
        }
        aveMonthlyCollectionTime = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyCollectionTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyCollectionTime.Values) aveMonthlyCollectionTime += timespan;
            aveMonthlyCollectionTime /= GameManager.instance.Data.monthly.aveMonthlyCollectionTime.Count;
        }
        aveMonthlyJigsawSelectTime = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyJigsawSelectTime.Values) aveMonthlyJigsawSelectTime += timespan;
            aveMonthlyJigsawSelectTime /= GameManager.instance.Data.monthly.aveMonthlyJigsawSelectTime.Count;
        }
        aveMonthlyTangramSelectTime = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyTangramSelectTime.Values) aveMonthlyTangramSelectTime += timespan;
            aveMonthlyTangramSelectTime /= GameManager.instance.Data.monthly.aveMonthlyTangramSelectTime.Count;
        }
        aveMonthlyColouringSelectTime = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyColouringSelectTime.Values) aveMonthlyColouringSelectTime += timespan;
            aveMonthlyColouringSelectTime /= GameManager.instance.Data.monthly.aveMonthlyColouringSelectTime.Count;
        }
        aveMonthlyInactivityPeriod = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyInactivityPeriod.Values) aveMonthlyInactivityPeriod += timespan;
            aveMonthlyInactivityPeriod /= GameManager.instance.Data.monthly.aveMonthlyInactivityPeriod.Count;
        }

        // Jigsaw Data
        aveMonthlyJigsawTime = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyJigsawTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyJigsawTime.Values) aveMonthlyJigsawTime += timespan;
            aveMonthlyJigsawTime /= GameManager.instance.Data.monthly.aveMonthlyJigsawTime.Count;
        }
        aveMonthlyJigsawMovesTaken = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyJigsawMovesTaken.Values) aveMonthlyJigsawMovesTaken += count;
            aveMonthlyJigsawMovesTaken /= GameManager.instance.Data.monthly.aveMonthlyJigsawMovesTaken.Count;
        }
        aveMonthlyJigsawErrorsMade = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyJigsawErrorsMade.Values) aveMonthlyJigsawErrorsMade += count;
            aveMonthlyJigsawErrorsMade /= GameManager.instance.Data.monthly.aveMonthlyJigsawErrorsMade.Count;
        }
        aveMonthlyJigsawGamesPlayed = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyJigsawGamesPlayed.Values) aveMonthlyJigsawGamesPlayed += count;
            aveMonthlyJigsawGamesPlayed /= GameManager.instance.Data.monthly.aveMonthlyJigsawGamesPlayed.Count;
        }

        // Tangram Data
        aveMonthlyTangramTime = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyTangramTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyTangramTime.Values) aveMonthlyTangramTime += timespan;
            aveMonthlyTangramTime /= GameManager.instance.Data.monthly.aveMonthlyTangramTime.Count;
        }
        aveMonthlyTangramMovesTaken = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyTangramMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyTangramMovesTaken.Values) aveMonthlyTangramMovesTaken += count;
            aveMonthlyTangramMovesTaken /= GameManager.instance.Data.monthly.aveMonthlyTangramMovesTaken.Count;
        }
        aveMonthlyTangramErrorsMade = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyTangramErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyTangramErrorsMade.Values) aveMonthlyTangramErrorsMade += count;
            aveMonthlyTangramErrorsMade /= GameManager.instance.Data.monthly.aveMonthlyTangramErrorsMade.Count;
        }
        aveMonthlyTangramGamesPlayed = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyTangramGamesPlayed.Values) aveMonthlyTangramGamesPlayed += count;
            aveMonthlyTangramGamesPlayed /= GameManager.instance.Data.monthly.aveMonthlyTangramGamesPlayed.Count;
        }

        // Colouring Data
        aveMonthlyColouringTime = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyColouringTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.monthly.aveMonthlyColouringTime.Values) aveMonthlyColouringTime += timespan;
            aveMonthlyColouringTime /= GameManager.instance.Data.monthly.aveMonthlyColouringTime.Count;
        }
        aveMonthlyColouringGamesPlayed = 0;
        if (GameManager.instance.Data.monthly.aveMonthlyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.monthly.aveMonthlyColouringGamesPlayed.Values) aveMonthlyColouringGamesPlayed += count;
            aveMonthlyColouringGamesPlayed /= GameManager.instance.Data.monthly.aveMonthlyColouringGamesPlayed.Count;
        }
    }
    public void CalculateYearlyData()
    {
        // General Data
        aveYearlyTime = 0;
        if (GameManager.instance.Data.yearly.aveYearlyTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyTime.Values) aveYearlyTime += timespan;
            aveYearlyTime /= GameManager.instance.Data.yearly.aveYearlyTime.Count;
        }
        aveYearlyMainMenuTime = 0;
        if (GameManager.instance.Data.yearly.aveYearlyMainMenuTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyMainMenuTime.Values) aveYearlyMainMenuTime += timespan;
            aveYearlyMainMenuTime /= GameManager.instance.Data.yearly.aveYearlyMainMenuTime.Count;
        }
        aveYearlyCollectionTime = 0;
        if (GameManager.instance.Data.yearly.aveYearlyCollectionTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyCollectionTime.Values) aveYearlyCollectionTime += timespan;
            aveYearlyCollectionTime /= GameManager.instance.Data.yearly.aveYearlyCollectionTime.Count;
        }
        aveYearlyJigsawSelectTime = 0;
        if (GameManager.instance.Data.yearly.aveYearlyJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyJigsawSelectTime.Values) aveYearlyJigsawSelectTime += timespan;
            aveYearlyJigsawSelectTime /= GameManager.instance.Data.yearly.aveYearlyJigsawSelectTime.Count;
        }
        aveYearlyTangramSelectTime = 0;
        if (GameManager.instance.Data.yearly.aveYearlyTangramSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyTangramSelectTime.Values) aveYearlyTangramSelectTime += timespan;
            aveYearlyTangramSelectTime /= GameManager.instance.Data.yearly.aveYearlyTangramSelectTime.Count;
        }
        aveYearlyColouringSelectTime = 0;
        if (GameManager.instance.Data.yearly.aveYearlyColouringSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyColouringSelectTime.Values) aveYearlyColouringSelectTime += timespan;
            aveYearlyColouringSelectTime /= GameManager.instance.Data.yearly.aveYearlyColouringSelectTime.Count;
        }
        aveYearlyInactivityPeriod = 0;
        if (GameManager.instance.Data.yearly.aveYearlyInactivityPeriod.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyInactivityPeriod.Values) aveYearlyInactivityPeriod += timespan;
            aveYearlyInactivityPeriod /= GameManager.instance.Data.yearly.aveYearlyInactivityPeriod.Count;
        }

        // Jigsaw Data
        aveYearlyJigsawTime = 0;
        if (GameManager.instance.Data.yearly.aveYearlyJigsawTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyJigsawTime.Values) aveYearlyJigsawTime += timespan;
            aveYearlyJigsawTime /= GameManager.instance.Data.yearly.aveYearlyJigsawTime.Count;
        }
        aveYearlyJigsawMovesTaken = 0;
        if (GameManager.instance.Data.yearly.aveYearlyJigsawMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyJigsawMovesTaken.Values) aveYearlyJigsawMovesTaken += count;
            aveYearlyJigsawMovesTaken /= GameManager.instance.Data.yearly.aveYearlyJigsawMovesTaken.Count;
        }
        aveYearlyJigsawErrorsMade = 0;
        if (GameManager.instance.Data.yearly.aveYearlyJigsawErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyJigsawErrorsMade.Values) aveYearlyJigsawErrorsMade += count;
            aveYearlyJigsawErrorsMade /= GameManager.instance.Data.yearly.aveYearlyJigsawErrorsMade.Count;
        }
        aveYearlyJigsawGamesPlayed = 0;
        if (GameManager.instance.Data.yearly.aveYearlyJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyJigsawGamesPlayed.Values) aveYearlyJigsawGamesPlayed += count;
            aveYearlyJigsawGamesPlayed /= GameManager.instance.Data.yearly.aveYearlyJigsawGamesPlayed.Count;
        }

        // Tangram Data
        aveYearlyTangramTime = 0;
        if (GameManager.instance.Data.yearly.aveYearlyTangramTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyTangramTime.Values) aveYearlyTangramTime += timespan;
            aveYearlyTangramTime /= GameManager.instance.Data.yearly.aveYearlyTangramTime.Count;
        }
        aveYearlyTangramMovesTaken = 0;
        if (GameManager.instance.Data.yearly.aveYearlyTangramMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyTangramMovesTaken.Values) aveYearlyTangramMovesTaken += count;
            aveYearlyTangramMovesTaken /= GameManager.instance.Data.yearly.aveYearlyTangramMovesTaken.Count;
        }
        aveYearlyTangramErrorsMade = 0;
        if (GameManager.instance.Data.yearly.aveYearlyTangramErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyTangramErrorsMade.Values) aveYearlyTangramErrorsMade += count;
            aveYearlyTangramErrorsMade /= GameManager.instance.Data.yearly.aveYearlyTangramErrorsMade.Count;
        }
        aveYearlyTangramGamesPlayed = 0;
        if (GameManager.instance.Data.yearly.aveYearlyTangramGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyTangramGamesPlayed.Values) aveYearlyTangramGamesPlayed += count;
            aveYearlyTangramGamesPlayed /= GameManager.instance.Data.yearly.aveYearlyTangramGamesPlayed.Count;
        }

        // Colouring Data
        aveYearlyColouringTime = 0;
        if (GameManager.instance.Data.yearly.aveYearlyColouringTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.yearly.aveYearlyColouringTime.Values) aveYearlyColouringTime += timespan;
            aveYearlyColouringTime /= GameManager.instance.Data.yearly.aveYearlyColouringTime.Count;
        }
        aveYearlyColouringGamesPlayed = 0;
        if (GameManager.instance.Data.yearly.aveYearlyColouringGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.yearly.aveYearlyColouringGamesPlayed.Values) aveYearlyColouringGamesPlayed += count;
            aveYearlyColouringGamesPlayed /= GameManager.instance.Data.yearly.aveYearlyColouringGamesPlayed.Count;
        }
    }
    public void CalculateAllTimeData()
    {
        // General Data
        aveTime = 0;
        if (GameManager.instance.Data.allTime.aveTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveTime.Values) aveTime += timespan;
            aveTime /= GameManager.instance.Data.allTime.aveTime.Count;
        }
        aveMainMenuTime = 0;
        if (GameManager.instance.Data.allTime.aveMainMenuTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveMainMenuTime.Values) aveMainMenuTime += timespan;
            aveMainMenuTime /= GameManager.instance.Data.allTime.aveMainMenuTime.Count;
        }
        aveCollectionTime = 0;
        if (GameManager.instance.Data.allTime.aveCollectionTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveCollectionTime.Values) aveCollectionTime += timespan;
            aveCollectionTime /= GameManager.instance.Data.allTime.aveCollectionTime.Count;
        }
        aveJigsawSelectTime = 0;
        if (GameManager.instance.Data.allTime.aveJigsawSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveJigsawSelectTime.Values) aveJigsawSelectTime += timespan;
            aveJigsawSelectTime /= GameManager.instance.Data.allTime.aveJigsawSelectTime.Count;
        }
        aveTangramSelectTime = 0;
        if (GameManager.instance.Data.allTime.aveTangramSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveTangramSelectTime.Values) aveTangramSelectTime += timespan;
            aveTangramSelectTime /= GameManager.instance.Data.allTime.aveTangramSelectTime.Count;
        }
        aveColouringSelectTime = 0;
        if (GameManager.instance.Data.allTime.aveColouringSelectTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveColouringSelectTime.Values) aveColouringSelectTime += timespan;
            aveColouringSelectTime /= GameManager.instance.Data.allTime.aveColouringSelectTime.Count;
        }
        aveInactivityPeriod = 0;
        if (GameManager.instance.Data.allTime.aveInactivityPeriod.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveInactivityPeriod.Values) aveInactivityPeriod += timespan;
            aveInactivityPeriod /= GameManager.instance.Data.allTime.aveInactivityPeriod.Count;
        }

        // Jigsaw Data
        aveJigsawTime = 0;
        if (GameManager.instance.Data.allTime.aveJigsawTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveJigsawTime.Values) aveJigsawTime += timespan;
            aveJigsawTime /= GameManager.instance.Data.allTime.aveJigsawTime.Count;
        }
        aveJigsawMovesTaken = 0;
        if (GameManager.instance.Data.allTime.aveJigsawMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveJigsawMovesTaken.Values) aveJigsawMovesTaken += count;
            aveJigsawMovesTaken /= GameManager.instance.Data.allTime.aveJigsawMovesTaken.Count;
        }
        aveJigsawErrorsMade = 0;
        if (GameManager.instance.Data.allTime.aveJigsawErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveJigsawErrorsMade.Values) aveJigsawErrorsMade += count;
            aveJigsawErrorsMade /= GameManager.instance.Data.allTime.aveJigsawErrorsMade.Count;
        }
        aveJigsawGamesPlayed = 0;
        if (GameManager.instance.Data.allTime.aveJigsawGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveJigsawGamesPlayed.Values) aveJigsawGamesPlayed += count;
            aveJigsawGamesPlayed /= GameManager.instance.Data.allTime.aveJigsawGamesPlayed.Count;
        }

        // Tangram Data
        aveTangramTime = 0;
        if (GameManager.instance.Data.allTime.aveTangramTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveTangramTime.Values) aveTangramTime += timespan;
            aveTangramTime /= GameManager.instance.Data.allTime.aveTangramTime.Count;
        }
        aveTangramMovesTaken = 0;
        if (GameManager.instance.Data.allTime.aveTangramMovesTaken.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveTangramMovesTaken.Values) aveTangramMovesTaken += count;
            aveTangramMovesTaken /= GameManager.instance.Data.allTime.aveTangramMovesTaken.Count;
        }
        aveTangramErrorsMade = 0;
        if (GameManager.instance.Data.allTime.aveTangramErrorsMade.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveTangramErrorsMade.Values) aveTangramErrorsMade += count;
            aveTangramErrorsMade /= GameManager.instance.Data.allTime.aveTangramErrorsMade.Count;
        }
        aveTangramGamesPlayed = 0;
        if (GameManager.instance.Data.allTime.aveTangramGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveTangramGamesPlayed.Values) aveTangramGamesPlayed += count;
            aveTangramGamesPlayed /= GameManager.instance.Data.allTime.aveTangramGamesPlayed.Count;
        }

        // Colouring Data
        aveColouringTime = 0;
        if (GameManager.instance.Data.allTime.aveColouringTime.Count > 0)
        {
            foreach (float timespan in GameManager.instance.Data.allTime.aveColouringTime.Values) aveColouringTime += timespan;
            aveColouringTime /= GameManager.instance.Data.allTime.aveColouringTime.Count;
        }
        aveColouringGamesPlayed = 0;
        if (GameManager.instance.Data.allTime.aveColouringGamesPlayed.Count > 0)
        {
            foreach (int count in GameManager.instance.Data.allTime.aveColouringGamesPlayed.Values) aveColouringGamesPlayed += count;
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