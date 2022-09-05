using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    //---------------------------------------------------------------------------------------------
    //싱글톤
    #region 싱글턴

    static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    //
    //---------------------------------------------------------------------------------------------


    // 게임오버 이후 총 점수 계산을 위한 UI    
    [SerializeField] GameObject GameOverWindow;
    [SerializeField] GameObject HomeWindow;
    //[SerializeField] GameObject MenuWindow;


    [SerializeField] Text TimeScore; // (00:00) -> 시간정보
    [SerializeField] Text TimeScore_Add; // (초)단위 점수 
    [SerializeField] Text MonsterScore; // (몬스터 잡은 수) -> 몬스터 정보
    [SerializeField] Text MonsterScore_Add; // 몬스터 잡은 수
    [SerializeField] Text LevelScore; // (레벨) -> 플레이어 정보
    [SerializeField] Text LevelScore_Add; // 레벨 * 10    
    [SerializeField] Text AllScore; // Earned
    [SerializeField] Text AllScore_Add; // 총 점수

    // 시간 계산을 위한 UI
    [SerializeField] Text TimeText; // 타이머 텍스트
    [SerializeField] Slider ExpSlider; // Exp 슬라이더
    [SerializeField] Image[] Heartimage; // 체력 이미지 배열
    //[SerializeField] Image GameOverImage; // 게임오버 이미지
    [SerializeField]ExpItem expitem;

    int maxExpValue; // 슬라이더 최댓값
   

    private PlayerController playerInfo;
    private Monster monster;

    float Time_S; // 초 계산
    float Time_M; // 분 계산
    float Stop_Time; // 시간 멈춤
    
    float PlusExpSliderValue;
    int curHeart = 0;
    int maxHeart = 4;

    bool isGameOver = false;

    //HUD캔버스
    //WaveText 빈오브젝트 생성
    //

    private void Awake()
    {
        //ExpSlider = GetComponent<Slider>();
        playerInfo = FindObjectOfType<PlayerController>();
        //expitem = FindObjectOfType<ExpItem>();
        curHeart = maxHeart;

        PlusExpSliderValue = 0f;
        Time_M = 20; // 분
        Time_S = 0; // 초

        GameOverWindow.SetActive(false);
        maxExpValue = 100; // 경험치 최댓값
    }

    private void Start()
    {
        ExpSlider.value = 0f; 
    }

    public void ExpUpdate() // 경험치 + 해주는 함수
    {        

        //ExpSlider.value += (PlusExp /float.MaxValue)*0.1f;//경험치 -> 소수형태로 변환 10(추가경험치)/100(최대 경험치)*0.1 ->1
        ExpSlider.value =  expitem.ExpValue / float.MaxValue;
        Debug.Log(expitem.ExpValue);
        if (ExpSlider.value > 100f)
        {
            ResetExpSlider();
        }

        PlusExpSliderValue = ExpSlider.value ;
    }

    void UseBulletCount() //사용된 총알 갯수 / 최대 총알 갯수 표시
    {

    }
    


    public void Update()
    {
        CheckHeart();
        Debug.Log("isGameOver" + isGameOver);
        //if ((Time_M == 0f) && (Time_S == 0f)) { RestoreTime(); }
        if (!isGameOver) { BackTime(); }

        else { RestoreTime(); }

        ExpUpdate();
        
    }

    

    public void ResetExpSlider() // 경험치가 일정 값만큼 쌓이면 초기화해주는 함수
    {
        ExpSlider.value = 0;
    }


    public void CheckHeart() // 플레이어가 데미지 입었을때 계산되는 하트 갯수
    {
        curHeart = playerInfo.curHp;

        if (curHeart <= 3)
        {
            Heartimage[curHeart].fillAmount = 0;
        }

    }

    public void BackTime() // 오른쪽 상단 UI 타이머 
    {
        if (Time_S <= 0 && Time_M <= 0)
        {

            TimeText.text = "00:00";
            isGameOver = true;
        }
        if (!isGameOver)
        {
            Time_S -= 1f * Time.deltaTime;

            if (Time_S * Time.deltaTime <= 0f)
            {
                Time_S += 60f;
                Time_M -= 1f;
            }

            if ((Time_M < 10f) && (Time_S < 10f))
            {
                TimeText.text = "0" + (int)Time_M + ":" + "0" + (int)Time_S;
            }

            if ((Time_M < 10f) && (Time_S > 10f))
            {
                TimeText.text = "0" + (int)Time_M + ":" + (int)Time_S;
            }
            if ((Time_M > 10f) && (Time_S > 10f))
            {
                TimeText.text = (int)Time_M + ":" + (int)Time_S;
            }
            if ((Time_M > 10f) && (Time_S < 10f))
            {
                TimeText.text = (int)Time_M + ":" + "0" + (int)Time_S;
            }
        }

    }
    void RestoreTime()
    {

        //Stop_Time = Time_M;

        //TimeText.text = "0" + (int)Stop_Time + ":" + "0" + (int)Stop_Time;
        TimeText.text = "00" + ":" + "00";

    }

    public void GameOver()
    {
        GameOverWindow.SetActive(true);

        //점수 계산 

        //최종 마지막에 기록된 시간(초)
        //최종 몬스터 잡은 수
        //최종 레벨 *10
        //--------------------------
        //총 점수
    }


}
