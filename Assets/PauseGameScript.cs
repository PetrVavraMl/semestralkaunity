using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GetComponent<Canvas>().enabled == true)
            {
                GetComponent<Canvas>().enabled = false;
                Time.timeScale = 1;
            }
            else {
                GetComponent<Canvas>().enabled = true;
                Time.timeScale = 0;

            }
            
        }
    }

    public void CloseCanvas() { 
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
}
