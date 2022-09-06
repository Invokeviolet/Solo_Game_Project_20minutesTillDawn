using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] BulletObject BulletPrefab; // �Ѿ� ������
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
        if (Input.GetMouseButtonDown(0))
        {
            InstBullet();
        }
           
    }

    public void InstBullet()
    {
        TimeAfterSpawn += Time.deltaTime;
        // ���콺Ŭ�� �� �Ѿ� ���
        
            if (TimeAfterSpawn >= SpawnRate)
            {
                //GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
                BulletPooling.Instance.CreateBullet(transform.position, BulletPrefab); // �ҷ�Ǯ������ �ҷ� �����ؼ� ������. (��������ġ���� �߻�, ���� �Ѿ� ������)
                
            }
            bulletObject.Shoot();
        
    }

}