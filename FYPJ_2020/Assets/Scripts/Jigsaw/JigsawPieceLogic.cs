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
        STATE_ONINVENTORY,
        STATE_PICKEDUP,
        STATE_PUTDOWN,
        STATE_LOCKED,
        STATE_ALL
    }
    [SerializeField] GameObject slot;
    [SerializeField] Vector3 slotPos;
    [SerializeField] LayerMask originalLayer;
    [SerializeField] LayerMask lockedLayer;
    [SerializeField] PIECE_STATE state;
    [SerializeField] SerializedDictionary neighbours = new SerializedDictionary();
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

    private void Awake()
    {
        originalLayer = gameObject.layer;
        lockedLayer = LayerMask.GetMask("Default");
        state = PIECE_STATE.STATE_NONE;
        offset = Vector2.zero;
    }

    public void CreatePiece(string name)
    {
        CreateSlot(name);
        gameObject.name = "Piece ID:" + name;
        transform.parent = InventoryLogic.instance.transform;
        InventoryLogic.instance.Inventory.Add(gameObject, true);
    }

    void Update()
    {
        switch (state)
        {
            case PIECE_STATE.STATE_ONINVENTORY:
                if (MouseLogic.instance.MousePos.x >= InventoryLogic.instance.onInventory.treshold) return;
                SwitchState(PIECE_STATE.STATE_PICKEDUP);
                break;
            case PIECE_STATE.STATE_PICKEDUP:
                transform.position = MouseLogic.instance.MousePos + offset;
                break;
        }
    }

    void CreateSlot(string name)
    {
        slot = new GameObject("Slot ID:" + name)
        {
            tag = "Slot"
        };
        slot.transform.parent = null;
        slot.transform.position = transform.position;
        slot.transform.localScale.Set(2, 2, 2);
        slot.transform.parent = transform.parent;
        transform.parent = slot.transform;
        slot = transform.parent.gameObject;
        slotPos = slot.transform.position;
    }

    public void SwitchState(JigsawPieceLogic.PIECE_STATE newState)
    {
        state = newState;
        switch (state)
        {
            case PIECE_STATE.STATE_PICKEDUP:
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
                    break;
                }
                else if (transform.position.x >= InventoryLogic.instance.onInventory.treshold)
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
}