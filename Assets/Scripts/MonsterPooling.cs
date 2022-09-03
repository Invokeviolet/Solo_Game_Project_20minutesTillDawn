using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPooling : MonoBehaviour
{
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
                    instance = new GameObject("Game").AddComponent<MonsterPooling>();
                }
            }
            return instance;
        }
    }

    Queue<Monster> pool = new Queue<Monster>();
    [SerializeField] Monster prefab;

    public Monster CreateMonster(Vector3 pos)
    {
        Monster instMob = null;
        //처음에는 아무것도 없으니 생성하자

        if (pool.Count == 0)
        {
            // 로드한 프리팹을 이용해서 인스턴트 객체 한개를 만든다.
            instMob = Instantiate(prefab, pos, Quaternion.identity, this.transform);
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
