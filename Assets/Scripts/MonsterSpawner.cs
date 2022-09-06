using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;

    [SerializeField] Monster[] MonsterPrefab; // 몬스터 프리팹을 배열로 선언 -> 몬스터를 종류별로 넣기 위함    

    float TimeAfterSpawn; // 생성하는데 걸리는 시간
    float SpawnRate; // 다음 생성까지 걸리는 시간

   

    void Start()
    {
        
        SpawnRate = Random.Range(1f,4f); // 다음 생성까지 걸리는 시간을 랜덤하게 배치함

    }


    private void Update()
    {
        processSpawn(); //밑에 선언한 메서드 가져와서 실행
    }


    private void processSpawn()
    {
        int randomvalue = Random.Range(0,3); // 밑에 선언할 몬스터 프리팹을 랜덤한 순서대로 생성하기 위한 값

        TimeAfterSpawn += Time.deltaTime; // 생성할때 시간을 초단위로 계산하여 더해줌


        //몬스터가 생성될 위치
        float Xpos = Random.Range(transform.localPosition.x - 10, transform.localPosition.x + 10); // X위치를 랜덤한 위치로 계산
        float Ypos = Random.Range(transform.localPosition.y - 10, transform.localPosition.y + 10); // Y위치를 랜덤한 위치로 계산

        Vector3 RandomPos = new Vector3(Xpos, Ypos, 0); // 랜덤 위치를 합쳐서 새로운 위치로 선언
        

        if (TimeAfterSpawn>=SpawnRate) // 스폰 지연시간보다 생성 주기 시간이 더 클때
        {

            TimeAfterSpawn = 0f; // 생성 주기 시간을 0으로 초기화
            Monster Mob = MonsterPooling.Instance.CreateMonster(RandomPos,MonsterPrefab[randomvalue]); // 몬스터 풀에서 몬스터 생성

            SpawnRate = Random.Range(1f, 4f); // 지연시간을 랜덤한 값으로 생성
        }
    }

}
