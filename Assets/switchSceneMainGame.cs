using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class switchSceneMainGame : MonoBehaviour
{
    // Start is called before the first frame update
    private int buildIndex = 0;
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
        SceneManager.LoadScene(sceneName:"MainGame");
    }

}
