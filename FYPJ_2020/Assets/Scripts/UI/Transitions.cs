using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ToTangram()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ToJigsaw()
    {
        SceneManager.LoadScene("Jigsaw");
    }
    public void ToColouring()
    {
        SceneManager.LoadScene("New Scene");
    }
    public void ToGameSelect()
    {
        SceneManager.LoadScene("Game Select Scene");
    }
    public void ToCollection()
    {
        SceneManager.LoadScene("Collection");
    }
}
