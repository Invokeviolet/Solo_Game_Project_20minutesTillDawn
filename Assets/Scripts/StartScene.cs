using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 로딩 화면
public class StartScene : MonoBehaviour
{
    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Change();
        }
        
    }*/
    void Change()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive);
    }
}
