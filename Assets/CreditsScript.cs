using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas canvasCredits;
    public SpriteRenderer rendererBackgroundCredits;
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        canvasCredits.enabled = true;
        canvasCredits.pixelPerfect = true;
        rendererBackgroundCredits.enabled = true;
    }
    public void DisableCanvasCredits()
    {
        canvasCredits.enabled = false;
        rendererBackgroundCredits.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
