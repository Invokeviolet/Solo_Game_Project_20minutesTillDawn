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
        GameObject.Find("UI_Button").GetComponentInChildren<Text>().text = "Play";
    }
    static public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    static void QuitGame()
    {
        GameObject.Find("UI_Button").GetComponentInChildren<Text>().text = "Back";
    }
    static void Home()
    {
        SceneManager.LoadScene("TitleScene");
    }
    static public void Quit()
    {
        Application.Quit();
    }
}
