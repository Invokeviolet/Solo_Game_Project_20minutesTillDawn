using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;

    [SerializeField] Monster[] MonsterPrefab;
    [SerializeField] Transform playerPos;
    [SerializeField] public int MonsterCount; // ���̺꿡 ���� ���� �ٲ��� ��

    float TimeAfterSpawn; // �����ϴµ� �ɸ��� �ð�
    float SpawnRate; // ���� �������� �ɸ��� �ð�

   

    void Start()
    {
        MonsterCount = 0;
        
        SpawnRate = Random.Range(1f, 4f);

    }


    private void Update()
    {
        processSpawn();
    }


    private void processSpawn()
    {
        int randomvalue = Random.Range(0,3);

        TimeAfterSpawn += Time.deltaTime;


        //���Ͱ� ������ ��ġ
        float Xpos = Random.Range(transform.localPosition.x - 10, transform.localPosition.x + 10);
        float Ypos = Random.Range(transform.localPosition.y - 10, transform.localPosition.y + 10);

        Vector3 RandomPos = new Vector3(Xpos, Ypos, 0);
        

        if (TimeAfterSpawn>=SpawnRate) 
        {

            TimeAfterSpawn = 0f;
            Monster Mob = MonsterPooling.Instance.CreateMonster(RandomPos,MonsterPrefab[randomvalue]);

            SpawnRate= Random.Range(6f, 12f);
        }
    }

}
