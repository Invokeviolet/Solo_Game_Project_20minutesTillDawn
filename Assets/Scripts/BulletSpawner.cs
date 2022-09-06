using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    
    public GameObject mousetransform; // ������ ���� ������Ʈ �ޱ�
    BulletObject bulletObject; // �ҷ� ������Ʈ�� ��ü ����

    float TimeAfterSpawn; // �����ϴµ� �ɸ��� �ð�
    float SpawnRate; // ���� �������� �ɸ��� �ð�

    void Start()
    {
        bulletObject = GetComponent<BulletObject>(); // �ҷ�������Ʈ�� ������Ʈ�� �����Ѵ�.
        TimeAfterSpawn = 0;
        SpawnRate = 1.0f;
    }
    void Update()
    {
        InstBullet();
    }

    public void InstBullet()
    {
        // ���콺Ŭ�� �� �Ѿ� ���

        if (Input.GetMouseButtonDown(0))
        {
            //GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            BulletPooling.Instance.CreateBullet(transform.position); // �ҷ�Ǯ������ �ҷ� �����ؼ� ������. (��������ġ���� �߻�, ���� �Ѿ� ������)            
        }
    }

}