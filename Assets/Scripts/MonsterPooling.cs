using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPooling : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------
    //�̱���
    #region �̱���
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
        // (���Ϸ� �����ϴ�)�������� �ε��ϴ� �Լ��̴�.
        // -> ���ϰ�θ� ã�ư��� ���̱� ������ ���ϰ�ΰ� �ٲ�� ���� �ٲ��־���ϹǷ� �ظ��ϸ� ���� ����
    }

    public Monster CreateMonster(Vector3 pos, Monster name)
    {

        Monster instMob = null;
        //ó������ �ƹ��͵� ������ ��������
        if (pool.Count == 0)
        {
            //Debug.Log("## ���� ������...");

            instMob = Instantiate(name, pos, Quaternion.identity, mobSpawner.transform);

            // �ε��� �������� �̿��ؼ� �ν���Ʈ ��ü �Ѱ��� �����.

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
        pool.Enqueue(mob); // pool �� 1�� �þ��.
    }

}
