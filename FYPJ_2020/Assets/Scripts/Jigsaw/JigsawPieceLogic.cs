using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPieceLogic : MonoBehaviour
{
    public enum PIECE_STATE
    {
        STATE_NONE,
        STATE_PICKEDUP,
        STATE_PUTDOWN,
        STATE_LOCKED,
        STATE_ALL
    }
    [SerializeField] GameObject slot;
    [SerializeField] Vector3 slotPos;
    [SerializeField] LayerMask lockedLayer;
    [SerializeField] PIECE_STATE state;
    
    #region Getters & Setters
    public PIECE_STATE State
    {
        get { return state; }
        set { state = value; }
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteMask>().sprite = GetComponent<SpriteRenderer>().sprite;
        lockedLayer = LayerMask.GetMask("Default");

        if (slot == null) slot = transform.parent.gameObject;
        if (slot) slotPos = slot.transform.position;

        state = PIECE_STATE.STATE_NONE;
        transform.position = new Vector2(Random.Range(2, 11), Random.Range(-3.5f, 3.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (state != PIECE_STATE.STATE_PUTDOWN) return;
        if (Vector2.Distance(transform.position, slotPos) > 1) return;

        transform.position = slotPos;
        state = PIECE_STATE.STATE_LOCKED;
        transform.tag = "Untagged";
        gameObject.layer = lockedLayer;
        if (MouseLogic.instance.SelectedPiece == gameObject) MouseLogic.instance.SelectedPiece = null;
    }
}
