using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총알 스포너 - 총알을 관리해주기 위한 클래스
public class BulletSpawner : MonoBehaviour
{

    [SerializeField] BulletObject bulletObject; // 불렛 오브젝트의 객체 생성

    [SerializeField] public int Maxbullet = 6; // 최대 총알 갯수
    [SerializeField] public int Curbullet = 0; // 현재 총알 갯수

    float TimeAfterSpawn; // 생성하는데 걸리는 시간
    float SpawnRate; // 다음 생성까지 걸리는 시간    
    

    UIManager uimanager;

    void Start()
    {
        uimanager = FindObjectOfType<UIManager>();
        //bulletObject = FindObjectOfType<BulletObject>(); // 불렛오브젝트의 컴포넌트를 참조한다.
        TimeAfterSpawn = 0;
        SpawnRate = 1.0f;

        Debug.Log("## Curbullet : " + Curbullet);
        Debug.Log("## Maxbullet : " + Maxbullet);
        Curbullet = Maxbullet; // 현재 불릿 갯수를 최대 갯수로 초기화
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

        UIManager.Instance.BulletCount(Curbullet); // 현재 불렛 갯수를 좌측 상단에 표시해줄 UI
        UIManager.Instance.MouseBulletCount(Curbullet); // 현재 불렛 갯수를 마우스에 표시해줄 UI
        

        if (Input.GetMouseButtonDown(0))
        {
            if (Curbullet >= 1) //총알이 있을때만 발사
            {
                BulletPooling.Instance.CreateBullet(transform.position, transform.rotation); // 불렛풀링에서 불렛 생성해서 가져옴. (스포너위치에서 발생, 사용될 총알 프리팹)                        

                Curbullet--; // 현재 총알 갯수 -1

                //Debug.Log("--Curbullet : " + Curbullet);


            }
        }
        if (Input.GetKey(KeyCode.R)) // 재장전 버튼을 꾹 누르고 있으면
        {
            UIManager.Instance.bulletCheck(); // 재장전
        }
        if (Curbullet <= 0) // 남은 총알이 0이거나 음수일때 재장전시간 이후에 총알 생성
        {
            Curbullet = 0; // 현재 불릿을 0으로 제한
            
            UIManager.Instance.bulletCheck(); // 불릿을 체크하는 메서드 생성
        }
    }



}