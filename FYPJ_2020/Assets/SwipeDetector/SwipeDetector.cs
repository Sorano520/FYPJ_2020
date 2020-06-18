using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 0.1f;

    public static event Action<SwipeData> OnSwipe = delegate { };

    /// <summary>
    /// Start of MouseLogic
    /// </summary>
    public static SwipeDetector instance = null;
    [SerializeField] string tag2Compare;
    [SerializeField] LayerMask layer2Compare;
    [SerializeField] GameObject selectedPiece;
    int sortingOrder = 0;

    #region Getters & Setters
    public string Tag2Compare
    {
        get { return tag2Compare; }
        set { tag2Compare = value; }
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
        if (GameObject.Find("Main Camera") && GameObject.Find("Main Camera") != gameObject)
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

    // Start is called before the first frame update
    void Start()
    {
        tag2Compare = "Piece";
        layer2Compare = LayerMask.GetMask("Jigsaw");
        selectedPiece = null;
        sortingOrder = 0;
        InventoryLogic.instance.Inventory = new SerializedDictionary();

        GameObject[] pieces = GameObject.FindGameObjectsWithTag(tag2Compare);
        foreach (GameObject piece in pieces)
        {
            if (piece == null) continue;
            if (!piece.GetComponent<JigsawPieceLogic>()) continue;

            piece.GetComponent<SortingGroup>().sortingOrder = sortingOrder;
            ++sortingOrder;
            InventoryLogic.instance.Inventory.Add(piece, true);
            piece.transform.parent = InventoryLogic.instance.transform;
        }

        InventoryLogic.instance.SortInventory();
    }

    void FindPiece()
    {
        if (selectedPiece && selectedPiece.GetComponent<JigsawPieceLogic>().State == JigsawPieceLogic.PIECE_STATE.STATE_PICKEDUP) return;
        InventoryLogic.instance.CheckOnInventory(fingerDownPosition);

        Collider2D[] hits = Physics2D.OverlapCircleAll(fingerDownPosition, .2f, layer2Compare);
        if (hits.Length <= 0) return;

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
        if (selected == null) return;
        selectedPiece = selected.gameObject;
    }

    public void SetSortingOrder(GameObject obj)
    {
        obj.GetComponent<SortingGroup>().sortingOrder = sortingOrder;
        ++sortingOrder;
    }
    /// <summary>
    /// End of MouseLogic
    /// </summary>

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = Camera.main.ScreenToWorldPoint(touch.position);
                fingerDownPosition = Camera.main.ScreenToWorldPoint(touch.position);

                FindPiece();
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = Camera.main.ScreenToWorldPoint(touch.position);
                DetectSwipe();

                InventoryLogic.instance.CheckOnInventory(fingerDownPosition);
            }
            
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = Camera.main.ScreenToWorldPoint(touch.position);
                DetectSwipe();

                if (selectedPiece && selectedPiece.GetComponent<JigsawPieceLogic>()) selectedPiece.GetComponent<JigsawPieceLogic>().State = JigsawPieceLogic.PIECE_STATE.STATE_PUTDOWN;
                selectedPiece = null;
            }

        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };
        OnSwipe(swipeData);
    }
}

public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}