using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ĳ����/����/�ó����� �����ϴ� ��
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
