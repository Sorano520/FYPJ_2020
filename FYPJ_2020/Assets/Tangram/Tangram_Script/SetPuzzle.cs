using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPuzzle : MonoBehaviour
{
    public GameObject chosenPuzzle;
    void OnEnable()
    {
        GameManager.instance.chosenPuzzle = chosenPuzzle;
    }
}
