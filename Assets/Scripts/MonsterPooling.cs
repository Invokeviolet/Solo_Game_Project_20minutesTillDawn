using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPooling : MonoBehaviour
{
    //싱글턴
    #region 싱글톤
    static MonsterPooling instance = null; // 자기자신의 객체를 선언 -> 싱글턴 (유일객체)
    
    
    public static MonsterPooling Inst //프로퍼티 선언
    {
        get 
        {
            if (instance==null) 
            { 
                instance = FindObjectOfType<MonsterPooling>();
                if (instance == null) 
                { 
                    instance = new GameObject("MonsterPooling").AddComponent<MonsterPooling>();
                }
            }

            return instance;

        }
    }
    #endregion
    //--------------------------------------------------------------------------------------------

    [SerializeField] Monster monsterPrefabs = null;
    Queue<Monster> MobPool = new Queue<Monster>(); //큐(선입선출)에 새로운 몬스터를 MobPool에 넣는다.

    public void Awake()
    {
        //monsterPrefab = Resources.Load<Monster>("Monster"); // 몬스터 프리팹을 불러온다. 리소스파일에 있는 몬스터스크립트가 달려있는 몬스터프리팹을
    }
    public Monster CreateMonster(Vector3 m_pos) //몬스터 클래스를 반환하는 형식
    {
        Monster instMob=null ; // 몬스터 개체의 값을 초기화해줌

        if (MobPool.Count==0) // 만약 풀의 카운트 갯수가 0 이면
        {
            instMob = Instantiate(monsterPrefabs, m_pos, Quaternion.identity); // 몹개체를 다음과 같은 조건을 가지고 생성해줌(오브젝트, 위치, 회전)
            //MonsterListMgr.Inst.AddMonster(instMob); //
            return instMob;
        }

        instMob = MobPool.Dequeue(); // pool에 저장했던애를 Dequeue로 꺼냄
        instMob.transform.position = m_pos;
        instMob.transform.rotation = Quaternion.identity;
        instMob.gameObject.SetActive(true);
        return instMob;
    }
    public void DestroyMonster(Monster mob) 
    {
        mob.transform.parent = this.transform;
        mob.gameObject.SetActive(false);
        MobPool.Enqueue(mob);

        //MonsterListMgr.Inst.DelMonster(mob);
    }
}
