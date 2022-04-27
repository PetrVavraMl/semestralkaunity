using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonReturnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animatorFade;
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        animatorFade.SetTrigger("onMapChange2");
        StartCoroutine(WaitForFade(1));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitForFade(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName: "Menu");
    }
}
