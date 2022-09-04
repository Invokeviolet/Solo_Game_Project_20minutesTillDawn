using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;

    [SerializeField] Monster[] MonsterPrefab;
    [SerializeField] Transform playerPos;
    [SerializeField] public int MonsterCount; // ���̺꿡 ���� ���� �ٲ��� ��

    float MonsterSpawnInterval;
    
    void Start()
    {
        MonsterCount = 0;
        MonsterSpawnInterval = 0f;
    }
    private void Update()
    {
        StartCoroutine(processSpawn());
    }
    IEnumerator processSpawn()
    {
        if (MonsterCount>20) 
        {
            MonsterCount = 20;
            yield return null;
        }

        MonsterSpawnInterval = Random.Range(3f, 10f);

        while (MonsterCount<20)
        {
            MonsterCount ++;
            yield return new WaitForSeconds(MonsterSpawnInterval); // spawnInterval ��ŭ ��ٸ��� 

            // ���͸� ���� spawnPoint �������� �����Ѵ�.
            //Instantiate(MonsterPrefab[0], transform.position, transform.rotation);

            int randomvalue = Random.Range(0,3); // int �� ������ ���� ���� ���� ����
            Monster Mob = MonsterPooling.Instance.CreateMonster(instRandomPos(), MonsterPrefab[randomvalue]);
        }
    }

    private Vector3 instRandomPos()
    {
        //Debug.Log("## �����Ȱž� ģ����?");
        float Xpos = Random.Range(playerPos.transform.position.x - 20, playerPos.transform.position.x + 20);
        float Ypos = Random.Range(playerPos.transform.position.y - 20, playerPos.transform.position.y + 20);


        if ((Xpos >= (playerPos.position.x - 15)) || (Xpos <= (playerPos.position.x + 15)) || (Ypos >= (playerPos.position.x - 15)) || (Ypos <= (playerPos.position.x + 15)))
        {
            Xpos = Random.Range(playerPos.transform.position.x - 20, playerPos.transform.position.x + 20);
            Ypos = Random.Range(playerPos.transform.position.y - 20, playerPos.transform.position.y + 20);            
        }

        Vector3 RandomPos = new Vector3(Xpos, Ypos, 0);
        return RandomPos;
    }

}
