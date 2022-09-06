using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseFollowText : MonoBehaviour
{
    [SerializeField] RectTransform transform_cursor; //메인 마우스 커서
    [SerializeField] Text text_Mouse; // 재장전 총알 갯수를 가져올 값
    [SerializeField] BulletObject bulletObj;
    int bulletCount;
    void Start()
    {
        bulletObj = GetComponent<BulletObject>();
        //bulletCount = bulletObj.Maxbullet;
        Init_Cursor();
    }

    void Update()
    {
        Update_MousePosition();
    }
    void Init_Cursor()
    {        
        transform_cursor.pivot = Vector2.up; // 마우스커서의 피봇

        if (transform_cursor.GetComponent<Graphic>())
        {
            transform_cursor.GetComponent<Graphic>().raycastTarget = false;
        }        
    }
    
    void Update_BulletCount() // 총알 갯수를 카운트해주는 애
    {
        Vector2 mouseMovePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        float mouseXPos = Input.mousePosition.x;
        float mouseYPos = Input.mousePosition.y;

        string message = bulletCount.ToString(); 
        Vector2 bullCountPos = Camera.main.ScreenToWorldPoint(mouseMovePos);
        text_Mouse.text = bullCountPos.ToString();
        text_Mouse.transform.position = mouseMovePos + (new Vector2(100, -50));//x는 양수 좌표, y는 음수 좌표
        //Debug.Log(bulletMessage);
    }

    void Update_MousePosition()
    {
        Vector2 mouseMovePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        float mouseXPos = Input.mousePosition.x;
        float mouseYPos = Input.mousePosition.y;

        /*transform_cursor.position = mousePos;
        float w = transform_cursor.rect.width;
        float h = transform_cursor.rect.height;
        transform_cursor.position = transform_cursor.position + (new Vector3(w, h) * 0.5f);*/

        string message = mouseMovePos.ToString();
        Vector2 newPos = Camera.main.ScreenToWorldPoint(mouseMovePos);
        text_Mouse.text = newPos.ToString();
        text_Mouse.transform.position = mouseMovePos + (new Vector2(45, -20));//x는 양수 좌표, y는 음수 좌표
        //Debug.Log(message);
    }
}
