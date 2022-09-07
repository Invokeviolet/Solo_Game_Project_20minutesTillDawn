using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    static void StartGame()
    {
        GameObject.Find("Play_Button").GetComponentInChildren<Text>().text = "Play";
    }
    static void QuitGame()
    {
        GameObject.Find("Quit_Button").GetComponentInChildren<Text>().text = "Quit";
    }
    static void TryAgain()
    {
        GameObject.Find("TryAgain_Button").GetComponentInChildren<Text>().text = "TryAgain";
    }
    static void QuitToMenu()
    {
        GameObject.Find("QuitToMenu_Button").GetComponentInChildren<Text>().text = "QuitToMenu";
    }
    static public void Play()
    {
        //SceneManager.LoadScene("HomeScene");

        SceneManager.LoadScene("HomeScene");


        //SceneManager.LoadScene("HomeScene", LoadSceneMode.Additive);
    }

    static public void Quit()
    {
        Application.Quit();
    }
}
