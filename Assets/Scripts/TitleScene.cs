using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// �������� ���̴� ù ȭ��
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
    // Fade In & Out ȿ��
    //
    #region ȭ�� Fade In & Out

    [SerializeField] Image image; // �ǳ�
    [SerializeField] GameObject PlayButton; //�÷��̹�ư

    private bool checkbool = false;  // bool ������ ������ �־ ������ ���������� �ǳ� �ı�

    void Awake()
    {
        PlayButton = this.gameObject; //��ũ��Ʈ ������ ������Ʈ
        image = PlayButton.GetComponent<Image>(); //�ǳڿ�����Ʈ�� �̹��� ����
    }
    void Update()
    {
        StartCoroutine("WindowOpacity"); //�ǳ� ���� ����

        if (checkbool==true) 
        {
            Destroy(this.gameObject); 
        }
    }

    IEnumerator WindowOpacity()
    {

        Color color = image.color; //color �� �ǳ� �̹��� ����

        for (int i = 100; i >= 0; i--)
        {
            color.a -= Time.deltaTime * 0.01f; // ȭ���� �ε巴�� ��ȯ�Ǳ� ���� �̹��� ���İ� ����
            image.color = color; //�ǳ� �̹��� �÷��� �ٲ� ���İ� ����

            if (image.color.a <= 0) //���� �ǳ� �̹��� ���� ���� 0���� ������
            {
                checkbool = true; 
            }
        }

        yield return null;  //�ڷ�ƾ ����

    }
    #endregion
    //
    //-----------------------------------------------------------------------------------------
}
