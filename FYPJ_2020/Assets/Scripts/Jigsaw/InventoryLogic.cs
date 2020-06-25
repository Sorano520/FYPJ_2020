using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public struct OnInventory
{
    public float treshold;
    public bool onInventory;
}

public class InventoryLogic : MonoBehaviour
{
    public static InventoryLogic instance = null;
    [Tooltip("The value is a bool which indicates whether the piece is in the inventory")]
    [SerializeField] SerializedDictionary inventory;
    [Tooltip("The distance between each piece in the inventory")]
    [SerializeField] float inventoryOffset;
    [SerializeField] float length;
    public OnInventory onInventory;
    float minY;
    public float maxY;

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
        onInventory.treshold = transform.parent.GetComponent<SpriteRenderer>().bounds.min.x;
        Debug.Log(onInventory.treshold);
        onInventory.onInventory = false;
        minY = maxY = transform.position.y;
    }

    public void SortInventory()
    {
        Vector2 pos = transform.position;
        int inventoryPieces = 0;
        foreach (KeyValuePair<GameObject, bool> piece in inventory)
        {
            if (!piece.Value) continue;

            piece.Key.transform.position = pos;
            pos.y -= inventoryOffset;
            ++inventoryPieces;
        }
        maxY = inventoryOffset * (inventoryPieces - 2.5f);

        UpdateSortingOrder();
    }

    public void UpdateSortingOrder()
    {
        transform.parent.GetComponent<SortingGroup>().sortingOrder = MouseLogic.instance.SortingOrder;
        ++MouseLogic.instance.SortingOrder;

        foreach (KeyValuePair<GameObject, bool> piece in inventory)
        {
            if (!piece.Value) continue;

            MouseLogic.instance.SetSortingOrder(piece.Key);
        }
    }

    public void CheckOnInventory(Vector2 pos)
    {
        if (pos.x > onInventory.treshold) onInventory.onInventory = true;
        else if (onInventory.onInventory) onInventory.onInventory = false;
    }

    public void OnMove(Vector2 prev, Vector2 curr)
    {
        if (!onInventory.onInventory) return;

        float newY = transform.position.y + (curr.y - prev.y);
        if (newY < minY) newY = minY;
        else if (newY > maxY) newY = maxY;
        transform.position = new Vector2(transform.position.x, newY);
    }
}