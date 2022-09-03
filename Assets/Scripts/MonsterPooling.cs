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
        //ó������ �ƹ��͵� ������ ��������

        if (pool.Count == 0)
        {
            // �ε��� �������� �̿��ؼ� �ν���Ʈ ��ü �Ѱ��� �����.
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
        pool.Enqueue(mob); // pool �� 1�� �þ��.
    }

}
