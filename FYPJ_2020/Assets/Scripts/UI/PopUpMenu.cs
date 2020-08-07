using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PopUpMenu : MonoBehaviour
{
    public GameObject popUpMenu;
  
    public void openPopUpMenu()
    {
        if(popUpMenu != null)
        {
            popUpMenu.SetActive(true);
        }
    }

    public void closePopUpMenu()
    {
        if(popUpMenu != null)
        {
            popUpMenu.SetActive(false);
        }
    }

    public void menuOpen()
    {
        popUpMenu.transform.DOMoveY(1050, 0.5f);
        //popUpMenu.transform.DOMove(new Vector3(11, 40, 0), 1);
    }
    public void menuOpenSettings()
    {
        popUpMenu.transform.DOMoveY(-450, 0.5f);
        //popUpMenu.transform.DOMove(new Vector3(11, 40, 0), 1);
    }
    public void menuOpenColouring()
    {
        popUpMenu.transform.DOMoveY(700, 0.5f);
        //popUpMenu.transform.DOMove(new Vector3(11, 40, 0), 1);
    }
    public void menuOpenLevelSelect()
    {
        popUpMenu.transform.DOMoveY(1900, 0.5f);
        //popUpMenu.transform.DOMove(new Vector3(11, 40, 0), 1);
    }
    public void menuCloseSettings()
    {
        popUpMenu.transform.DOMoveY(750, 0.5f);
        //popUpMenu.transform.DOMove(new Vector3(11, 40, 0), 1);
    }
    public void menuCloseLevelSelect()
    {
        popUpMenu.transform.DOMoveY(3300, 1);
    }
    public void menuClose()
    {
        popUpMenu.transform.DOMoveY(2400, 1);
    }
}
