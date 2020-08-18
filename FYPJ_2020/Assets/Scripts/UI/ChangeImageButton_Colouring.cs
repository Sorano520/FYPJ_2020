using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeImageButton_Colouring : MonoBehaviour
{
    public Texture chosenTexture;
    public Texture chosenTextureMedium;
    public Texture chosenTextureHard;
    public GameObject easyButtonDisable;
    public GameObject easyButtonEnable;
    public GameObject mediumButtonDisable;
    public GameObject mediumButtonEnable;
    public GameObject hardButtonDisable;
    public GameObject hardButtonEnable;
    public GameObject easyPicEnable;
    public GameObject mediumPicEnable;
    public GameObject hardPicEnable;

    // Start is called before the first frame update
    void OnEnable()
    {
        easyButtonDisable.SetActive(false);
        easyButtonEnable.SetActive(true);

        mediumButtonDisable.SetActive(true);
        mediumButtonEnable.SetActive(false);

        hardButtonDisable.SetActive(true);
        hardButtonEnable.SetActive(false);

        easyPicEnable.SetActive(true);
        mediumPicEnable.SetActive(false);
        hardPicEnable.SetActive(false);

        GameManager.instance.chosenTexture = chosenTexture;
        
        if (GameObject.Find("Canvas"))
            GameObject.Find("Canvas").GetComponent<Transitions>().variable = "Colouring";

    }

    public void clickOnEasyButton()
    {
        easyButtonDisable.SetActive(false);
        easyButtonEnable.SetActive(true);

        mediumButtonDisable.SetActive(true);
        mediumButtonEnable.SetActive(false);

        hardButtonDisable.SetActive(true);
        hardButtonEnable.SetActive(false);

        easyPicEnable.SetActive(true);
        mediumPicEnable.SetActive(false);
        hardPicEnable.SetActive(false);

        GameManager.instance.chosenTexture = chosenTexture;

        if (GameObject.Find("Canvas"))
            GameObject.Find("Canvas").GetComponent<Transitions>().variable = "Colouring";
    }

    public void clickOnMediumButton()
    {
        easyButtonDisable.SetActive(true);
        easyButtonEnable.SetActive(false);

        mediumButtonDisable.SetActive(false);
        mediumButtonEnable.SetActive(true);

        hardButtonDisable.SetActive(true);
        hardButtonEnable.SetActive(false);

        easyPicEnable.SetActive(false);
        mediumPicEnable.SetActive(true);
        hardPicEnable.SetActive(false);

        GameManager.instance.chosenTexture = chosenTextureMedium;

        if (GameObject.Find("Canvas"))
            GameObject.Find("Canvas").GetComponent<Transitions>().variable = "Colouring Medium";
    }

    public void clickOnHardButton()
    {
        easyButtonDisable.SetActive(true);
        easyButtonEnable.SetActive(false);

        mediumButtonDisable.SetActive(true);
        mediumButtonEnable.SetActive(false);

        hardButtonDisable.SetActive(false);
        hardButtonEnable.SetActive(true);

        easyPicEnable.SetActive(false);
        mediumPicEnable.SetActive(false);
        hardPicEnable.SetActive(true);

        GameManager.instance.chosenTexture = chosenTextureHard;

        if (GameObject.Find("Canvas"))
            GameObject.Find("Canvas").GetComponent<Transitions>().variable = "Colouring Hard";
    }
}
