using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѿ� ������Ʈ Ŭ����
public class BulletObject : MonoBehaviour
{
    
    [SerializeField] public float AttackSpeed = 10.0f; // ���� �ӵ�
    
    public float BulletDamage = 20f; // �Ѿ� ������

    [SerializeField] BulletObject BulletPrefab; // ������ ���� ��������


    public Transform myTarget { get; set; }

    

    void Update()
    {
        BulletMove();

    }

    public void BulletMove()
    {
        transform.Translate(Vector3.right * AttackSpeed * Time.deltaTime); // ���ӿ�����Ʈ�� �����ϰž� (��� ����� �Ÿ� * �ð�}       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.tag == "Mob") || (collision.tag == "OutBox"))
        {            
            
            BulletPooling.Instance.DestroyBullet(BulletPrefab); // �ڱ��ڽ��� Ǯ���� �ٽ� ����
        }

    }


}