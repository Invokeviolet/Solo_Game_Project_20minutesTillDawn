using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    static void StartGame()
    {
        GameObject.Find("Play_Button").GetComponentInChildren<Text>().text = "Play";
    }
    static void QuitGame()
    {
        GameObject.Find("Back_Button").GetComponentInChildren<Text>().text = "Back";
    }

    static public void Home() 
    {
        SceneManager.LoadScene("HomeScene");
    }
    static public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    static public void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }
    static public void Quit()
    {
        Application.Quit();
    }
}
