using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    void SettingKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu();
        }

    }
    static void Menu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
    
    static void Home()
    {
        SceneManager.LoadScene("GameHome");
    }
    static public void Quit()
    {
        Application.Quit();
    }
}
