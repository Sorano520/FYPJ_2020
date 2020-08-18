using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum GAME_TYPES
{
    NONE_GAME,
    TANGRAM_GAME,
    JIGSAW_GAME,
    COLOURING_GAME
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    GameData data;

    protected int timesLoggedIn;
    protected int timesLoggedInToday;
    [SerializeField] protected string displayName;
    
    [SerializeField] int chosenDifficulty; // 0 = Easy, 1 = Medium, 2 = Hard
    [SerializeField] int chosenLevel;
    [SerializeField] Sprite chosenImg;
    [SerializeField] DirectoryInfo jigsawImgs;
    [SerializeField] List<int> jigsawDifficulties;
    public Texture chosenTexture;

    public DebugConsole con;

    int noOfWins;
    int noOfLosses;
    bool thisBadge;
    bool thatBadge;

    #region Getters & Setters
    public GameData Data
    {
        get { return data; }
        set { data = value; }
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
    public int ChosenDifficulty
    {
        get { return chosenDifficulty; }
        set { chosenDifficulty = value; }
    }
    public int ChosenLevel
    {
        get { return chosenLevel; }
        set { chosenLevel = value; }
    }
    public Sprite ChosenImg
    {
        get { return chosenImg; }
        set { chosenImg = value; }
    }
    public List<int> JigsawDifficulties
    {
        get { return jigsawDifficulties; }
        set { jigsawDifficulties = value; }
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

        DontDestroyOnLoad(gameObject);

        data = GetComponent<GameData>();
        con = GetComponent<DebugConsole>();
        chosenImg = null;
        jigsawDifficulties = new List<int> { 3, 4, 5 };
    }
}