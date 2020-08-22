using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateJigsawStars : MonoBehaviour
{
    public int level = -1;
    GameObject leftStar;
    GameObject middleStar;
    GameObject rightStar;

    public void Start()
    {
        middleStar = transform.Find("Middle Star").gameObject;
        leftStar = transform.Find("Left Star").gameObject;
        rightStar = transform.Find("Right Star").gameObject;

        if (level == -1) level = GameManager.instance.ChosenLevel;
        if (GetComponent<PopUpMenu>())
            if (GetComponent<PopUpMenu>().popUpMenu.GetComponent<ChangeImageButton_JigsawLevelSelect>())
                level = GetComponent<PopUpMenu>().popUpMenu.GetComponent<ChangeImageButton_JigsawLevelSelect>().jigsawLevel;

        if (GameManager.instance.Data.allTime.jigsawLevels.ContainsKey(level))
        {
            switch (GameManager.instance.Data.allTime.jigsawLevels[level])
            {
                case 1:
                    leftStar.GetComponent<Image>().enabled = false;
                    middleStar.transform.GetChild(0).GetComponentInChildren<Image>().enabled = false;
                    rightStar.transform.GetChild(0).GetComponentInChildren<Image>().enabled = false;
                    break;
                case 2:
                    leftStar.GetComponent<Image>().enabled = false;
                    middleStar.GetComponent<Image>().enabled = false;
                    rightStar.transform.GetChild(0).GetComponentInChildren<Image>().enabled = false;
                    break;
                case 3:
                    leftStar.GetComponent<Image>().enabled = false;
                    middleStar.GetComponent<Image>().enabled = false;
                    rightStar.GetComponent<Image>().enabled = false;
                    break;
                default:
                    leftStar.transform.GetChild(0).GetComponentInChildren<Image>().enabled = false;
                    middleStar.transform.GetChild(0).GetComponentInChildren<Image>().enabled = false;
                    rightStar.transform.GetChild(0).GetComponentInChildren<Image>().enabled = false;
                    break;
            }
        }
        else
        {
            leftStar.transform.GetChild(0).GetComponentInChildren<Image>().enabled = false;
            middleStar.transform.GetChild(0).GetComponentInChildren<Image>().enabled = false;
            rightStar.transform.GetChild(0).GetComponentInChildren<Image>().enabled = false;
        }
    }
}
