using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    // ���ӿ��� ���� �� ���� ����� ���� UI    
    [SerializeField] GameObject GameOverWindow;

    [SerializeField] Text TimeScore; // (00:00) -> �ð�����
    [SerializeField] Text TimeScore_Add; // (��)���� ���� 
    [SerializeField] Text MonsterScore; // (���� ���� ��) -> ���� ����
    [SerializeField] Text MonsterScore_Add; // ���� ���� ��
    [SerializeField] Text LevelScore; // (����) -> �÷��̾� ����
    [SerializeField] Text LevelScore_Add; // ���� * 10    
    [SerializeField] Text AllScore; // Earned
    [SerializeField] Text AllScore_Add; // �� ����

    // �ð� ����� ���� UI
    [SerializeField] Text TimeText;
    [SerializeField] Slider Expslider;
    [SerializeField] Image[] Heartimage;
    [SerializeField] Image GameOverImage;


    private PlayerController playerInfo;
    private Monster monster;    

    float Time_S; // �� ���
    float Time_M; // �� ���
    float Stop_Time; // �ð� ����
    float ExpValue; // ����ġ ���
    float PlusExpSliderValue;
    int curHeart = 0;
    int maxHeart = 4;

    bool isGameOver=false;

    //HUDĵ����
    //WaveText �������Ʈ ����
    //
    void UseBulletCount() //���� �Ѿ� ���� / �ִ� �Ѿ� ���� ǥ��
    {

    }

    private void Awake()
    {
        playerInfo = FindObjectOfType<PlayerController>();

        curHeart = maxHeart;

        PlusExpSliderValue = 0f;
        Stop_Time = 0;
        Time_M = 20; // ��
        Time_S = 0; // ��

        GameOverWindow.SetActive(false);
    }

    public void Update()
    {
        CheckHeart();
        Debug.Log("isGameOver" + isGameOver);
        //if ((Time_M == 0f) && (Time_S == 0f)) { RestoreTime(); }
        if (!isGameOver) { BackTime(); }

        else { RestoreTime(); }

        PlusExpSlider();
    }

    public void PlusExpSlider() // ����ġ + ���ִ� �Լ�
    {

        /*if () // ���͸� ������� ����ġ ������ ȹ�� ��
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

    public void ResetExpSlider() // ����ġ�� ���� ����ŭ ���̸� �ʱ�ȭ���ִ� �Լ�
    {

        ExpValue = 0;
    }


    public void CheckHeart() // �÷��̾ ������ �Ծ����� ���Ǵ� ��Ʈ ����
    {
        curHeart = playerInfo.curHp;

        if (curHeart <= 3)
        {
            Heartimage[curHeart].fillAmount = 0;
        }

    }

    public void BackTime() // ������ ��� UI Ÿ�̸� 
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

        //���� ��� 

        //���� �������� ��ϵ� �ð�(��)
        //���� ���� ���� ��
        //���� ���� *10
        //--------------------------
        //�� ����
    }


}
