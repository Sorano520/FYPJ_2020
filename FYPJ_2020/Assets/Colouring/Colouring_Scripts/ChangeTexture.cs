using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Material mat = GetComponent<MeshRenderer>().materials[0];
        mat.mainTexture = GameManager.instance.chosenTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
