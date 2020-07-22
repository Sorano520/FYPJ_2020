using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int chosenDifficulty;
    [SerializeField] List<int> jigsawDifficulties;

    protected int timesLoggedIn;
    protected int timesLoggedInToday;
    [SerializeField] protected string displayName;

    int noOfWins;
    int noOfLosses;
    bool thisBadge;
    bool thatBadge;

    #region Getters & Setters
    public List<int> JigsawDifficulties
    {
        get { return jigsawDifficulties; }
        set { jigsawDifficulties = value; }
    }
    public int TimesLoggedIn
    {
        get { return timesLoggedIn; }
        set { timesLoggedIn = value; }
    }
    public int TimesLoggedInToday
    {
        get { return timesLoggedInToday; }
        set { timesLoggedInToday = value; }
    }
    public string DisplayName
    {
        get { return displayName; }
        set { displayName = value; }
    }
    #endregion

    void Awake()
    {
        if (GameObject.Find("GameManager") && GameObject.Find("GameManager") != gameObject)
        {
            Destroy(gameObject);
            return;
        }
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
}