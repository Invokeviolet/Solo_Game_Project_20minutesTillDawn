using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
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


    /* [SerializeField] Text TimeScore; // (00:00) -> �ð�����
     [SerializeField] Text TimeScore_Add; // (��)���� ���� 
     [SerializeField] Text MonsterScore; // (���� ���� ��) -> ���� ����
     [SerializeField] Text MonsterScore_Add; // ���� ���� ��
     [SerializeField] Text AllScore; // Earned
     [SerializeField] Text AllScore_Add; // �� ����*/
    [SerializeField] Text LevelScore; // (����) -> �÷��̾� ����
    [SerializeField] Text LevelAdd_text; // ���� * 10    

    //Exp
    [SerializeField] Slider ExpSlider; // Exp �����̴�
    [SerializeField] ExpItem expitem;

    //ü�¹�
    [SerializeField] Image[] Heartimage; // ü�� �̹��� �迭    

    //Ÿ�̸�
    [SerializeField] Text TimeText; // Ÿ�̸� �ؽ�Ʈ

    //�Ѿ�
    public float ReloadTime; // ������ �ð�
    [SerializeField] public Slider ReloadSlider; // Reload �����̴�
    [SerializeField] Text BulletCountText; // �Ѿ� ����
    [SerializeField] public Text MouseBulletCountText; // �Ѿ� ����

    private BulletSpawner bulletspawner;
    private BulletObject bulletObject;
    private PlayerController playerInfo;
    private Monster monster;

    float Time_S; // �� ���
    float Time_M; // �� ���
    float Stop_Time; // �ð� ����


    float maxExpValue; // �����̴� �ִ�

    // ��Ʈ ����� ���� ��
    int curHeart = 0;
    int maxHeart = 4;

    bool isGameOver = false;

    //[SerializeField] Animator reloadBullet; //������ �ִϸ��̼� ��������    
    bool isReload = false;
    int LevelUp; // ���� ����� ��

    private void Awake()
    {
        //reloadBullet = FindObjectOfType<Animator>();
        //ExpSlider = GetComponent<Slider>();
        playerInfo = FindObjectOfType<PlayerController>();
        bulletObject = FindObjectOfType<BulletObject>();
        bulletspawner = FindObjectOfType<BulletSpawner>();
        //expitem = FindObjectOfType<ExpItem>();
        curHeart = maxHeart;

        Time_M = 20; // ��
        Time_S = 0; // ��

        GameOverWindow.SetActive(false);
        ReloadSlider.gameObject.SetActive(false); // �����̴� ��Ȱ��ȭ
        maxExpValue = 100f; // ����ġ �ִ�

        ExpSlider.value = 0f;
        ReloadSlider.value = 0f;

        ReloadTime = 1.0f;

        LevelUp = 1;
    }

    private void Start()
    {
        ReloadSlider.value = 1f; // ������ �ð��� 1���� 0���� ������

    }


    public void Update()
    {
        CheckHeart();

        LevelUpdate();

        if (!isGameOver) { BackTime(); }

        else { RestoreTime(); }
    }


    public void bulletCheck() // �ҷ������� 0�����϶� �������� üũ�ϴ� ��
    {

        ReloadTime -= Time.deltaTime; // �������ð��� ���� �ٿ�����
        //Debug.Log("ReloadTime : " + ReloadTime);

        ReloadSlider.gameObject.SetActive(true);
        ReloadSlider.value = ReloadTime; // �Ѿ��� ���� üũ�ϴ� ���� ��� UI -> �����̴��� �ð����� ���� ���

        //isReload = true;
        //reloadBullet.SetBool("isReload", isReload);

        if (ReloadTime <= 0f) // ������ �ð��� 0�� �Ǹ�
        {
            //isReload = false; // ������ �ִϸ��̼�
            //reloadBullet.SetBool("isReload", isReload);

            ReloadTime = 1; // ���ε� Ÿ���� �ٽ� �ʱ�ȭ ����.
            ReloadSlider.gameObject.SetActive(false);

            if (bulletspawner.Curbullet <= 0) // ���� �Ѿ��� 0�̰ų� �����϶� �������ð� ���Ŀ� �Ѿ� ����
            {
                bulletspawner.Curbullet = bulletspawner.Maxbullet;
            }
            Debug.Log("ReloadTime : " + ReloadTime);
        }

    }

    public void BulletCount(int curCount) // �Ѿ��� ���� üũ�ϴ� ���� ��� UI
    {

        BulletCountText.text = ("00" + curCount + "/" + "00" + bulletspawner.Maxbullet); //�ҷ� ������ �ؽ�Ʈ�� ǥ��

    }
    public void MouseBulletCount(int curCount) // �Ѿ��� ���� üũ�ϴ� ���� ��� UI
    {

        MouseBulletCountText.text = "" + curCount; //�ҷ� ������ �ؽ�Ʈ�� ǥ��

    }

    public void ExpUpdate(float ExpValue) // ����ġ + ���ִ� �Լ�
    {
        ExpSlider.value += ExpValue;
    }


    public void LevelUpdate() // ������ �ޱ� ���� ��
    {
        LevelAdd_text.text = "" + LevelUp;
        if (ExpSlider.value >= maxExpValue)
        {
            
            Debug.Log("Level : " + LevelAdd_text.text);
            ++LevelUp;
            ExpSlider.value = 0f;
        }
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

        Time.timeScale = 0;


        //���� ��� 

        //���� �������� ��ϵ� �ð�(��)
        //���� ���� ���� ��
        //���� ���� *10
        //--------------------------
        //�� ����
    }


}
