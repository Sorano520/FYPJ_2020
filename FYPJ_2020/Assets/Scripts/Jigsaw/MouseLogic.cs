using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MouseLogic : MonoBehaviour
{
    public static MouseLogic instance = null;
    [SerializeField] string tag2Compare;
    [SerializeField] LayerMask layer2Compare;
    [SerializeField] GameObject selectedPiece;
    int sortingOrder = 0;
    Vector2 offset;

    #region Getters & Setters
    public GameObject SelectedPiece
    {
        get { return selectedPiece; }
        set { selectedPiece = value; }
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

        GameObject[] pieces = GameObject.FindGameObjectsWithTag(tag2Compare);
        foreach (GameObject piece in pieces)
        {
            if (piece == null) continue;
            if (!piece.GetComponent<JigsawPieceLogic>()) continue;

            piece.GetComponent<SortingGroup>().sortingOrder = sortingOrder;
            ++sortingOrder;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) FindPiece();
        else if (Input.GetMouseButtonUp(0))
        {
            if (selectedPiece && selectedPiece.GetComponent<JigsawPieceLogic>()) selectedPiece.GetComponent<JigsawPieceLogic>().State = JigsawPieceLogic.PIECE_STATE.STATE_PUTDOWN;
            selectedPiece = null;
        }
        if (selectedPiece)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.position = mousePos + offset;
        }
    }

    void FindPiece()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, .2f, layer2Compare);
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
        selectedPiece.GetComponent<JigsawPieceLogic>().State = JigsawPieceLogic.PIECE_STATE.STATE_PICKEDUP;
        selectedPiece.GetComponent<SortingGroup>().sortingOrder = sortingOrder;
        ++sortingOrder;
        offset = (Vector2) selectedPiece.transform.position - pos;
    }
}
