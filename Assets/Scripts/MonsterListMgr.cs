using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterListMgr : MonoBehaviour
{
    private MonsterListMgr() { } // 
    static MonsterListMgr instance = null;

    //--------------------------------------------------------------------------------------   
    #region �̱���
    public static MonsterListMgr Inst
    {
        get
        {
            if (instance == null)
            {
                instance = new MonsterListMgr();
            }

            return instance;
        }
    }
    #endregion
    //--------------------------------------------------------------------------------------


    //���� �迭 ����Ʈ (�Ϲݸ�, �߰���, ������)
    List<Monster>[] listMobs = new List<Monster>[] { new List<Monster>(), new List<Monster>(), new List<Monster>() };

    public int GetAllCount()
    {
        int count = 0;
        for (int i = 0; i < listMobs.Length; ++i) //������ �迭�� ����.
        {
            count += listMobs[i].Count; //ī��Ʈ += ����Ʈ�� �ִ� ���� i ��° 
        }

        return count;
    }

    int findMyPosIdx(Vector3 pos)
    {
        if (pos.x < 0f && pos.y > 0f)return 0; // ����

        if (pos.x >= 0f && pos.y > 0f)return 1; // ������

        if (pos.x < 0f && pos.y <= 0f)return 2; // ���� �Ʒ�
        
        return 3; //������ �Ʒ�
    }
/*
    public void AddMonster(Monster mob)
    {
        int posIdx = findMyPosIdx(mob.transform.position);
        mob.MyPosIdx = posIdx;
        listMobs[posIdx].Add(mob);
    }

    public void UpdateMonster(Monster mob)
    {
        int posIdx = findMyPosIdx(mob.transform.position);
        if (mob.MyPosIdx == posIdx) return;

        listMobs[mob.MyPosIdx].Remove(mob);

        mob.MyPosIdx = posIdx;
        listMobs[posIdx].Add(mob);
    }

    public void DelMonster(Monster mob) 
    {
        listMobs[mob.MyPosIdx].Remove(mob);
    }

    List<Monster> listRes = new List<Monster>();
    public List<Monster> FindMonsterListBy(Vector3 pos, float range) 
    {
        int posIdx = findMyPosIdx(pos);

        listRes.Clear();
        foreach (Monster mob in listMobs[posIdx]) 
        {
            if (Vector3.Distance(pos,mob.transform.position)<range) 
            { 
                listRes.Add(mob);
            }
            return listRes;
        }
    }*/
}
