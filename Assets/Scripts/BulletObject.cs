using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField] public float ReloadTime = 1.0f; // ������ �ð�
    [SerializeField] public int Maxbullet = 6; // �ִ� �Ѿ� ����
    [SerializeField] public int Curbullet = 0; // ���� �Ѿ� ����
    [SerializeField] public float AttackSpeed = 10.0f; // ���� �ӵ�
    public float BulletDamage = 20f; // �Ѿ� ������
      

    [SerializeField] BulletObject BulletPrefab; // ������ ���� ��������


    public Transform myTarget { get; set; }

           
    private void Start()
    {        
        Curbullet = Maxbullet; // ���� �Ҹ� ������ �ִ� ������ �ʱ�ȭ
    }


    void Update()
    {
        BulletMove();

        if (Curbullet <= 0)
        {
            StartCoroutine(ReloadBullet());
            UIManager.Instance.bulletCheck(ReloadTime);
        }

    }

    public void BulletMove()
    {
        transform.Translate(Vector3.right * AttackSpeed * Time.deltaTime); // ���ӿ�����Ʈ�� �����ϰž� (��� ����� �Ÿ� * �ð�}       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.tag == "Mob") || (collision.tag == "OutBox"))
        {
            Debug.Log("���� �Ѿ� ���� : "+Curbullet);

            Curbullet--; // ���� �Ѿ� ���� -1
            UIManager.Instance.BulletCount(Curbullet);

            Debug.Log("���� �Ѿ� ���� : " + Curbullet);
            BulletPooling.Instance.DestroyBullet(BulletPrefab); // �ڱ��ڽ��� Ǯ���� �ٽ� ����

        }

    }


    IEnumerator ReloadBullet()
    {
        yield return new WaitForSeconds(ReloadTime);
    }
}