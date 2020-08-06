using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    public List<Text> logs;
    public GameObject text;
    
    // Start is called before the first frame update
    void Awake()
    {
        logs = new List<Text>();
    }

    private void OnLevelWasLoaded(int level)
    {
        logs.Clear();
    }

    public void Log(string msg)
    {
        GameObject newGO = Instantiate(text, GameObject.Find("Canvas").transform) as GameObject;
        logs.Add(newGO.GetComponent<Text>());
        newGO.name = "Log " + logs.Count;
        newGO.transform.position = new Vector3(0, 0, 0);
        newGO.GetComponent<Text>().text = msg;
        newGO.GetComponent<Text>().color = Color.black;

        if (logs.Count > 0)
        {
            for (int i = 1; i < logs.Count; ++i)
            {
                logs[i].transform.position = new Vector3(logs[i].transform.position.x, logs[i - 1].transform.position.y + 16, logs[i].transform.position.z);
            }
        }
    }

    public void LogWarning(string msg)
    {
        GameObject newGO = Instantiate(text, GameObject.Find("Canvas").transform) as GameObject;
        logs.Add(newGO.GetComponent<Text>());
        newGO.name = "Log " + logs.Count;
        newGO.transform.position = new Vector3(0, 0, 0);
        newGO.GetComponent<Text>().text = msg;
        newGO.GetComponent<Text>().color = Color.yellow;

        if (logs.Count > 0)
        {
            for (int i = 1; i < logs.Count; ++i)
            {
                logs[i].transform.position = new Vector3(logs[i].transform.position.x, logs[i - 1].transform.position.y + 16, logs[i].transform.position.z);
            }
        }
    }

    public void LogError(string msg)
    {
        GameObject newGO = Instantiate(text, GameObject.Find("Canvas").transform) as GameObject;
        logs.Add(newGO.GetComponent<Text>());
        newGO.name = "Log " + logs.Count;
        newGO.transform.position = new Vector3(0, 0, 0);
        newGO.GetComponent<Text>().text = msg;
        newGO.GetComponent<Text>().color = Color.red;

        if (logs.Count > 0)
        {
            for (int i = 1; i < logs.Count; ++i)
            {
                logs[i].transform.position = new Vector3(logs[i].transform.position.x, logs[i - 1].transform.position.y + 16, logs[i].transform.position.z);
            }
        }
    }
}
