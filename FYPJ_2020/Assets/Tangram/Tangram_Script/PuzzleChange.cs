using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject puzzle = Instantiate(GameManager.instance.chosenPuzzle) as GameObject;
        transform.parent = null;
        puzzle.transform.lossyScale.Set(0.7500001f, 0.7500001f, 0.7500001f);
        puzzle.transform.position.Set(1.52002f, -3.317596f, -0.5416667f);
        //GameObject puzzle = GetComponent<GameObject>().gameObject
    }

    // Update is called once per frame
    void Update()
    {

    }

}
