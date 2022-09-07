using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���콺 Ŀ�� ��ġ�� �����Ǿ� ����ٴϴ� �ؽ�Ʈ�� ���� Ŭ����
public class MouseFollowText : MonoBehaviour
{
    [SerializeField] RectTransform transform_cursor; //���� ���콺 Ŀ��
    [SerializeField] Text text_Mouse; // ������ �Ѿ� ������ ������ ��
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
        transform_cursor.pivot = Vector2.up; // ���콺Ŀ���� �Ǻ�

        if (transform_cursor.GetComponent<Graphic>())
        {
            transform_cursor.GetComponent<Graphic>().raycastTarget = false;
        } 
    }
    
    void Update_BulletCount() // �Ѿ� ������ ī��Ʈ���ִ� ��
    {

        Vector2 mouseMovePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float mouseXPos = Input.mousePosition.x;
        float mouseYPos = Input.mousePosition.y;
        string message = UIManager.Instance.MouseBulletCountText.text;

        Vector2 bulletCountPos = Camera.main.ScreenToWorldPoint(mouseMovePos);
       
        UIManager.Instance.MouseBulletCountText.transform.position = mouseMovePos + (new Vector2(80, -40)); // ������ �Ѿ� ���� ǥ�����ִ� �ؽ�Ʈ / ǥ�õ� ��ġ
               
    }

}
