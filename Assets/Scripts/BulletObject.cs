using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField] float ReloadTime = 1.0f; // ������ �ð�
    [SerializeField] public int Maxbullet = 6; // �ִ� �Ѿ� ����
    [SerializeField] public int Curbullet = 0; // ���� �Ѿ� ����
    [SerializeField] float AttackSpeed = 10.0f; // ���� �ӵ�
    public float BulletDamage = 20f; // �Ѿ� ������

    [SerializeField] Monster monster; // ���� ������ �޾ƿ� ���� ����
    [SerializeField] BulletSpawner bulletSpawner; // ������ ���� ��������
    [SerializeField] BulletObject BulletPrefab; // ������ ���� ��������

    bool isReload = false; // ������ ������?
    bool isShoot = true; // �߻����ΰ�?
    private void Start()
    {
        bulletSpawner = FindObjectOfType<BulletSpawner>(); // ������ ��ũ��Ʈ�� ã�ƿ�
        monster = FindObjectOfType<Monster>(); // ���� ��ũ��Ʈ�� ã�ƿ�        
        Curbullet = Maxbullet; // ���� �Ҹ� ������ �ִ� ������ �ʱ�ȭ
    }


    void Update()
    {

        if (Curbullet <= Maxbullet)
        {
            ReloadBullet();

            isReload = true;
        }
        else
        {
            isReload = false;
        }
    }

    public void Shoot()
    {
        //transform.Translate(Vector3.right * AttackSpeed * Time.deltaTime);
        
            gameObject.transform.Translate(Vector3.right * AttackSpeed * Time.deltaTime); // ���ӿ�����Ʈ�� �����ϰž� (��� ����� �Ÿ� * �ð�}

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.tag == "Mob") || (collision.tag == "OutBox"))
        {
            //Debug.Log("## �Ѿ� ������ ���� ��");

            Curbullet--; // ���� �Ѿ� ���� -1

            gameObject.SetActive(false); // �浹�ϸ� �ҷ� ��Ȱ��ȭ

            BulletPooling.Instance.DestroyBullet(BulletPrefab); // �ڱ��ڽ��� Ǯ���� �ٽ� ����

            gameObject.Reload(); // �Ѿ� ������
        }

    }


    IEnumerator ReloadBullet()
    {
        while (isReload == true)
        {
            yield return new WaitForSeconds(ReloadTime);
        }
    }
}