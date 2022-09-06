using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    BulletObject bulletObject; // �ҷ� ������Ʈ�� ��ü ����

    float TimeAfterSpawn; // �����ϴµ� �ɸ��� �ð�
    float SpawnRate; // ���� �������� �ɸ��� �ð�    
    public Transform mousetransform;


    void Start()
    {
        bulletObject = GetComponent<BulletObject>(); // �ҷ�������Ʈ�� ������Ʈ�� �����Ѵ�.
        TimeAfterSpawn = 0;
        SpawnRate = 1.0f;
    }
    void Update()
    {
        Vector2 lens = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // ���콺 - �÷��̾� ��ġ
        float z = Mathf.Atan2(lens.y, lens.x) * Mathf.Rad2Deg; // �ﰢ�Լ� ��� / ���� ��� �Ѱ��� z ���� �ְ�
        transform.rotation = Quaternion.Euler(0, 0, z); // ���Ϸ��� ��ȯ�ؼ� ���� ��ǥ ���� �ֱ� 
        //���Ϸ� ����ϴ� ����? (���ʹϾ��� ������̱� ������ ���Ͱ����� ��ȯ�ؼ� ���)

        InstBullet();
    }

    public void InstBullet()
    {
        // ���콺Ŭ�� �� �Ѿ� ���

        if (Input.GetMouseButtonDown(0))
        {
            BulletPooling.Instance.CreateBullet(transform.position, transform.rotation); // �ҷ�Ǯ������ �ҷ� �����ؼ� ������. (��������ġ���� �߻�, ���� �Ѿ� ������)                        
        }

    }

}