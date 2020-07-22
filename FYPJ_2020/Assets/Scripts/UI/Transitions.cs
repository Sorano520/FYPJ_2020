using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    //ChangeImageButton_JigsawLevelSelect example;
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
        SceneManager.LoadScene("Tangram");
    }
    public void ToJigsaw()
    {
        SceneManager.LoadScene("Jigsaw");
    }
    public void ToColouring()
    {
        SceneManager.LoadScene("Colouring");
    }
    public void ToGameSelect()
    {
        SceneManager.LoadScene("Game Select Scene");
    }
    public void ToTangramLevelSelect()
    {
        SceneManager.LoadScene("Tangram Level Select");
    }
    public void ToJigsawLevelSelect()
    {
        //if(example.easyButtonEnable == true)
        //{

        //}
        SceneManager.LoadScene("Jigsaw Level Select");
    }
    public void ToColouringLevelSelect()
    {
        SceneManager.LoadScene("Colouring Level Select");
    }
    public void ToCollection()
    {
        SceneManager.LoadScene("Collection");
    }
}
