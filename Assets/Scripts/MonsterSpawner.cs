using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;

    [SerializeField] Monster[] MonsterPrefab; // ���� �������� �迭�� ���� -> ���͸� �������� �ֱ� ����    

    float TimeAfterSpawn; // �����ϴµ� �ɸ��� �ð�
    float SpawnRate; // ���� �������� �ɸ��� �ð�

   

    void Start()
    {
        
        SpawnRate = Random.Range(1f,4f); // ���� �������� �ɸ��� �ð��� �����ϰ� ��ġ��

    }


    private void Update()
    {
        processSpawn(); //�ؿ� ������ �޼��� �����ͼ� ����
    }


    private void processSpawn()
    {
        int randomvalue = Random.Range(0,3); // �ؿ� ������ ���� �������� ������ ������� �����ϱ� ���� ��

        TimeAfterSpawn += Time.deltaTime; // �����Ҷ� �ð��� �ʴ����� ����Ͽ� ������


        //���Ͱ� ������ ��ġ
        float Xpos = Random.Range(transform.localPosition.x - 10, transform.localPosition.x + 10); // X��ġ�� ������ ��ġ�� ���
        float Ypos = Random.Range(transform.localPosition.y - 10, transform.localPosition.y + 10); // Y��ġ�� ������ ��ġ�� ���

        Vector3 RandomPos = new Vector3(Xpos, Ypos, 0); // ���� ��ġ�� ���ļ� ���ο� ��ġ�� ����
        

        if (TimeAfterSpawn>=SpawnRate) // ���� �����ð����� ���� �ֱ� �ð��� �� Ŭ��
        {

            TimeAfterSpawn = 0f; // ���� �ֱ� �ð��� 0���� �ʱ�ȭ
            Monster Mob = MonsterPooling.Instance.CreateMonster(RandomPos,MonsterPrefab[randomvalue]); // ���� Ǯ���� ���� ����

            SpawnRate = Random.Range(1f, 4f); // �����ð��� ������ ������ ����
        }
    }

}
