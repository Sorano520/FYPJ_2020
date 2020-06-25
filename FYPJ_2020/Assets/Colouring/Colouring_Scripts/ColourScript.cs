using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ColourScript : MonoBehaviour
{   
    [SerializeField]
    Texture2D texture;
    public KeyCode mouseLeft;
    Vector3 worldPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            texture.FloodFillBorder((int)Input.mousePosition.x, (int)Input.mousePosition.y, Color.red, Color.black);
            Debug.Log("Clicked2");
            Debug.Log("X = " + (int)Input.mousePosition.x);
            Debug.Log("Y = " + (int)Input.mousePosition.y);
            //texture.Apply();
            //Debug.Log("Clicked");
            //texture.FloodFillArea((int)worldPosition.x, (int)worldPosition.y, Color.red);
            //Debug.Log("Clicked2");
            //Debug.Log("Clicked");
            //ImageUtils.FloodFill(readTexture, writeTexture,Color.black, 1.0f, (int)Input.mousePosition.x, (int)Input.mousePosition.y);
            //Debug.Log("Clicked2");
            texture.Apply();
        }
    }
}
