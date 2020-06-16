using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryLogic : MonoBehaviour
{
    public static InventoryLogic instance = null;
    [Tooltip("The value is a bool which indicates whether the piece is in the inventory")]
    [SerializeField] SerializedDictionary inventory;
    [Tooltip("The distance between each piece in the inventory")]
    [SerializeField] float inventoryOffset;
    [SerializeField] float length;

    #region Getters & Setters
    public SerializedDictionary Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }
    public float InventoryOffset
    {
        get { return inventoryOffset; }
        set { inventoryOffset = value; }
    }
    #endregion

    private void Awake()
    {
        if (GameObject.Find("Inventory") && GameObject.Find("Inventory") != gameObject)
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

    private void Start()
    {
        length = transform.parent.GetComponent<SpriteRenderer>().size.x;
    }
    
    public void SortInventory()
    {
        Vector2 pos = transform.position;
        foreach (KeyValuePair<GameObject, bool> piece in inventory)
        {
            if (!piece.Value) continue;

            piece.Key.transform.position = pos;
            pos.y -= inventoryOffset;
        }

        UpdateSortingOrder();
    }

    public void UpdateSortingOrder()
    {
        transform.parent.GetComponent<SortingGroup>().sortingOrder = SwipeDetector.instance.SortingOrder;
        ++SwipeDetector.instance.SortingOrder;

        foreach (KeyValuePair<GameObject, bool> piece in inventory)
        {
            if (!piece.Value) continue;

            SwipeDetector.instance.SetSortingOrder(piece.Key);
        }
    }
}
