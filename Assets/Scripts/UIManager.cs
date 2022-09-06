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
    //[SerializeField] GameObject HomeWindow;
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
    [SerializeField] Slider ExpSlider; // Exp �����̴�
    [SerializeField] Slider ReloadSlider; // Reload �����̴�
    [SerializeField] Image[] Heartimage; // ü�� �̹��� �迭    
    [SerializeField] Text TimeText; // Ÿ�̸� �ؽ�Ʈ
    [SerializeField] Text BulletCountText; // �Ѿ� ����

    [SerializeField] ExpItem expitem;


    private BulletObject bulletObject;
    private PlayerController playerInfo;
    private Monster monster;

    float Time_S; // �� ���
    float Time_M; // �� ���
    float Stop_Time; // �ð� ����

    int curBullet; // ���� �Ѿ� ����
    int maxBullet = 6; // �ִ� �Ѿ� ����


    float maxExpValue; // �����̴� �ִ�

    // ��Ʈ ����� ���� ��
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
        bulletObject = FindObjectOfType<BulletObject>();
        //expitem = FindObjectOfType<ExpItem>();
        curHeart = maxHeart;

        Time_M = 20; // ��
        Time_S = 0; // ��

        GameOverWindow.SetActive(false);
        maxExpValue = 100f; // ����ġ �ִ�
        //BulletCountText.text = bulletObject.Curbullet + "/" + bulletObject.Maxbullet;

        ExpSlider.value = 0f;

    }

    private void Start()
    {

    }


    public void Update()
    {
        CheckHeart();


        if (!isGameOver) { BackTime(); }

        else { RestoreTime(); }


        //BulletCountText.text = bulletObject.Curbullet + "/" + bulletObject.Maxbullet;
    }


    public void BulletCount(int curCount)
    {
        BulletCountText.text = ("00" + curCount + "/" + "00" + maxBullet);

    }



    public void ExpUpdate(float ExpValue) // ����ġ + ���ִ� �Լ�
    {

        //ExpSlider.value += (PlusExp /float.MaxValue)*0.1f;//����ġ -> �Ҽ����·� ��ȯ 10(�߰�����ġ)/100(�ִ� ����ġ)*0.1 ->1

        ExpSlider.value += ExpValue;

        if (ExpSlider.value >= maxExpValue)
        {
            ResetExpSlider(); // �ִ� �ʱ�ȭ ���ֱ� ���� -> �ʱ�ȭ�� �ȵ�
        }

    }

    public void ResetReloadSlider() // ���ε� �� �ʱ�ȭ
    {
        ReloadSlider.value = 0;
    }

    public void ResetExpSlider() // ����ġ�� ���� ����ŭ ���̸� �ʱ�ȭ���ִ� �Լ�
    {
        ExpSlider.value = 0f;
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
