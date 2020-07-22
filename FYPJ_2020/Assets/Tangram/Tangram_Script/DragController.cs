using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class DragController : MonoBehaviour {	   

    List<Draggable> dragging = new List<Draggable>();
    float lastRotationTime = 1;
    float rotationDelay = 2f;

    public GameObject rotateLeftButton;
    public GameObject rotateRightButton;
    public PieceSet PieceSet { get; set; }

    Touch touch;
    Vector2 prevMousePos, mousePos;


    void Awake()
    {
        rotateLeftButton.GetComponent<Button>().onClick.AddListener(RotatePiecesCounterClockwise);
        rotateRightButton.GetComponent<Button>().onClick.AddListener(RotatePiecesClockwise);
    }

	void Update () 
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    var hits = Physics2D.OverlapPointAll(worldPosition);
        //    foreach (var hit in hits)
        //    {
        //        if (hit != null)
        //        {
        //            var draggable = hit.GetComponent<Draggable>();
        //            if (draggable != null)
        //            {
        //                draggable.StartDrag(worldPosition);
        //                PinOthers(draggable);
        //                dragging.Add(draggable);
        //                break;
        //            }
        //        }
        //    }
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    foreach (var draggable in dragging)
        //    {
        //        draggable.EndDrag();
        //        PinThis(draggable);
        //    }
        //    dragging.Clear();
        //}

        //      if (Input.touchCount > 0)
        //      {
        //          Touch touch = Input.GetTouch(0);
        //          var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //          var hits = Physics2D.OverlapPointAll(worldPosition);
        //          foreach (var hit in hits)
        //          {
        //              if (hit != null)
        //              {
        //                  var draggable = hit.GetComponent<Draggable>();
        //                  if (draggable != null)
        //                  {
        //                      draggable.StartDrag(worldPosition);
        //                      PinOthers(draggable);
        //                      dragging.Add(draggable);
        //                      break;
        //                  }
        //              }
        //          }

        //          if(Input.touchCount == 2)
        //          {
        //              touch = Input.GetTouch(1);
        //          }
        //      }
        //      if(Input.touchCount <= 0)
        //      {
        //          foreach (var draggable in dragging)
        //          {
        //              draggable.EndDrag();
        //              PinThis(draggable);
        //          }
        //          dragging.Clear();
        //      }

        Touch touch = Input.touches[0];
        switch (touch.phase)
        {
            case TouchPhase.Began:
                prevMousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                var hits = Physics2D.OverlapPointAll(mousePos);
                foreach (var hit in hits)
                {
                    if (hit != null)
                    {
                        var draggable = hit.GetComponent<Draggable>();
                        if (draggable != null)
                        {
                            draggable.StartDrag(mousePos);
                            PinOthers(draggable);
                            dragging.Add(draggable);
                            break;
                        }
                    }
                }
                break;
            case TouchPhase.Moved:
                prevMousePos = mousePos;
                mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                foreach (Draggable obj in dragging)
                {
                    obj.Move(mousePos);
                }
                break;
            case TouchPhase.Ended:
                foreach (var draggable in dragging)
                {
                    draggable.EndDrag();
                    PinThis(draggable);
                }
                dragging.Clear();
                break;
        }

        if (Input.GetKeyDown (KeyCode.X)) {
            RotatePiecesCounterClockwise();
        } else if (Input.GetKey(KeyCode.X)){
            if (Time.time - lastRotationTime > rotationDelay)
            {
                RotatePiecesCounterClockwise();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            RotatePiecesClockwise();
        } else if (Input.GetKey(KeyCode.Z)){
            if (Time.time - lastRotationTime > rotationDelay)
            {
                RotatePiecesClockwise();
            }

        }
	}

    public void RotatePiecesClockwise(){
        lastRotationTime = Time.time;
        foreach (var draggable in dragging) {
            draggable.RotateClockwise ();
        }
    }

    public void RotatePiecesCounterClockwise(){
        lastRotationTime = Time.time;
        foreach (var draggable in dragging) {
            draggable.RotateCounterClockwise ();
        }
    }

	void PinOthers(Draggable dragged){
        foreach (var piece in PieceSet.Pieces){
            if (piece.GetInstanceID() == dragged.GetInstanceID())
            {
                piece.Draggable.Unpin();
            } else
            {
                piece.Draggable.Pin();
            }
		}
	}

    void PinThis(Draggable dropped){
        foreach (var piece in PieceSet.Pieces){
            if (piece.GetInstanceID() == dropped.GetInstanceID())
            {
                piece.Draggable.Pin();
            } else
            {
                piece.Draggable.Unpin();
            }

        }
    }
}
