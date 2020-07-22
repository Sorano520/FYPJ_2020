﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        easyButtonDisable.SetActive(false);
        easyButtonEnable.SetActive(true);
        mediumButtonDisable.SetActive(true);
        mediumButtonEnable.SetActive(false);
        hardButtonDisable.SetActive(true);
        hardButtonEnable.SetActive(false);
    }

    public void clickOnEasyButton()
    {
        easyButtonDisable.SetActive(false);
        easyButtonEnable.SetActive(true);
        mediumButtonDisable.SetActive(true);
        mediumButtonEnable.SetActive(false);
        hardButtonDisable.SetActive(true);
        hardButtonEnable.SetActive(false);
    }

    public void clickOnMediumButton()
    {
        easyButtonDisable.SetActive(true);
        easyButtonEnable.SetActive(false);
        mediumButtonDisable.SetActive(false);
        mediumButtonEnable.SetActive(true);
        hardButtonDisable.SetActive(true);
        hardButtonEnable.SetActive(false);
    }

    public void clickOnHardButton()
    {
        easyButtonDisable.SetActive(true);
        easyButtonEnable.SetActive(false);
        mediumButtonDisable.SetActive(true);
        mediumButtonEnable.SetActive(false);
        hardButtonDisable.SetActive(false);
        hardButtonEnable.SetActive(true);
    }
}