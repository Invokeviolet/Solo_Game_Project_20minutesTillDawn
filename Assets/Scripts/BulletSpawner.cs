using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    
    public GameObject mousetransform; // 몬스터의 게임 오브젝트 받기
    BulletObject bulletObject; // 불렛 오브젝트의 객체 생성

    float TimeAfterSpawn; // 생성하는데 걸리는 시간
    float SpawnRate; // 다음 생성까지 걸리는 시간

    void Start()
    {
        bulletObject = GetComponent<BulletObject>(); // 불렛오브젝트의 컴포넌트를 참조한다.
        TimeAfterSpawn = 0;
        SpawnRate = 1.0f;
    }
    void Update()
    {
        InstBullet();
    }

    public void InstBullet()
    {
        // 마우스클릭 시 총알 뱉기

        if (Input.GetMouseButtonDown(0))
        {
            //GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            BulletPooling.Instance.CreateBullet(transform.position); // 불렛풀링에서 불렛 생성해서 가져옴. (스포너위치에서 발생, 사용될 총알 프리팹)            
        }
    }

}