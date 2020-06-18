using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using RotaryHeart.Lib.SerializableDictionary;

[System.Serializable]
public class SerializedDictionary : SerializableDictionaryBase<GameObject, bool> { }

public class JigsawPieceLogic : MonoBehaviour
{
    public enum PIECE_STATE
    {
        STATE_NONE,
        STATE_PICKEDUPONINVENTORY,
        STATE_PICKEDUP,
        STATE_PUTDOWN,
        STATE_LOCKED,
        STATE_ALL
    }
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float length;
    [SerializeField] GameObject slot;
    [SerializeField] Vector3 slotPos;
    [SerializeField] LayerMask originalLayer;
    [SerializeField] LayerMask lockedLayer;
    [SerializeField] PIECE_STATE state;
    [SerializeField] SerializedDictionary neighbours = new SerializedDictionary();
    public bool updateSortingOrder = false;
    Vector2 offset;

    #region Getters & Setters
    public PIECE_STATE State
    {
        get { return state; }
        set { state = value; }
    }
    public GameObject Slot
    {
        get { return slot; }
        set { slot = value; }
    }
    public Vector2 Offset
    {
        get { return offset; }
        set { offset = value; }
    }
    #endregion

    void Awake()
    {
        SwipeDetector.OnSwipe += JigsawPieceLogic_OnSwipe;
    }

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        length = sprite.size.x * 0.5f;
        GetComponent<SpriteMask>().sprite = sprite.sprite;
        originalLayer = gameObject.layer;
        lockedLayer = LayerMask.GetMask("Default");

        if (slot == null) slot = transform.parent.gameObject;
        if (slot) slotPos = slot.transform.position;

        state = PIECE_STATE.STATE_NONE;
        updateSortingOrder = false;
        transform.parent = InventoryLogic.instance.transform;
        offset = Vector2.zero;
        //transform.position = new Vector2(Random.Range(2, 11), Random.Range(-3.5f, 3.5f));
    }

    void Update()
    {
        switch (state)
        {
            case PIECE_STATE.STATE_PICKEDUPONINVENTORY:
                if (MouseLogic.instance.MousePos.x >= InventoryLogic.instance.onInventory.treshold) return;
                SwitchState(PIECE_STATE.STATE_PICKEDUP);
                break;
            case PIECE_STATE.STATE_PICKEDUP:
                transform.position = MouseLogic.instance.MousePos + offset;
                break;
        }
    }

    public void SwitchState(JigsawPieceLogic.PIECE_STATE newState)
    {
        state = newState;
        switch (state)
        {
            case PIECE_STATE.STATE_PICKEDUP:
                if (Mathf.Abs(transform.position.x - InventoryLogic.instance.onInventory.treshold) <= length) return;
                if (InventoryLogic.instance.Inventory.ContainsKey(gameObject))
                    InventoryLogic.instance.Inventory[gameObject] = false;
                transform.parent = slot.transform;
                InventoryLogic.instance.SortInventory();
                MouseLogic.instance.SetSortingOrder(gameObject);
                break;
            case PIECE_STATE.STATE_PUTDOWN:
                if (Vector2.Distance(transform.position, slotPos) <= 1)
                {
                    transform.position = slotPos;
                    transform.parent = slot.transform;
                    state = PIECE_STATE.STATE_LOCKED;
                    transform.tag = "Untagged";
                    gameObject.layer = lockedLayer;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                }
                else if (Mathf.Abs(transform.position.x - InventoryLogic.instance.transform.position.x) <= length)
                {
                    if (InventoryLogic.instance.Inventory.ContainsKey(gameObject))
                        InventoryLogic.instance.Inventory[gameObject] = true;
                    transform.parent = InventoryLogic.instance.transform;
                    InventoryLogic.instance.SortInventory();
                }
                InventoryLogic.instance.UpdateSortingOrder();

                if (MouseLogic.instance.SelectedPiece == gameObject) MouseLogic.instance.SelectedPiece = null;
                state = PIECE_STATE.STATE_NONE;
                break;
        }
    }
    
    private void JigsawPieceLogic_OnSwipe(SwipeData data)
    {
        if (InventoryLogic.instance.onInventory.onInventory || MouseLogic.instance.SelectedPiece != gameObject) return;
        if (state != PIECE_STATE.STATE_PICKEDUP) state = PIECE_STATE.STATE_PICKEDUP;
        transform.position = data.EndPosition;
    }
}