using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MouseLogic : MonoBehaviour
{
    public static MouseLogic instance = null;
    [Tooltip("The value is a bool which indicates whether the piece is in the inventory")]
    [SerializeField] List<GameObject> inventory;
    [SerializeField] int lockedPieces = 0;
    [SerializeField] int totalPieces = 0;
    public PieceGenerator pieceGenerator;
    [SerializeField] Vector2 mousePos;
    [SerializeField] Vector2 prevMousePos;
    [SerializeField] Vector2 minMax; // min max position the board can be at
    [SerializeField] Vector2 minMaxSize; // min = x, max = y
    [SerializeField] Vector3 spawnZone; // min position the board can be at
    [SerializeField] GameObject selectedPiece;
    int sortingOrder = 0;
    Vector2 offset;

    /// <FOR FPS>
    float deltaTime;
    public float fpsText;
    /// </FOR FPS>

    #region Getters & Setters
    public List<GameObject> Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }
    public int LockedPieces
    {
        get { return lockedPieces; }
        set { lockedPieces = value; }
    }
    public int TotalPieces
    {
        get { return totalPieces; }
        set { totalPieces = value; }
    }
    public Vector2 MousePos
    {
        get { return mousePos; }
        set { mousePos = value; }
    }
    public Vector2 MinMax
    {
        get { return minMax; }
        set { minMax = value; }
    }
    public Vector2 MinMaxSize
    {
        get { return minMaxSize; }
        set { minMaxSize = value; }
    }
    public Vector3 SpawnZone
    {
        get { return spawnZone; }
        set { spawnZone = value; }
    }
    public GameObject SelectedPiece
    {
        get { return selectedPiece; }
        set { selectedPiece = value; }
    }
    public int SortingOrder
    {
        get { return sortingOrder; }
        set { sortingOrder = value; }
    }
    #endregion

    void Awake()
    {
        if (GameObject.Find("JigsawCenter") && GameObject.Find("JigsawCenter") != gameObject)
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
        pieceGenerator = GetComponent<PieceGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ++GameManager.instance.Data.jigsawGamesPlayed;
        GameManager.instance.Data.currentGame = GAME_TYPES.JIGSAW_GAME;
        selectedPiece = null;
        sortingOrder = 0;
        lockedPieces = 0;

        GameManager.instance.Data.jigsawTime.Add(0f);
        GameManager.instance.Data.jigsawMovesTaken.Add(0);
        GameManager.instance.Data.jigsawErrorsMade.Add(0);
    }

    // Update is called once per frame
    void Update()
    {
        /// <FOR FPS>
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText = Mathf.Ceil(fps);
        /// </FOR FPS>

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touches.Length <= 0) return;
        else if (Input.touchCount == 2 && selectedPiece == null)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            float prev = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            float curr = (firstTouch.position - secondTouch.position).magnitude;
            float diff = curr - prev;
            Zoom(diff * 0.01f);
        }
        else
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    prevMousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    FindPiece(mousePos);
                    break;
                case TouchPhase.Moved:
                    prevMousePos = mousePos;
                    mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    if (selectedPiece == null) OnMove(prevMousePos, mousePos);
                    break;
                case TouchPhase.Ended:
                    if (selectedPiece && selectedPiece.GetComponent<JigsawPieceLogic>()) selectedPiece.GetComponent<JigsawPieceLogic>().SwitchState(JigsawPieceLogic.PIECE_STATE.STATE_PUTDOWN);
                    selectedPiece = null;
                    break;
            }
        }
#else
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = mousePos;
            FindPiece(mousePos);
        }
        else if (Input.GetMouseButton(0))
        {
            if (selectedPiece == null) OnMove(prevMousePos, mousePos);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (selectedPiece && selectedPiece.GetComponent<JigsawPieceLogic>()) selectedPiece.GetComponent<JigsawPieceLogic>().SwitchState(JigsawPieceLogic.PIECE_STATE.STATE_PUTDOWN);
            selectedPiece = null;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0) Zoom(Input.GetAxis("Mouse ScrollWheel"));
        prevMousePos = mousePos;
#endif
    }

    bool FindPiece(Vector2 pos)
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(pos);
        if (hits.Length <= 0) return false;

        GameObject selected = null;
        int order = int.MinValue;
        foreach (Collider2D hit in hits)
        {
            if (hit == null) continue;
            if (!hit.GetComponent<JigsawPieceLogic>()) continue;
            if (hit.transform.GetComponent<JigsawPieceLogic>().State == JigsawPieceLogic.PIECE_STATE.STATE_LOCKED) continue;
            if (hit.GetComponent<SortingGroup>().sortingOrder < order) continue;

            order = hit.GetComponent<SortingGroup>().sortingOrder;
            selected = hit.gameObject;
        }

        if (selected == null) return false;
        selectedPiece = selected.gameObject;
        JigsawPieceLogic piece = selectedPiece.GetComponent<JigsawPieceLogic>();
        piece.Offset = (Vector2)selectedPiece.transform.position - pos;
        piece.SwitchState(JigsawPieceLogic.PIECE_STATE.STATE_PICKEDUP);
        return true;
    }
    
    public void UpdateSortingOrder()
    {
        ++sortingOrder;

        foreach (GameObject piece in inventory)
        {
            if (!piece) continue;

            SetSortingOrder(piece);
        }
    }

    public void SetSortingOrder(GameObject obj)
    {
        obj.GetComponent<SortingGroup>().sortingOrder = sortingOrder;
        ++sortingOrder;
    }

    bool CheckPickedUp()
    {
        if (selectedPiece == null) return false;
        if (!selectedPiece.GetComponent<JigsawPieceLogic>()) return false;
        if (selectedPiece.GetComponent<JigsawPieceLogic>().State != JigsawPieceLogic.PIECE_STATE.STATE_PICKEDUP) return false;
        return true;
    }

    public void Zoom(float zoomDist)
    {
        //Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomDist, minMaxSize.x, minMaxSize.y);
    }

    public void OnMove(Vector2 prev, Vector2 curr)
    {
        //Vector2 newPos = (Vector2)Camera.main.transform.position + curr - prev;

        //if (newPos.x < -minMax.x * zoomDiff) newPos.x = -minMax.x * zoomDiff;
        //else if (newPos.x > minMax.x * zoomDiff) newPos.x = minMax.x * zoomDiff;
        //if (newPos.y < -minMax.y * zoomDiff) newPos.y = -minMax.y * zoomDiff;
        //else if (newPos.y > minMax.y * zoomDiff) newPos.y = minMax.y * zoomDiff;

        //Camera.main.transform.position = new Vector3(newPos.x, newPos.y, Camera.main.transform.position.z);
    }

    public void CheckComplete()
    {
        ++lockedPieces;
        if (lockedPieces >= totalPieces)
        {
            // Victory Screen Pop-Ups
            Debug.Log("You Win!");
        }
    }
}