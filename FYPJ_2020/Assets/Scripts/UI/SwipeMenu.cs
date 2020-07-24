using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    float scroll_pos = 0;
    float[] pos;
    //public Animator level1Animation;
    //public Animator level2Animation;
    //public Animator level3Animation;
    //public Animator level4Animation;
    //public Animator level5Animation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            //if(scroll_pos < 0.1)
            //{
            //    level1Animation.SetBool("Level1ScrollLeft", true);
            //    level1Animation.SetBool("Level1ScrollRight", false);

            //    level2Animation.SetBool("Level2ScrollLeft", true);
            //    level2Animation.SetBool("Level2ScrollRight", false);

            //    level3Animation.SetBool("Level3ScrollLeft", true);
            //    level3Animation.SetBool("Level3ScrollRight", false);

            //    level4Animation.SetBool("Level4ScrollLeft", true);
            //    level4Animation.SetBool("Level4ScrollRight", false);

            //    level5Animation.SetBool("Level5ScrollLeft", true);
            //    level5Animation.SetBool("Level5ScrollRight", false);
            //}
            //else if(scroll_pos >0.1)
            //{
            //    level1Animation.SetBool("Level1ScrollLeft", false);
            //    level1Animation.SetBool("Level1ScrollRight", true);

            //    level2Animation.SetBool("Level2ScrollLeft", false);
            //    level2Animation.SetBool("Level2ScrollRight", true);

            //    level3Animation.SetBool("Level3ScrollLeft", false);
            //    level3Animation.SetBool("Level3ScrollRight", true);

            //    level4Animation.SetBool("Level4ScrollLeft", false);
            //    level4Animation.SetBool("Level4ScrollRight", true);

            //    level5Animation.SetBool("Level5ScrollLeft", false);
            //    level5Animation.SetBool("Level5ScrollRight", true);
            //}
            Debug.Log("scrollpos = " + scroll_pos);
            
        }
        //if (!Input.GetMouseButton(0))
        //{
        //    scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        //    Debug.Log("scrollpos = " + scroll_pos);
            
        //}
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
            //level1Animation.SetBool("Level1ScrollLeft", false);
            //level1Animation.SetBool("Level1ScrollRight", false);

            //level2Animation.SetBool("Level2ScrollLeft", false);
            //level2Animation.SetBool("Level2ScrollRight", false);

            //level3Animation.SetBool("Level3ScrollLeft", false);
            //level3Animation.SetBool("Level3ScrollRight", false);

            //level4Animation.SetBool("Level4ScrollLeft", false);
            //level4Animation.SetBool("Level4ScrollRight", false);

            //level5Animation.SetBool("Level5ScrollLeft", false);
            //level5Animation.SetBool("Level5ScrollRight", false);
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.2f, 5f), 0.1f);
                for (int j = 0; j < pos.Length; j++)
                {
                    if(j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(1f, 4f), 0.1f);
                    }
                }
            }
        }
    }
}
