using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    public void ToTitleScreen()
    {
        FirebaseManager.instance.SignOut();
    }

    public void ToSignIn()
    {
        SceneManager.LoadScene("SignIn");
    }

    public void ToLoadingScreen()
    {
        SceneManager.LoadScene("Loading Screen");
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
    public void ToToolkitBadge()
    {
        SceneManager.LoadScene("Toolkit Badge");
    }
    public void ToToolkitJigsaw()
    {
        SceneManager.LoadScene("Toolkit Jigsaw1");
    }
    public void ToToolkitTangram()
    {
        SceneManager.LoadScene("Toolkit Tangram");
    }
    public void ToToolkitColouring()
    {
        SceneManager.LoadScene("Toolkit Colouring");
    }

    public void ToToolkitBadgeLevelSelect()
    {
        SceneManager.LoadScene("Toolkit Badge Level Select");
    }
    public void ToToolkitJigsawLevelSelect()
    {
        SceneManager.LoadScene("Toolkit Jigsaw Level Select");
    }
    public void ToToolkitTangramLevelSelect()
    {
        SceneManager.LoadScene("Toolkit Tangram Level Select");
    }
    public void ToToolkitColouringLevelSelect()
    {
        SceneManager.LoadScene("Toolkit Colouring Level Select");
    }

    public void ToToolkitBadgeGame()
    {
        SceneManager.LoadScene("Toolkit Badge Game");
    }
    public void ToToolkitJigsawGame()
    {
        SceneManager.LoadScene("Toolkit Jigsaw Game");
    }
    public void ToToolkitTangramGame()
    {
        SceneManager.LoadScene("Toolkit Tangram Game");
    }
    public void ToToolkitColouringGame()
    {
        SceneManager.LoadScene("Toolkit Colouring Game");
    }
}
