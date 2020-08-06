using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    [SerializeField]
    private Image progressbar;
    // Start is called before the first frame update
    void Start()
    {
        //start async operation
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        //create an async operation
        yield return new WaitForSeconds(3.0f);
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync("Game Select Scene");

        FirebaseManager.instance.EnterGame();

        while (gameLevel.progress < 1)
        {
            //take the progress bar fill = async opertaion progress
            progressbar.fillAmount = gameLevel.progress;

            //when finished, load game scene
            yield return new WaitForEndOfFrame();
        }
    }
}
