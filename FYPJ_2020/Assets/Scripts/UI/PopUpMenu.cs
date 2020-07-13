using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
