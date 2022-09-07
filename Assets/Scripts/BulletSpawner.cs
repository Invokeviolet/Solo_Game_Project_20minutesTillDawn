using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѿ� ������ - �Ѿ��� �������ֱ� ���� Ŭ����
public class BulletSpawner : MonoBehaviour
{

    [SerializeField] BulletObject bulletObject; // �ҷ� ������Ʈ�� ��ü ����

    [SerializeField] public int Maxbullet = 6; // �ִ� �Ѿ� ����
    [SerializeField] public int Curbullet = 0; // ���� �Ѿ� ����

    float TimeAfterSpawn; // �����ϴµ� �ɸ��� �ð�
    float SpawnRate; // ���� �������� �ɸ��� �ð�    
    

    UIManager uimanager;

    void Start()
    {
        uimanager = FindObjectOfType<UIManager>();
        //bulletObject = FindObjectOfType<BulletObject>(); // �ҷ�������Ʈ�� ������Ʈ�� �����Ѵ�.
        TimeAfterSpawn = 0;
        SpawnRate = 1.0f;

        Debug.Log("## Curbullet : " + Curbullet);
        Debug.Log("## Maxbullet : " + Maxbullet);
        Curbullet = Maxbullet; // ���� �Ҹ� ������ �ִ� ������ �ʱ�ȭ
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

        UIManager.Instance.BulletCount(Curbullet); // ���� �ҷ� ������ ���� ��ܿ� ǥ������ UI
        UIManager.Instance.MouseBulletCount(Curbullet); // ���� �ҷ� ������ ���콺�� ǥ������ UI
        

        if (Input.GetMouseButtonDown(0))
        {
            if (Curbullet >= 1) //�Ѿ��� �������� �߻�
            {
                BulletPooling.Instance.CreateBullet(transform.position, transform.rotation); // �ҷ�Ǯ������ �ҷ� �����ؼ� ������. (��������ġ���� �߻�, ���� �Ѿ� ������)                        

                Curbullet--; // ���� �Ѿ� ���� -1

                //Debug.Log("--Curbullet : " + Curbullet);


            }
        }
        if (Input.GetKey(KeyCode.R)) // ������ ��ư�� �� ������ ������
        {
            UIManager.Instance.bulletCheck(); // ������
        }
        if (Curbullet <= 0) // ���� �Ѿ��� 0�̰ų� �����϶� �������ð� ���Ŀ� �Ѿ� ����
        {
            Curbullet = 0; // ���� �Ҹ��� 0���� ����
            
            UIManager.Instance.bulletCheck(); // �Ҹ��� üũ�ϴ� �޼��� ����
        }
    }



}