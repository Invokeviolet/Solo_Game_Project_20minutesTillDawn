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
            // spawnInterval 만큼 기다리기 
            yield return new WaitForSeconds(MonsterSpawnInterval);

            // 몬스터를 현재 spawnPoint 기준으로 생성한다.
            //Instantiate(MonsterPrefab[0], transform.position, transform.rotation);

            int randomvalue = Random.Range(0,3); // int 는 마지막 값이 포함 되지 않음
            Monster Mob = MonsterPooling.Instance.CreateMonster(instRandomPos(), MonsterPrefab[randomvalue]);
        }
    }

    private Vector3 instRandomPos()
    {
        //Debug.Log("## 생성된거야 친구야?");
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
