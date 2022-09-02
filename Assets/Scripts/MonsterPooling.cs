using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPooling : MonoBehaviour
{
    //�̱���
    #region �̱���
    static MonsterPooling instance = null; // �ڱ��ڽ��� ��ü�� ���� -> �̱��� (���ϰ�ü)
    
    
    public static MonsterPooling Inst //������Ƽ ����
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
    Queue<Monster> MobPool = new Queue<Monster>(); //ť(���Լ���)�� ���ο� ���͸� MobPool�� �ִ´�.

    public void Awake()
    {
        //monsterPrefab = Resources.Load<Monster>("Monster"); // ���� �������� �ҷ��´�. ���ҽ����Ͽ� �ִ� ���ͽ�ũ��Ʈ�� �޷��ִ� ������������
    }
    public Monster CreateMonster(Vector3 m_pos) //���� Ŭ������ ��ȯ�ϴ� ����
    {
        Monster instMob=null ; // ���� ��ü�� ���� �ʱ�ȭ����

        if (MobPool.Count==0) // ���� Ǯ�� ī��Ʈ ������ 0 �̸�
        {
            instMob = Instantiate(monsterPrefabs, m_pos, Quaternion.identity); // ����ü�� ������ ���� ������ ������ ��������(������Ʈ, ��ġ, ȸ��)
            //MonsterListMgr.Inst.AddMonster(instMob); //
            return instMob;
        }

        instMob = MobPool.Dequeue(); // pool�� �����ߴ��ָ� Dequeue�� ����
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
