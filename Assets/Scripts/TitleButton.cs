using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

//버튼 위치에 마우스를 가져갔을 때 보이는 효과
public class TitleEnterButton : MonoBehaviour, IPointerEnterHandler
{
    SpriteRenderer colorSprite;
    [SerializeField] Transform _Button = null;
    private void Awake()
    {
        colorSprite = GetComponent<SpriteRenderer>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Up Event
        colorSprite.material.color = Color.red;
    }

}

public class TitleExitButton : MonoBehaviour, IPointerExitHandler
{
    SpriteRenderer colorSprite;
    [SerializeField] Transform _Button = null;

    //Vector3 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private void Awake()
    {
        colorSprite = GetComponent<SpriteRenderer>();        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Exit Event
        colorSprite.material.color = Color.white;
    }

    
}
