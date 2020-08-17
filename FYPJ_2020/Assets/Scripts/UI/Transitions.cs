using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    public string variable;
    public void ToTitleScreen()
    {
        FirebaseManager.instance.SignOut();
        SceneManager.LoadScene("Title Screen");
    }

    public void ToSignIn()
    {
        SceneManager.LoadScene("SignIn");
    }

    public void ToCaregiverMenu()
    {
        SceneManager.LoadScene("Caregiver Menu");
    }

    public void ToLoadingScreen()
    {
        SceneManager.LoadScene("Loading Screen");
    }

    public void ToTangram()
    {
        SceneManager.LoadScene("Tangram");
    }
    public void ToTangramLevel2()
    {
        SceneManager.LoadScene("Tangram Lvl 2");
    }
    public void ToTangramLevel3()
    {
        SceneManager.LoadScene("Tangram Lvl 3");
    }
    public void ToJigsaw()
    {
        SceneManager.LoadScene("Jigsaw");
    }
    //public void ToColouring()
    //{
    //    if(difficultySelector.easyButtonEnable.activeSelf == true)
    //    {
    //        SceneManager.LoadScene("Colouring");
    //    }
    //    else if(difficultySelector.mediumButtonEnable.activeSelf == true)
    //    {
    //        SceneManager.LoadScene("Colouring Medium");
    //    }
       
    //}
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

    public void ToColouring()
    {
        SceneManager.LoadScene(variable);
        
    }
}
