using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 캐릭터/무기/시너지를 선택하는 씬
public class HomeScene : MonoBehaviour
{
    static void StartGame()
    {
        GameObject.Find("Play_Button").GetComponentInChildren<Text>().text = "Play";
    }
    static void Play()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }
    static void QuitGame()
    {
        GameObject.Find("Quit_Button").GetComponentInChildren<Text>().text = "Quit";
    }
    static void Home()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive);
    }
    static public void Quit()
    {
        Application.Quit();
    }
}
