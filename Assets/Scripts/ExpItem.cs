using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    //[SerializeField] Transform TargetPos;
    public int ExpValue = 10; // ����ġ ��
    public int TargetMoveSpeed = 3; // �÷��̾�Է� ���� �̵��ӵ�
    Monster monsterInfo;

    void Awake()
    {
        monsterInfo = FindObjectOfType<Monster>();
       

    }

    void Update()
    {
        //�÷��̾ ������ ������ �÷��̾�� �̵�
        //transform.Translate(transform.position* TargetMoveSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

}
