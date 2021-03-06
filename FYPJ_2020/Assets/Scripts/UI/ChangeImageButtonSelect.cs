﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageButtonSelect : MonoBehaviour
{
    public GameObject jigsawButtonDisable;
    public GameObject jigsawButtonEnable;
    public GameObject tangramButtonDisable;
    public GameObject tangramButtonEnable;
    public GameObject badgesButtonDisable;
    public GameObject badgesButtonEnable;
    public GameObject galleryButtonDisable;
    public GameObject galleryButtonEnable;

    public GameObject jigsawPanelEnable; 
    public GameObject tangramPanelEnable;
    public GameObject badgePanelEnable;
    public GameObject galleryPanelEnable;

    // Start is called before the first frame update
    void Start()
    {
        jigsawButtonDisable.SetActive(false);
        jigsawButtonEnable.SetActive(true);
        jigsawPanelEnable.SetActive(true);

        tangramButtonDisable.SetActive(true);
        tangramButtonEnable.SetActive(false);
        tangramPanelEnable.SetActive(false);

        badgesButtonDisable.SetActive(true);
        badgesButtonEnable.SetActive(false);
        badgePanelEnable.SetActive(false);

        galleryButtonDisable.SetActive(true);
        galleryButtonEnable.SetActive(false);
        galleryPanelEnable.SetActive(false);
    }
    public void clickOnJigsawButton()
    {
        jigsawButtonDisable.SetActive(false);
        jigsawButtonEnable.SetActive(true);
        jigsawPanelEnable.SetActive(true);

        tangramButtonDisable.SetActive(true);
        tangramButtonEnable.SetActive(false);
        tangramPanelEnable.SetActive(false);

        badgesButtonDisable.SetActive(true);
        badgesButtonEnable.SetActive(false);
        badgePanelEnable.SetActive(false);

        galleryButtonDisable.SetActive(true);
        galleryButtonEnable.SetActive(false);
        galleryPanelEnable.SetActive(false);
    }

    public void clickOnTangramButton()
    {
        jigsawButtonDisable.SetActive(true);
        jigsawButtonEnable.SetActive(false);
        jigsawPanelEnable.SetActive(false);

        tangramButtonDisable.SetActive(false);
        tangramButtonEnable.SetActive(true);
        tangramPanelEnable.SetActive(true);

        badgesButtonDisable.SetActive(true);
        badgesButtonEnable.SetActive(false);
        badgePanelEnable.SetActive(false);

        galleryButtonDisable.SetActive(true);
        galleryButtonEnable.SetActive(false);
        galleryPanelEnable.SetActive(false);
    }

    public void clickOnBadgesButton()
    {
        jigsawButtonDisable.SetActive(true);
        jigsawButtonEnable.SetActive(false);
        jigsawPanelEnable.SetActive(false);

        tangramButtonDisable.SetActive(true);
        tangramButtonEnable.SetActive(false);
        tangramPanelEnable.SetActive(false);

        badgesButtonDisable.SetActive(false);
        badgesButtonEnable.SetActive(true);
        badgePanelEnable.SetActive(true);

        galleryButtonDisable.SetActive(true);
        galleryButtonEnable.SetActive(false);
        galleryPanelEnable.SetActive(false);
    }

    public void clickOnGalleryButton()
    {
        jigsawButtonDisable.SetActive(true);
        jigsawButtonEnable.SetActive(false);
        jigsawPanelEnable.SetActive(false);

        tangramButtonDisable.SetActive(true);
        tangramButtonEnable.SetActive(false);
        tangramPanelEnable.SetActive(false);

        badgesButtonDisable.SetActive(true);
        badgesButtonEnable.SetActive(false);
        badgePanelEnable.SetActive(false);

        galleryButtonDisable.SetActive(false);
        galleryButtonEnable.SetActive(true);
        galleryPanelEnable.SetActive(true);
    }
}
