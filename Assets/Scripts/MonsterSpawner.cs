using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;

    [SerializeField] Monster[] MonsterPrefab;
    [SerializeField] Transform playerPos;
    [SerializeField] public int MonsterCount; // 웨이브에 따라 값이 바뀌어야 함

    float TimeAfterSpawn;
    float SpawnRate;

   

    void Start()
    {
        MonsterCount = 0;
        
        SpawnRate = Random.Range(2f, 6f);

    }


    private void Update()
    {
        processSpawn();
    }


    private void processSpawn()
    {
        int randomvalue = Random.Range(0,3);

        TimeAfterSpawn += Time.deltaTime;


        //몬스터가 생성될 위치
        float Xpos = Random.Range(transform.localPosition.x - 20, transform.localPosition.x + 20);
        float Ypos = Random.Range(transform.localPosition.y - 20, transform.localPosition.y + 20);

        Vector3 RandomPos = new Vector3(Xpos, Ypos, 0);
        Debug.Log("## transform.position.x" + transform.localPosition.x);
        Debug.Log("## transform.position.y" + transform.localPosition.y);

        if (TimeAfterSpawn>=SpawnRate) 
        {

            TimeAfterSpawn = 0f;
            Monster Mob = MonsterPooling.Instance.CreateMonster(RandomPos,MonsterPrefab[randomvalue]);

            SpawnRate= Random.Range(6f, 12f);
        }
    }

}
