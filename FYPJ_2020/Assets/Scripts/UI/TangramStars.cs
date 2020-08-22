using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangramStars : MonoBehaviour
{
    public int TangramLevel;
    public GameObject GreyStarLeft;
    public GameObject GreyStarMiddle;
    public GameObject GreyStarRight;
    public GameObject YellowStarLeft;
    public GameObject YellowStarMiddle;
    public GameObject YellowStarRight;

    public void Start()
    {
        GreyStarLeft.SetActive(true);
        GreyStarMiddle.SetActive(true);
        GreyStarRight.SetActive(true);
        YellowStarLeft.SetActive(false);
        YellowStarMiddle.SetActive(false);
        YellowStarRight.SetActive(false);
        CheckTangram();
    }
    public void CheckTangram()
    {
        if (!GameManager.instance.Data.allTime.tangramLevels.ContainsKey(TangramLevel)) GameManager.instance.Data.allTime.tangramLevels.Add(TangramLevel, 0);
       switch (GameManager.instance.Data.allTime.tangramLevels[TangramLevel])
        {
            case 1:
                GreyStarLeft.SetActive(false);
                GreyStarMiddle.SetActive(true);
                GreyStarRight.SetActive(true);
                YellowStarLeft.SetActive(true);
                YellowStarMiddle.SetActive(false);
                YellowStarRight.SetActive(false);
                break;
            case 2:
                GreyStarLeft.SetActive(false);
                GreyStarMiddle.SetActive(false);
                GreyStarRight.SetActive(true);
                YellowStarLeft.SetActive(true);
                YellowStarMiddle.SetActive(true);
                YellowStarRight.SetActive(false);
                break;
            case 3:
                GreyStarLeft.SetActive(false);
                GreyStarMiddle.SetActive(false);
                GreyStarRight.SetActive(false);
                YellowStarLeft.SetActive(true);
                YellowStarMiddle.SetActive(true);
                YellowStarRight.SetActive(true);
                break;
            default:
                break;
        }
    }
}
