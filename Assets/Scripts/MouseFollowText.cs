using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 마우스 커서 위치에 생성되어 따라다니는 텍스트를 위한 클래스
public class MouseFollowText : MonoBehaviour
{
    [SerializeField] RectTransform transform_cursor; //메인 마우스 커서
    [SerializeField] Text text_Mouse; // 재장전 총알 갯수를 가져올 값
    [SerializeField] BulletObject bulletObj;
    UIManager uiManager;

    int bulletCount;
    void Start()
    {
        bulletObj = GetComponent<BulletObject>();
        //bulletCount = bulletObj.Maxbullet;
        Init_Cursor();
    }

    void Update()
    {
        Update_BulletCount();
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
        string message = UIManager.Instance.MouseBulletCountText.text;

        Vector2 bulletCountPos = Camera.main.ScreenToWorldPoint(mouseMovePos);
       
        UIManager.Instance.MouseBulletCountText.transform.position = mouseMovePos + (new Vector2(80, -40)); // 재장전 총알 갯수 표시해주는 텍스트 / 표시될 위치
               
    }

}
