using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    [SerializeField] ExpItem expItemPrefab;
    public int ExpValue = 10; // ����ġ ��
    public int TargetMoveSpeed = 3; // �̵��ӵ�
    [SerializeField]Transform monsterInfo; //  �������� �ش� (������)���ͷ� �̵���Ű�� ���� ������ ��ǥ���� �޾ƿ�

    void Awake()
    {
       
    }

    void Update()
    {
        //�÷��̾ ������ ������ �÷��̾�� �̵�
       // transform.Translate(monsterInfo.position* TargetMoveSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("�Ծ���?");
            gameObject.SetActive(false);
            ItemPool.Instance.DestroyItem(expItemPrefab);
        }
    }

}
