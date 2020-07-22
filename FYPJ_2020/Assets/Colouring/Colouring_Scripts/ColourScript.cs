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
    Vector3 mousePos;

    float deltaTime;
    public float fpsText;

    // Start is called before the first frame update
    void Start()
    {

    }    
    
    // Update is called once per frame
    void Update()
    {
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            return;

        Renderer renderer = hit.collider.GetComponent<Renderer>();
        if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null || hit.collider == null)
            return;

        Texture2D tex = (Texture2D)renderer.material.mainTexture;
        Vector2 pixelUV = hit.textureCoord;
        
        if (Input.GetMouseButtonDown(0))
            tex.FloodFillBorder((int)(pixelUV.x * renderer.material.mainTexture.width), (int)(pixelUV.y * renderer.material.mainTexture.height), UnityEngine.Color.red, UnityEngine.Color.black);
        else if (Input.GetMouseButtonDown(1))
            tex.FloodFillBorder((int)(pixelUV.x * renderer.material.mainTexture.width), (int)(pixelUV.y * renderer.material.mainTexture.height), UnityEngine.Color.green, UnityEngine.Color.black);
        tex.Apply();

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText = Mathf.Ceil(fps);
    }

}
