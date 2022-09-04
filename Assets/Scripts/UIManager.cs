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

    float GetBackTime = 1200;
    float ExpValue;
    int curHeart = 0;
    int maxHeart = 4;

    //HUD캔버스
    //WaveText 빈오브젝트 생성
    //
    //사용된 총알 갯수 / 최대 총알 갯수 표시되는 곳 -> 마우스 커서 오른쪽 아래, 왼쪽 상단 총알


    private void Awake()
    {
        playerInfo = FindObjectOfType<PlayerController>();

        curHeart = maxHeart;

    }

    public void Update()
    {
        CheckHeart();
        BackTime();
    }

    public void PlusExpSlider() // 경험치 + 해주는 함수
    {
        ExpValue += playerInfo.ExpPoint;
    }

    public void ResetExpSlider() // 경험치가 일정 값만큼 쌓이면 초기화해주는 함수
    {
        ExpValue = 0;
    }


    public void CheckHeart() 
    {
        curHeart = playerInfo.curHp;

        if (curHeart<=3) 
        {
            Heartimage[curHeart].fillAmount = 0;
        }
        
    }

    public void BackTime() 
    {
        GetBackTime -= Time.deltaTime;
        //int getTime = (int)GetBackTime;

        TimeText.text = ""+(int)GetBackTime; // 시계 수정 필요
    }



}
