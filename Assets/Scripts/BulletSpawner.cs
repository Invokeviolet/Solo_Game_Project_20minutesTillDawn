using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    BulletObject bulletObject; // 불렛 오브젝트의 객체 생성

    float TimeAfterSpawn; // 생성하는데 걸리는 시간
    float SpawnRate; // 다음 생성까지 걸리는 시간    
    public Transform mousetransform;


    void Start()
    {
        bulletObject = GetComponent<BulletObject>(); // 불렛오브젝트의 컴포넌트를 참조한다.
        TimeAfterSpawn = 0;
        SpawnRate = 1.0f;
    }
    void Update()
    {
        Vector2 lens = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // 마우스 - 플레이어 위치
        float z = Mathf.Atan2(lens.y, lens.x) * Mathf.Rad2Deg; // 삼각함수 계산 / 각도 계산 한것을 z 값에 넣고
        transform.rotation = Quaternion.Euler(0, 0, z); // 오일러로 변환해서 실제 좌표 값에 넣기 
        //오일러 사용하는 이유? (쿼터니언이 사원수이기 때문에 벡터값으로 변환해서 사용)

        InstBullet();
    }

    public void InstBullet()
    {
        // 마우스클릭 시 총알 뱉기

        if (Input.GetMouseButtonDown(0))
        {
            BulletPooling.Instance.CreateBullet(transform.position, transform.rotation); // 불렛풀링에서 불렛 생성해서 가져옴. (스포너위치에서 발생, 사용될 총알 프리팹)                        
        }

    }

}