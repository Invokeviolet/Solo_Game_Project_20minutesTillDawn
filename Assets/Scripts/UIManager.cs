using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    //---------------------------------------------------------------------------------------------
    //�̱���
    #region �̱���

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


    // ���ӿ��� ���� �� ���� ����� ���� UI    
    [SerializeField] GameObject GameOverWindow;
    [SerializeField] GameObject HomeWindow;
    //[SerializeField] GameObject MenuWindow;


    [SerializeField] Text TimeScore; // (00:00) -> �ð�����
    [SerializeField] Text TimeScore_Add; // (��)���� ���� 
    [SerializeField] Text MonsterScore; // (���� ���� ��) -> ���� ����
    [SerializeField] Text MonsterScore_Add; // ���� ���� ��
    [SerializeField] Text LevelScore; // (����) -> �÷��̾� ����
    [SerializeField] Text LevelScore_Add; // ���� * 10    
    [SerializeField] Text AllScore; // Earned
    [SerializeField] Text AllScore_Add; // �� ����

    // �ð� ����� ���� UI
    [SerializeField] Text TimeText; // Ÿ�̸� �ؽ�Ʈ
    [SerializeField] Slider ExpSlider; // Exp �����̴�
    [SerializeField] Image[] Heartimage; // ü�� �̹��� �迭
    //[SerializeField] Image GameOverImage; // ���ӿ��� �̹���
    [SerializeField]ExpItem expitem;

    int maxExpValue; // �����̴� �ִ�
   

    private PlayerController playerInfo;
    private Monster monster;

    float Time_S; // �� ���
    float Time_M; // �� ���
    float Stop_Time; // �ð� ����
    
    float PlusExpSliderValue;
    int curHeart = 0;
    int maxHeart = 4;

    bool isGameOver = false;

    //HUDĵ����
    //WaveText �������Ʈ ����
    //

    private void Awake()
    {
        //ExpSlider = GetComponent<Slider>();
        playerInfo = FindObjectOfType<PlayerController>();
        //expitem = FindObjectOfType<ExpItem>();
        curHeart = maxHeart;

        PlusExpSliderValue = 0f;
        Time_M = 20; // ��
        Time_S = 0; // ��

        GameOverWindow.SetActive(false);
        maxExpValue = 100; // ����ġ �ִ�
    }

    private void Start()
    {
        ExpSlider.value = 0f; 
    }

    public void ExpUpdate() // ����ġ + ���ִ� �Լ�
    {        

        //ExpSlider.value += (PlusExp /float.MaxValue)*0.1f;//����ġ -> �Ҽ����·� ��ȯ 10(�߰�����ġ)/100(�ִ� ����ġ)*0.1 ->1
        ExpSlider.value =  expitem.ExpValue / float.MaxValue;
        Debug.Log(expitem.ExpValue);
        if (ExpSlider.value > 100f)
        {
            ResetExpSlider();
        }

        PlusExpSliderValue = ExpSlider.value ;
    }

    void UseBulletCount() //���� �Ѿ� ���� / �ִ� �Ѿ� ���� ǥ��
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

    

    public void ResetExpSlider() // ����ġ�� ���� ����ŭ ���̸� �ʱ�ȭ���ִ� �Լ�
    {
        ExpSlider.value = 0;
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

        //���� ��� 

        //���� �������� ��ϵ� �ð�(��)
        //���� ���� ���� ��
        //���� ���� *10
        //--------------------------
        //�� ����
    }


}
