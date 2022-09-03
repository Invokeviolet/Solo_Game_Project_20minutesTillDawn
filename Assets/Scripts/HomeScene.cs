using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    static public void Start()
    {
        GameObject.Find("Next Button").GetComponentInChildren<Text>().text = "Next";
        GameObject.Find("Home Button").GetComponentInChildren<Text>().text = "Home";
    }
    static public void Level1()
    {
        SceneManager.LoadScene("InGame1");
    }
    static public void Level2()
    {
        SceneManager.LoadScene("InGame2");
    }
    static public void Level3()
    {
        SceneManager.LoadScene("InGame3");
    }

    public void Home()
    {
        SceneManager.LoadScene("GameHome");
    }
}
