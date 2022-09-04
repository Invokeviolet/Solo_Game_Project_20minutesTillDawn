using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

//버튼 위치에 마우스를 가져갔을 때 보이는 효과
public class TitleButton : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Button button;

    public void SetTargetGraphic()
    {
        button.targetGraphic = image;
    }
}
