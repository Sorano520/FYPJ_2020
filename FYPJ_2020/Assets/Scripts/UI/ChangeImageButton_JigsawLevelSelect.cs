using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageButton_JigsawLevelSelect : MonoBehaviour
{
    public Sprite jigsawImg;
    public int jigsawLevel;

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

    private void OnEnable()
    {
        GameManager.instance.ChosenImg = jigsawImg;
        GameManager.instance.ChosenLevel = jigsawLevel;
    }

    public void clickOnEasyButton()
    {
        GameManager.instance.ChosenDifficulty = 0;
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
        if (GameManager.instance.Data.allTime.jigsawLevels.ContainsKey(jigsawLevel))
            if (!GameManager.instance.Data.allTime.jigsawLevels[jigsawLevel].Contains(0)) return;

        GameManager.instance.ChosenDifficulty = 1;
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
        if (GameManager.instance.Data.allTime.jigsawLevels.ContainsKey(jigsawLevel))
            if (!GameManager.instance.Data.allTime.jigsawLevels[jigsawLevel].Contains(1)) return;

        GameManager.instance.ChosenDifficulty = 2;
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
