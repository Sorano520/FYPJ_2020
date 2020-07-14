using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MouseLogic : MonoBehaviour
{
    public static MouseLogic instance = null;
    public PieceGenerator pieceGenerator;
    [SerializeField] Vector2 mousePos;
    [SerializeField] Vector2 prevMousePos;
    [SerializeField] Vector2 min; // min position the board can be at
    [SerializeField] Vector2 max; // max position the board can be at
    [SerializeField] Vector2 minMaxSize; // min = x, max = y
    [SerializeField] LayerMask layer2Compare;
    [SerializeField] GameObject selectedPiece;
    int sortingOrder = 0;
    Vector2 offset;
    bool onInventory;

    /// <FOR FPS>
    float deltaTime;
    public float fpsText;
    /// </FOR FPS>

    #region Getters & Setters
    public Vector2 MousePos
    {
        get { return mousePos; }
        set { mousePos = value; }
    }
    public Vector2 MIN
    {
        get { return min; }
        set { min = value; }
    }
    public Vector2 MAX
    {
        get { return max; }
        set { max = value; }
    }
    public Vector2 MINMAXSize
    {
        get { return minMaxSize; }
        set { minMaxSize = value; }
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
    public bool OnInventory
    {
        get { return onInventory; }
        set { onInventory = value; }
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
        layer2Compare = LayerMask.GetMask("Jigsaw");
        onInventory = false;
        pieceGenerator = GetComponent<PieceGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedPiece = null;
        sortingOrder = 0;
        InventoryLogic.instance.transform.parent.parent = Camera.main.transform;
        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        InventoryLogic.instance.transform.parent.parent = null;
        InventoryLogic.instance.SetTreshold();
    }

    // Update is called once per frame
    void Update()
    {
        /// <FOR FPS>
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText = Mathf.Ceil(fps);
        /// </FOR FPS>
        
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touches.Length <= 0) return;
        else if (Input.touchCount == 2 && selectedPiece == null && Input.touches[0].position.x < InventoryLogic.instance.Treshold.x)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            float prev = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            float curr = (firstTouch.position - secondTouch.position).magnitude;
            float diff = curr - prev;
            Zoom(diff * 0.01f);
        }
        else
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    prevMousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    onInventory = (Input.touches[0].position.x > InventoryLogic.instance.Treshold.x) ? true : false;
                    FindPiece(mousePos);
                    break;
                case TouchPhase.Moved:
                    prevMousePos = mousePos;
                    mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    if (!CheckPickedUp()) onInventory = (Input.touches[0].position.x > InventoryLogic.instance.Treshold.x) ? true : false;
                    else onInventory = false;
                    if (onInventory) InventoryLogic.instance.OnMove(prevMousePos, mousePos);
                    else if (selectedPiece == null) OnMove (prevMousePos, mousePos);
                    break;
                case TouchPhase.Ended:
                    onInventory = false;
                    if (selectedPiece && selectedPiece.GetComponent<JigsawPieceLogic>()) selectedPiece.GetComponent<JigsawPieceLogic>().SwitchState(JigsawPieceLogic.PIECE_STATE.STATE_PUTDOWN);
                    selectedPiece = null;
                    break;
            }
        }
#else
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!CheckPickedUp()) onInventory = (Input.mousePosition.x > InventoryLogic.instance.Treshold.x) ? true : false;
        else onInventory = false;
        
        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = mousePos;
            FindPiece(mousePos);

        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
        if (Input.GetMouseButton(0))
        {
            if (onInventory) InventoryLogic.instance.OnMove(prevMousePos, mousePos);
            else if (selectedPiece == null) OnMove(prevMousePos, mousePos);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (selectedPiece && selectedPiece.GetComponent<JigsawPieceLogic>()) selectedPiece.GetComponent<JigsawPieceLogic>().SwitchState(JigsawPieceLogic.PIECE_STATE.STATE_PUTDOWN);
            selectedPiece = null;
        }
        prevMousePos = mousePos;
#endif
    }

    bool FindPiece(Vector2 pos)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, .2f);//, layer2Compare);
        if (hits.Length <= 0) return false;

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

        if (selected == null) return false;
        selectedPiece = selected.gameObject;
        JigsawPieceLogic piece = selectedPiece.GetComponent<JigsawPieceLogic>();
        piece.Offset = (Vector2)selectedPiece.transform.position - pos;
        piece.SwitchState(JigsawPieceLogic.PIECE_STATE.STATE_ONINVENTORY);
        return true;
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

    public void Zoom(float zoomDist)
    {
        zoomDist = Mathf.Clamp(transform.localScale.x - zoomDist, minMaxSize.x, minMaxSize.y);
        transform.localScale = new Vector3(zoomDist, zoomDist, transform.localScale.z);
    }

    public void OnMove(Vector2 prev, Vector2 curr)
    {
        Vector2 newPos = (Vector2)transform.position + curr - prev;
        if (newPos.x < min.x) newPos.x = min.x;
        else if (newPos.x > max.x) newPos.x = max.x;
        if (newPos.y < min.y) newPos.y = min.y;
        else if (newPos.y > max.y) newPos.y = max.y;
        transform.position = new Vector2(newPos.x, newPos.y);
    }
}




//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Rendering;

//public class MouseLogic : MonoBehaviour
//{
//    public static MouseLogic instance = null;
//    public PieceGenerator pieceGenerator;
//    [SerializeField] Vector2 mousePos;
//    [SerializeField] Vector2 prevMousePos;
//    [SerializeField] Vector2 min; // min position the board can be at
//    [SerializeField] Vector2 max; // max position the board can be at
//    [SerializeField] Vector2 minMaxSize; // min = x, max = y
//    [SerializeField] GameObject selectedPiece;
//    int sortingOrder = 0;
//    Vector2 offset;
//    bool onInventory;

//    /// <FOR FPS>
//    float deltaTime;
//    public float fpsText;
//    /// </FOR FPS>

//    #region Getters & Setters
//    public Vector2 MousePos
//    {
//        get { return mousePos; }
//        set { mousePos = value; }
//    }
//    public Vector2 MIN
//    {
//        get { return min; }
//        set { min = value; }
//    }
//    public Vector2 MAX
//    {
//        get { return max; }
//        set { max = value; }
//    }
//    public Vector2 MINMAXSize
//    {
//        get { return minMaxSize; }
//        set { minMaxSize = value; }
//    }
//    public GameObject SelectedPiece
//    {
//        get { return selectedPiece; }
//        set { selectedPiece = value; }
//    }
//    public int SortingOrder
//    {
//        get { return sortingOrder; }
//        set { sortingOrder = value; }
//    }
//    public bool OnInventory
//    {
//        get { return onInventory; }
//        set { onInventory = value; }
//    }
//    #endregion

//    void Awake()
//    {
//        if (GameObject.Find("JigsawCenter") && GameObject.Find("JigsawCenter") != gameObject)
//        {
//            Destroy(gameObject);
//            return;
//        }
//        if (instance == null)
//            instance = this;
//        else if (instance != this)
//        {
//            Destroy(gameObject);
//            return;
//        }
//        onInventory = false;
//        pieceGenerator = GetComponent<PieceGenerator>();
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        selectedPiece = null;
//        sortingOrder = 0;
//        //InventoryLogic.instance.transform.parent.parent = Camera.main.transform;
//        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
//        //InventoryLogic.instance.transform.parent.parent = null;
//        //InventoryLogic.instance.SetTreshold();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        /// <FOR FPS>
//        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
//        float fps = 1.0f / deltaTime;
//        fpsText = Mathf.Ceil(fps);
//        /// </FOR FPS>

//#if UNITY_ANDROID || UNITY_IOS
//        if (Input.touches.Length <= 0) return;
//        else if (Input.touchCount == 2 && selectedPiece == null)// && Input.touches[0].position.x < InventoryLogic.instance.Treshold.x)
//        {
//            Touch firstTouch = Input.GetTouch(0);
//            Touch secondTouch = Input.GetTouch(1);

//            Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
//            Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

//            float prev = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
//            float curr = (firstTouch.position - secondTouch.position).magnitude;
//            float diff = curr - prev;
//            Zoom(diff * 0.01f);
//        }
//        else
//        {
//            Touch touch = Input.touches[0];
//            switch (touch.phase)
//            {
//                case TouchPhase.Began:
//                    prevMousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
//                    mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
//                    //onInventory = (Input.touches[0].position.x > InventoryLogic.instance.Treshold.x) ? true : false;
//                    FindPiece(mousePos);
//                    break;
//                case TouchPhase.Moved:
//                    prevMousePos = mousePos;
//                    mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
//                    //if (!CheckPickedUp()) onInventory = (Input.touches[0].position.x > InventoryLogic.instance.Treshold.x) ? true : false;
//                    //else onInventory = false;
//                    //if (onInventory) InventoryLogic.instance.OnMove(prevMousePos, mousePos);
//                    else if (selectedPiece == null) OnMove(prevMousePos, mousePos);
//                    break;
//                case TouchPhase.Ended:
//                    //onInventory = false;
//                    if (selectedPiece && selectedPiece.GetComponent<JigsawPieceLogic>()) selectedPiece.GetComponent<JigsawPieceLogic>().SwitchState(JigsawPieceLogic.PIECE_STATE.STATE_PUTDOWN);
//                    selectedPiece = null;
//                    break;
//            }
//        }
//#else
//        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        //if (!CheckPickedUp()) onInventory = (Input.mousePosition.x > InventoryLogic.instance.Treshold.x) ? true : false;
//        //else onInventory = false;

//        if (Input.GetMouseButtonDown(0))
//        {
//            prevMousePos = mousePos;
//            FindPiece(mousePos);

//        }
//        Zoom(Input.GetAxis("Mouse ScrollWheel"));
//        if (Input.GetMouseButton(0))
//        {
//            //if (onInventory) InventoryLogic.instance.OnMove(prevMousePos, mousePos);
//            if (selectedPiece == null) OnMove(prevMousePos, mousePos);
//        }
//        else if (Input.GetMouseButtonUp(0))
//        {
//            if (selectedPiece && selectedPiece.GetComponent<JigsawPieceLogic>()) selectedPiece.GetComponent<JigsawPieceLogic>().SwitchState(JigsawPieceLogic.PIECE_STATE.STATE_PUTDOWN);
//            selectedPiece = null;
//        }
//        prevMousePos = mousePos;
//#endif
//    }

//    bool FindPiece(Vector2 pos)
//    {
//        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, .2f);//, layer2Compare);
//        if (hits.Length <= 0) return false;

//        GameObject selected = null;
//        int order = int.MinValue;
//        foreach (Collider2D hit in hits)
//        {
//            if (hit == null) continue;
//            if (!hit.GetComponent<JigsawPieceLogic>()) continue;
//            if (hit.transform.GetComponent<JigsawPieceLogic>().State == JigsawPieceLogic.PIECE_STATE.STATE_LOCKED) continue;
//            if (hit.GetComponent<SortingGroup>().sortingOrder < order) continue;

//            order = hit.GetComponent<SortingGroup>().sortingOrder;
//            selected = hit.gameObject;
//        }

//        if (selected == null) return false;
//        selectedPiece = selected.gameObject;
//        JigsawPieceLogic piece = selectedPiece.GetComponent<JigsawPieceLogic>();
//        piece.Offset = (Vector2)selectedPiece.transform.position - pos;
//        piece.SwitchState(JigsawPieceLogic.PIECE_STATE.STATE_PICKEDUP);
//        return true;
//    }

//    public void SetSortingOrder(GameObject obj)
//    {
//        obj.GetComponent<SortingGroup>().sortingOrder = sortingOrder;
//        ++sortingOrder;
//    }

//    bool CheckPickedUp()
//    {
//        if (selectedPiece == null) return false;
//        if (!selectedPiece.GetComponent<JigsawPieceLogic>()) return false;
//        if (selectedPiece.GetComponent<JigsawPieceLogic>().State != JigsawPieceLogic.PIECE_STATE.STATE_PICKEDUP) return false;
//        return true;
//    }

//    public void Zoom(float zoomDist)
//    {
//        zoomDist = Mathf.Clamp(transform.localScale.x - zoomDist, minMaxSize.x, minMaxSize.y);
//        transform.localScale = new Vector3(zoomDist, zoomDist, transform.localScale.z);
//    }

//    public void OnMove(Vector2 prev, Vector2 curr)
//    {
//        Vector2 newPos = (Vector2)transform.position + curr - prev;
//        if (newPos.x < min.x) newPos.x = min.x;
//        else if (newPos.x > max.x) newPos.x = max.x;
//        if (newPos.y < min.y) newPos.y = min.y;
//        else if (newPos.y > max.y) newPos.y = max.y;
//        transform.position = new Vector2(newPos.x, newPos.y);
//    }
//}