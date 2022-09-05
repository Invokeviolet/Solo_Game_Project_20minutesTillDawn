using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text TimeText;
    [SerializeField] Slider Expslider;
    [SerializeField] Image[] Heartimage;
    private PlayerController playerInfo;

    float Time_S; // 초 계산
    float Time_M; // 분 계산
    float Stop_Time; // 시간 멈춤
    float ExpValue; // 경험치 계산
    float PlusExpSliderValue;
    int curHeart = 0;
    int maxHeart = 4;

    bool isGameOver=false;

    //HUD캔버스
    //WaveText 빈오브젝트 생성
    //
    // 마우스 커서 오른쪽 아래, 왼쪽 상단 총알
    void UseBulletCount() //사용된 총알 갯수 / 최대 총알 갯수 표시
    {

    }

    private void Awake()
    {
        playerInfo = FindObjectOfType<PlayerController>();

        curHeart = maxHeart;

        PlusExpSliderValue = 0f;
        Stop_Time = 0;
        Time_M = 0; // 분
        Time_S = 3; // 초
    }

    public void Update()
    {
        CheckHeart();

        //if ((Time_M == 0f) && (Time_S == 0f)) { RestoreTime(); }
        if (!isGameOver) { RestoreTime(); }

        else { BackTime(); }

        PlusExpSlider();
    }

    public void PlusExpSlider() // 경험치 + 해주는 함수
    {

        /*if () // 몬스터를 잡았을때 경험치 아이템 획득 시
        {
            ExpValue += playerInfo.plusExpPoint;
            Debug.Log("## ExpValue : " + ExpValue);

            if (ExpValue > 100f)
            {
                ResetExpSlider();
            }

            PlusExpSliderValue = Expslider.value * Time.deltaTime;
        }*/

    }

    public void ResetExpSlider() // 경험치가 일정 값만큼 쌓이면 초기화해주는 함수
    {

        ExpValue = 0;
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
        if (Time_S <= 0&& Time_M <= 0)
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
            /*if ((Time_M > 10f) && (Time_S > 10f))
            {
                TimeText.text = (int)Time_M + ":" + (int)Time_S;
            }*/
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

    void GameOver() 
    { 
        
    }



}
