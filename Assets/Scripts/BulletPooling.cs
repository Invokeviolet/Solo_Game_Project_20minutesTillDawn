using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------    
    //싱글턴
    #region 싱글턴
    static public BulletPooling instance; // 자신을 static(모든 곳에서 사용가능하도록) 생성
    public static BulletPooling Instance // 자신이 생성되지 않았을때 생성해주는 메서드
    {
        get
        {
            if (instance == null) // 자기 자신이 null 값이면?
            {
                instance = FindObjectOfType<BulletPooling>(); // 불렛풀링을 찾아서 넣어줘.

                if (instance == null) // 그래도 없으면?
                {
                    instance = new GameObject("BulletPooling").AddComponent<BulletPooling>(); // 새로 게임 오브젝트로 생성함. 불렛풀링이라는 컴포넌트를 더해서.
                }
            }
            return instance; // 그리고 반환
        }
    }
    #endregion
    //
    //---------------------------------------------------------------------------------------------

    Queue<BulletObject> pool = new Queue<BulletObject>(); // 불렛오브젝트 형태의 풀을 새로 생성.
    [SerializeField] GameObject BulletSpawner; // 오브젝트 지정
    [SerializeField] BulletObject BulletPrefab; // 총알 프리팹

    public BulletObject CreateBullet(Vector3 pos, Quaternion rot) //불렛오브젝트 형태의 총알생성메서드(받아올 위치값, 불렛오브젝트 형태)
    {
        BulletObject instbullet = null; // 불렛오브젝트의 객체 null으로 초기화

        if (pool.Count == 0) // 풀 안에 오브젝트의 갯수가 0 이면
        {
            instbullet = Instantiate(BulletPrefab, pos, rot); // instbullet에 객체를 생성해주는 값을 할당함
            BulletPrefab.gameObject.SetActive(true);

            return instbullet; // 생성한 객체를 반환 -> 풀로 반환?
        }

        instbullet = pool.Dequeue(); // 풀에서 꺼내쓴다.
        instbullet.transform.position = pos; // 위치는 위에서 지정한 위치값으로
        instbullet.transform.rotation = rot; // 회전값도
        instbullet.gameObject.SetActive(true); // 게임오브젝트 활성화

        return instbullet; // 객체를 반환
    }

    public void DestroyBullet(BulletObject bullobj) // 삭제되는 총알 메서드 생성(총알 오브젝트 형태로 받아오는 매개변수)
    {
        bullobj.gameObject.SetActive(false); // 받아온 매개변수의 오브젝트를 비활성화 한다.
        pool.Enqueue(bullobj); // 사용된 총알은 Enqueue를 사용하여 풀에 다시 넣어준다.
    }

}
