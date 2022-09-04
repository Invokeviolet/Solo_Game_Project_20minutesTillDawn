using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPooling : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------
    //싱글톤
    #region 싱글턴
    private MonsterPooling() { }
    static MonsterPooling instance = null;
    public static MonsterPooling Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MonsterPooling>();
                if (instance == null)
                {
                    instance = new GameObject("MonsterPool").AddComponent<MonsterPooling>();
                }
            }
            return instance;
        }
    }
    #endregion
    //
    //---------------------------------------------------------------------------------------------

    [SerializeField] MonsterSpawner mobSpawner;
    
    Queue<Monster> pool = new Queue<Monster>();
    //Monster monster;
    private void Awake()
    {
        
        //prefabMob = Resources.Load<Monster>("Monster"); 
        // (파일로 존재하는)프리팹을 로드하는 함수이다.
        // -> 파일경로를 찾아가는 것이기 때문에 파일경로가 바뀌면 같이 바꿔주어야하므로 왠만하면 쓰지 말자
    }

    public Monster CreateMonster(Vector3 pos, Monster name)
    {

        Monster instMob = null;
        //처음에는 아무것도 없으니 생성하자
        if (pool.Count == 0)
        {
            //Debug.Log("## 몬스터 생성중...");

            instMob = Instantiate(name, pos, Quaternion.identity, mobSpawner.transform);

            // 로드한 프리팹을 이용해서 인스턴트 객체 한개를 만든다.

            //Debug.Log("## instMob : "+ instMob.name);
            return instMob;

        }


        instMob = pool.Dequeue();
        //instMob.transform.parent = null;
        instMob.transform.position = pos;
        instMob.transform.rotation = Quaternion.identity;
        instMob.gameObject.SetActive(true);

        //if (pool.Count > 0)
        return instMob;
    }

    public void DestroyMonster(Monster mob)
    {
        //mob.transform.parent = this.transform;
        mob.gameObject.SetActive(false);
        pool.Enqueue(mob); // pool 에 1개 늘어난다.
    }

}
