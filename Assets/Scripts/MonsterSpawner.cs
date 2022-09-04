using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;

    [SerializeField] Monster[] MonsterPrefab;
    [SerializeField] Transform playerPos;
    float MonsterSpawnInterval;
    
    void Start()
    {
        StartCoroutine(processSpawn());
        MonsterSpawnInterval = 0f;
    }

    IEnumerator processSpawn()
    {
        MonsterSpawnInterval = Random.Range(3f, 10f);
        while (true)
        {
            // spawnInterval ��ŭ ��ٸ��� 
            yield return new WaitForSeconds(MonsterSpawnInterval);

            // ���͸� ���� spawnPoint �������� �����Ѵ�.
            //Instantiate(MonsterPrefab[0], transform.position, transform.rotation);

            int randomvalue = Random.Range(0,3); // int �� ������ ���� ���� ���� ����
            Monster Mob = MonsterPooling.Instance.CreateMonster(instRandomPos(), MonsterPrefab[randomvalue]);
        }
    }

    private Vector3 instRandomPos()
    {
        //Debug.Log("## �����Ȱž� ģ����?");
        float Xpos = Random.Range(playerPos.transform.position.x - 10, playerPos.transform.position.x + 10);
        float Ypos = Random.Range(playerPos.transform.position.y - 8, playerPos.transform.position.y + 8);


        if ((Xpos >= (playerPos.position.x - 9)) || (Xpos <= (playerPos.position.x + 9)) || (Ypos >= (playerPos.position.x - 7)) || (Ypos <= (playerPos.position.x + 7)))
        {
            Xpos = Random.Range(playerPos.transform.position.x - 10, playerPos.transform.position.x + 10);
            Ypos = Random.Range(playerPos.transform.position.y - 8, playerPos.transform.position.y + 8);            
        }

        Vector3 RandomPos = new Vector3(Xpos, Ypos, 0);
        return RandomPos;
    }

}
