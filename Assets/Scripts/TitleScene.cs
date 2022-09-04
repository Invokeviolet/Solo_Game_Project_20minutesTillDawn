using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 유저에게 보이는 첫 화면
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
        SceneManager.LoadScene("HomeScene", LoadSceneMode.Additive);
    }
   
    static public void Quit()
    {
        Application.Quit();
    }

    //-----------------------------------------------------------------------------------------
    // Fade In & Out 효과
    //
    #region 화면 Fade In & Out

    [SerializeField] Image image; // 판넬
    [SerializeField] GameObject PlayButton; //플레이버튼

    private bool checkbool = false;  // bool 값으로 조건을 넣어서 조건이 만족했을때 판넬 파괴

    void Awake()
    {
        PlayButton = this.gameObject; //스크립트 참조된 오브젝트
        image = PlayButton.GetComponent<Image>(); //판넬오브젝트에 이미지 참조
    }
    void Update()
    {
        StartCoroutine("WindowOpacity"); //판넬 투명도 조절

        if (checkbool==true) 
        {
            Destroy(this.gameObject); 
        }
    }

    IEnumerator WindowOpacity()
    {

        Color color = image.color; //color 에 판넬 이미지 참조

        for (int i = 100; i >= 0; i--)
        {
            color.a -= Time.deltaTime * 0.01f; // 화면이 부드럽게 전환되기 위한 이미지 알파값 조절
            image.color = color; //판넬 이미지 컬러에 바뀐 알파값 참조

            if (image.color.a <= 0) //만약 판넬 이미지 알파 값이 0보다 작으면
            {
                checkbool = true; 
            }
        }

        yield return null;  //코루틴 종료

    }
    #endregion
    //
    //-----------------------------------------------------------------------------------------
}
