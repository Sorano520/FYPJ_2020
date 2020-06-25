using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MouseLogic : MonoBehaviour
{
    public static MouseLogic instance = null;
    [SerializeField] Vector2 mousePos;
    [SerializeField] Vector2 prevMousePos;
    [SerializeField] string tag2Compare;
    [SerializeField] LayerMask layer2Compare;
    [SerializeField] GameObject selectedPiece;
    int sortingOrder = 0;
    Vector2 offset;

    #region Getters & Setters
    public Vector2 MousePos
    {
        get { return mousePos; }
        set { mousePos = value; }
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
        if (GameObject.Find("Center") && GameObject.Find("Center") != gameObject)
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

        tag2Compare = "Piece";
        layer2Compare = LayerMask.GetMask("Jigsaw");
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedPiece = null;
        sortingOrder = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (!CheckPickedUp())
            InventoryLogic.instance.CheckOnInventory(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = mousePos;
            FindPiece(mousePos);
        }
        if (Input.GetMouseButton(0))
        {
            InventoryLogic.instance.OnMove(prevMousePos, mousePos);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (selectedPiece && selectedPiece.GetComponent<JigsawPieceLogic>()) selectedPiece.GetComponent<JigsawPieceLogic>().SwitchState(JigsawPieceLogic.PIECE_STATE.STATE_PUTDOWN);
            selectedPiece = null;
        }
        prevMousePos = mousePos;
    }
    
    void FindPiece(Vector2 pos)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, .2f);//, layer2Compare);
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
        JigsawPieceLogic piece = selectedPiece.GetComponent<JigsawPieceLogic>();
        piece.Offset = (Vector2)selectedPiece.transform.position - pos;
        piece.SwitchState(JigsawPieceLogic.PIECE_STATE.STATE_ONINVENTORY);
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
}