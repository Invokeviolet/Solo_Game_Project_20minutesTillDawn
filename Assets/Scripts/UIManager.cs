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

    //HUDĵ����
    //WaveText �������Ʈ ����
    //
    //���� �Ѿ� ���� / �ִ� �Ѿ� ���� ǥ�õǴ� �� -> ���콺 Ŀ�� ������ �Ʒ�, ���� ��� �Ѿ�


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

    public void PlusExpSlider() // ����ġ + ���ִ� �Լ�
    {
        ExpValue += playerInfo.ExpPoint;
    }

    public void ResetExpSlider() // ����ġ�� ���� ����ŭ ���̸� �ʱ�ȭ���ִ� �Լ�
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

        TimeText.text = ""+(int)GetBackTime; // �ð� ���� �ʿ�
    }



}
