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
        sprite = GetComponent<SpriteRenderer>();
        length = sprite.size.x * 0.5f;
        GetComponent<SpriteMask>().sprite = sprite.sprite;
        originalLayer = gameObject.layer;
        lockedLayer = LayerMask.GetMask("Default");

        if (slot == null) slot = transform.parent.gameObject;
        if (slot) slotPos = slot.transform.position;

        state = PIECE_STATE.STATE_NONE;
        updateSortingOrder = false;
        //transform.position = new Vector2(Random.Range(2, 11), Random.Range(-3.5f, 3.5f));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PIECE_STATE.STATE_PICKEDUP:
                if (Mathf.Abs(transform.position.x - InventoryLogic.instance.transform.position.x) <= length) return;
                if (!updateSortingOrder) return;
                if (InventoryLogic.instance.Inventory.ContainsKey(gameObject))
                    InventoryLogic.instance.Inventory[gameObject] = false;
                transform.parent = slot.transform;
                InventoryLogic.instance.SortInventory();
                SwipeDetector.instance.SetSortingOrder(gameObject);
                updateSortingOrder = false;
                break;
            case PIECE_STATE.STATE_PUTDOWN:
                if (Vector2.Distance(transform.position, slotPos) <= 1)
                {
                    transform.position = slotPos;
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

                if (SwipeDetector.instance.SelectedPiece == gameObject) SwipeDetector.instance.SelectedPiece = null;
                state = PIECE_STATE.STATE_NONE;
                break;
        }
    }
}
