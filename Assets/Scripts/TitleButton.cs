using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

//��ư ��ġ�� ���콺�� �������� �� ���̴� ȿ��
public class TitleButton : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Button button;

    public void SetTargetGraphic()
    {
        button.targetGraphic = image;
    }
}
