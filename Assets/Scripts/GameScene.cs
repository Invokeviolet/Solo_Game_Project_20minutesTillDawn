using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���� ȭ�� ��
public class GameScene : MonoBehaviour
{
    void SettingKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu();
        }

    }
    static void Menu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
    static void Home()
    {
        SceneManager.LoadScene("GameHome");
    }
    static public void Quit()
    {
        Application.Quit();
    }
    /*static void LevelUp()
    {
    
    }*/
   /* static void ChestOpen()
    {
        SceneManager.ChestOpenScene("GameHome");
    }*/


/*
    // ���� �ϴ� ����
    [SerializeField] private Text timeText; //���� �ð�
    [SerializeField] private float surviveTime; //���� �ð�
    [SerializeField] private bool isGameOver; // ���ӿ��� ����
       

    // ���� ���� �� ����â ǥ��
    [SerializeField] Image ScorePanal; // ���� ���� �� ������ ǥ������ �ǳ�
    [SerializeField] GameObject PlayButton; // Ȯ�ι�ư

    public GameObject GameOver;
    private bool checkbool = false;  // bool ������ ������ �־ ������ ���������� �ǳ� �ı�

    

    void Awake()
    {
        ScorePanal = PlayButton.GetComponent<Image>(); // ���� ������ ����� �ǳ�
    }

    void Start()
    {
        surviveTime = 12000;
        isGameOver = false;
        GameOver.SetActive(false);
    }

    void Update()
    {

        //���� ������ �ƴ� ����
        if (!isGameOver)
        {
            //���� �ð� ����
            surviveTime -= Time.deltaTime;
            //������ ���� �ð��� timeText �ؽ�Ʈ ������Ʈ�� �̿��� ǥ��  
            timeText.text = " " + (int)surviveTime;
        }

        if (isGameOver) 
        {
            Invoke();
        }
        
    }
    void Invoke()
    {
        //�ǳ� ���� ����
        StartCoroutine("WindowOpacity");
        // ��ƾ �ð� ǥ��
        timeText.text = " " + (int)surviveTime;
        // ���� ����Ʈ ǥ��
        // ����Ʈ ��� = ��ƾ �ð� (��) + ���� ���� �� + �ְ� ���� = Earned ����Ʈ
        // ����Ʈ�� �����Ǹ�, ����Ʈ�� ĳ���� �Ǵ� ���� �ر� ���� (������ ����)
    }
    public void EndGame() // �׾ ���� ��
    {
        isGameOver = true; //���� ���¸� ���ӿ��� ���·� ��ȯ

        GameOver.SetActive(true); // GameOver �ؽ�Ʈ ���

        Invoke("Invoke", 3f); // 3�� �ڿ� ���� â �����ֱ�

    }


    //-----------------------------------------------------------------------------------------
    // Fade In & Out ȿ��
    //
    #region ȭ�� Fade In & Out

    IEnumerator WindowOpacity()
    {

        Color color = ScorePanal.color; //color �� �ǳ� �̹��� ����

        for (int i = 0; i >= 255; i--)
        {
            color.a += Time.deltaTime * 0.01f; // ȭ���� �ε巴�� ��ȯ�Ǳ� ���� �̹��� ���İ� ����
            ScorePanal.color = color; //�ǳ� �̹��� �÷��� �ٲ� ���İ� ����

            if (ScorePanal.color.a >= 255) 
            {
                checkbool = true;
            }
        }

        yield return null;  //�ڷ�ƾ ����

    }
    #endregion
    //
    //-----------------------------------------------------------------------------------------*/
}
