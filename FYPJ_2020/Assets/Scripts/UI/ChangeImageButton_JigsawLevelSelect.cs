using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeImageButton_JigsawLevelSelect : MonoBehaviour
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
    }

    public void clickOnEasyButton()
    {
        GameManager.instance.chosenDifficulty = 0;
        easyButtonDisable.SetActive(false);
        easyButtonEnable.SetActive(true);

        mediumButtonDisable.SetActive(true);
        mediumButtonEnable.SetActive(false);

        hardButtonDisable.SetActive(true);
        hardButtonEnable.SetActive(false);

        easyPicEnable.SetActive(true);
        mediumPicEnable.SetActive(false);
        hardPicEnable.SetActive(false);
    }

    public void clickOnMediumButton()
    {
        GameManager.instance.chosenDifficulty = 1;
        easyButtonDisable.SetActive(true);
        easyButtonEnable.SetActive(false);

        mediumButtonDisable.SetActive(false);
        mediumButtonEnable.SetActive(true);

        hardButtonDisable.SetActive(true);
        hardButtonEnable.SetActive(false);

        easyPicEnable.SetActive(false);
        mediumPicEnable.SetActive(true);
        hardPicEnable.SetActive(false);
    }

    public void clickOnHardButton()
    {
        GameManager.instance.chosenDifficulty = 2;
        easyButtonDisable.SetActive(true);
        easyButtonEnable.SetActive(false);

        mediumButtonDisable.SetActive(true);
        mediumButtonEnable.SetActive(false);

        hardButtonDisable.SetActive(false);
        hardButtonEnable.SetActive(true);

        easyPicEnable.SetActive(false);
        mediumPicEnable.SetActive(false);
        hardPicEnable.SetActive(true);
    }
}
