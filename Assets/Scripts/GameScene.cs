using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
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
    static void Home()
    {
        SceneManager.LoadScene("GameHome");
    }
    static public void Quit()
    {
        Application.Quit();
    }




    // 게임 하는 동안
    [SerializeField] private Text timeText; //생존 시간
    [SerializeField] private float surviveTime; //생존 시간
    [SerializeField] private bool isGameOver; // 게임오버 상태
       

    // 게임 끝난 후 점수창 표시
    [SerializeField] Image ScorePanal; // 게임 오버 후 점수를 표시해줄 판넬
    [SerializeField] GameObject PlayButton; // 확인버튼

    public GameObject GameOver;
    private bool checkbool = false;  // bool 값으로 조건을 넣어서 조건이 만족했을때 판넬 파괴

    

    void Awake()
    {
        ScorePanal = PlayButton.GetComponent<Image>(); // 게임 오버후 사용할 판넬
    }

    void Start()
    {
        surviveTime = 12000;
        isGameOver = false;
        GameOver.SetActive(false);
    }

    void Update()
    {

        //게임 오버가 아닌 동안
        if (!isGameOver)
        {
            //생존 시간 갱신
            surviveTime -= Time.deltaTime;
            //갱신한 생존 시간을 timeText 텍스트 컴포넌트를 이용해 표시  
            timeText.text = " " + (int)surviveTime;
        }

        if (isGameOver) 
        {
            Invoke();
        }
        
    }
    void Invoke()
    {
        //판넬 투명도 조절
        StartCoroutine("WindowOpacity");
        // 버틴 시간 표시
        timeText.text = " " + (int)surviveTime;
        // 얻은 포인트 표시
        // 포인트 계산 = 버틴 시간 (초) + 죽인 몬스터 수 + 최고 레벨 = Earned 포인트
        // 포인트는 누적되며, 포인트로 캐릭터 또는 무기 해금 가능 (나머지 생략)
    }
    public void EndGame() // 죽어서 게임 끝
    {
        isGameOver = true; //현재 상태를 게임오버 상태로 전환

        GameOver.SetActive(true); // GameOver 텍스트 출력

        Invoke("Invoke", 3f); // 3초 뒤에 점수 창 보여주기

    }


    //-----------------------------------------------------------------------------------------
    // Fade In & Out 효과
    //
    #region 화면 Fade In & Out

    IEnumerator WindowOpacity()
    {

        Color color = ScorePanal.color; //color 에 판넬 이미지 참조

        for (int i = 0; i >= 255; i--)
        {
            color.a += Time.deltaTime * 0.01f; // 화면이 부드럽게 전환되기 위한 이미지 알파값 조절
            ScorePanal.color = color; //판넬 이미지 컬러에 바뀐 알파값 참조

            if (ScorePanal.color.a >= 255) 
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
