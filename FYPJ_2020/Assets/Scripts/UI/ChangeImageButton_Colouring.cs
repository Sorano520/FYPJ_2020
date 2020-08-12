using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ChangeImageButton_Colouring : MonoBehaviour
{
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
    void Start()
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

        if (GameObject.Find("Canvas"))
            GameObject.Find("Canvas").GetComponent<Transitions>().variable = "Colouring Hard";
    }
}
