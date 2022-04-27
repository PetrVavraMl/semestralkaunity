using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchSceneMainGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animatorFade;
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void TaskOnClick()
    {
        animatorFade.SetTrigger("onMapChange1");
        StartCoroutine(WaitForFade(1));
        
    }

    private IEnumerator WaitForFade(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName: "MainGame");
    }

}
