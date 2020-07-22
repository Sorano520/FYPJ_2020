﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Rendering;
//using RotaryHeart.Lib.SerializableDictionary;

//[System.Serializable]
//public class SerializedDictionary : SerializableDictionaryBase<GameObject, bool> { }

//public class JigsawPieceLogic : MonoBehaviour
//{
//    public enum PIECE_STATE
//    {
//        STATE_NONE,
//        STATE_ONINVENTORY,
//        STATE_PICKEDUP,
//        STATE_PUTDOWN,
//        STATE_LOCKED,
//        STATE_ALL
//    }
//    [SerializeField] GameObject slot;
//    [SerializeField] LayerMask originalLayer;
//    [SerializeField] LayerMask lockedLayer;
//    [SerializeField] PIECE_STATE state;
//    [SerializeField] SerializedDictionary neighbours = new SerializedDictionary();
//    Vector2 offset;

//    #region Getters & Setters
//    public PIECE_STATE State
//    {
//        get { return state; }
//        set { state = value; }
//    }
//    public GameObject Slot
//    {
//        get { return slot; }
//        set { slot = value; }
//    }
//    public Vector2 Offset
//    {
//        get { return offset; }
//        set { offset = value; }
//    }
//    #endregion

//    private void Awake()
//    {
//        originalLayer = gameObject.layer;
//        lockedLayer = LayerMask.GetMask("Default");
//        state = PIECE_STATE.STATE_NONE;
//        offset = Vector2.zero;
//    }

//    public void CreatePiece(string name)
//    {
//        CreateSlot(name);
//        gameObject.name = "Piece ID:" + name;
//        transform.parent = InventoryLogic.instance.transform;
//        InventoryLogic.instance.Inventory.Add(gameObject, true);
//    }

//    void Update()
//    {
//        switch (state)
//        {
//            case PIECE_STATE.STATE_ONINVENTORY:
//                if (MouseLogic.instance.OnInventory) break;
//                SwitchState(PIECE_STATE.STATE_PICKEDUP);
//                break;
//            case PIECE_STATE.STATE_PICKEDUP:
//                transform.position = Vector2.Lerp(MouseLogic.instance.MousePos, MouseLogic.instance.MousePos + offset, Vector2.Distance(MouseLogic.instance.MousePos, MouseLogic.instance.MousePos + offset));
//                break;
//        }
//    }

//    void CreateSlot(string name)
//    {
//        slot = new GameObject("Slot ID:" + name) { tag = "Slot" };
//        slot.transform.parent = null;
//        slot.transform.position = transform.position;
//        slot.transform.localScale.Set(2, 2, 2);
//        slot.transform.parent = transform.parent;
//        transform.parent = slot.transform;
//        slot = transform.parent.gameObject;
//    }

//    public void SwitchState(JigsawPieceLogic.PIECE_STATE newState)
//    {
//        state = newState;
//        switch (state)
//        {
//            case PIECE_STATE.STATE_PICKEDUP:
//                if (InventoryLogic.instance.Inventory.ContainsKey(gameObject))
//                    InventoryLogic.instance.Inventory[gameObject] = false;
//                transform.parent = slot.transform;
//                transform.localScale = new Vector3(1, 1, 1) * MouseLogic.instance.pieceGenerator.PieceSize;
//                InventoryLogic.instance.SortInventory();
//                MouseLogic.instance.SetSortingOrder(gameObject);
//                break;
//            case PIECE_STATE.STATE_PUTDOWN:
//                if (Vector2.Distance(transform.position, slot.transform.position) <= 0.5f)
//                {
//                    transform.position = slot.transform.position;
//                    transform.parent = slot.transform;
//                    state = PIECE_STATE.STATE_LOCKED;
//                    transform.tag = "Untagged";
//                    gameObject.layer = lockedLayer;
//                    GetComponent<SortingGroup>().sortingOrder = 0;
//                    break;
//                }
//                else if (Camera.main.WorldToScreenPoint(transform.position).x > InventoryLogic.instance.Treshold.x)
//                {
//                    if (InventoryLogic.instance.Inventory.ContainsKey(gameObject))
//                        InventoryLogic.instance.Inventory[gameObject] = true;
//                    transform.parent = InventoryLogic.instance.transform;
//                    InventoryLogic.instance.SortInventory();
//                }
//                InventoryLogic.instance.UpdateSortingOrder();

//                if (MouseLogic.instance.SelectedPiece == gameObject) MouseLogic.instance.SelectedPiece = null;
//                state = PIECE_STATE.STATE_NONE;
//                break;
//        }
//    }
//}




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
        int rand = Random.Range(0, 2);
        if (rand == 0) transform.position = new Vector2(Random.Range(-MouseLogic.instance.SpawnZone.y, -MouseLogic.instance.SpawnZone.x), Random.Range(-MouseLogic.instance.SpawnZone.z, MouseLogic.instance.SpawnZone.z));
        else transform.position = new Vector2(Random.Range(MouseLogic.instance.SpawnZone.y, MouseLogic.instance.SpawnZone.x), Random.Range(-MouseLogic.instance.SpawnZone.z, MouseLogic.instance.SpawnZone.z));

        InventoryLogic.instance.Inventory.Add(gameObject, true);
    }

    void Update()
    {
        switch (state)
        {
            case PIECE_STATE.STATE_PICKEDUP:
                transform.position = Vector2.Lerp(MouseLogic.instance.MousePos, MouseLogic.instance.MousePos + offset, Vector2.Distance(MouseLogic.instance.MousePos, MouseLogic.instance.MousePos + offset));
                break;
        }
    }

    void CreateSlot(string name)
    {
        slot.name = "Slot ID:" + name;
        Color combinedMask = new Color(2, 2, 2, 2);
        slot.GetComponent<MeshFilter>().mesh.SetColors(new List<Color>() { combinedMask, combinedMask, combinedMask, combinedMask });
        slot.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1);
        slot.GetComponent<SortingGroup>().sortingOrder = -1;
        slot.transform.parent = null;
        slot.transform.position = transform.position;
        slot.transform.localScale *= 1.2f;
        slot.transform.parent = MouseLogic.instance.pieceGenerator.slotHolder.transform;
    }

    public void SwitchState(PIECE_STATE newState)
    {
        state = newState;
        switch (state)
        {
            case PIECE_STATE.STATE_PICKEDUP:
                InventoryLogic.instance.UpdateSortingOrder();
                MouseLogic.instance.SetSortingOrder(gameObject);
                break;
            case PIECE_STATE.STATE_PUTDOWN:
                if (Vector2.Distance(transform.position, slot.transform.position) <= 0.5f)
                {
                    transform.position = slot.transform.position;
                    state = PIECE_STATE.STATE_LOCKED;
                    transform.tag = "Untagged";
                    gameObject.layer = lockedLayer;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                    break;
                }

                InventoryLogic.instance.UpdateSortingOrder();

                if (MouseLogic.instance.SelectedPiece == gameObject) MouseLogic.instance.SelectedPiece = null;
                state = PIECE_STATE.STATE_NONE;
                break;
        }
    }
}