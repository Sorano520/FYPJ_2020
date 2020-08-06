using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
   public void play_sound(int s)
   {
        SoundManager.instance.s_playsound(s);
   }
}
