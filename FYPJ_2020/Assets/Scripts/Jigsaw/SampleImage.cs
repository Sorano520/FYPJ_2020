using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SampleImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject image;

    void Start()
    {
        image = Instantiate(gameObject);
        image.transform.parent = transform.parent;
        image.transform.localPosition = Vector3.zero;
        image.transform.parent = transform;
        image.transform.localScale = new Vector3(3, 3, 3);
        image.SetActive(false);
        Destroy(image.GetComponent<SampleImage>());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.SetActive(false);
    }
}