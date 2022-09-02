using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterListMgr : MonoBehaviour
{
    private MonsterListMgr() { } // 
    static MonsterListMgr instance = null;

    //--------------------------------------------------------------------------------------   
    #region 싱글턴
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


    //몬스터 배열 리스트 (일반몹, 중간몹, 보스몹)
    List<Monster>[] listMobs = new List<Monster>[] { new List<Monster>(), new List<Monster>(), new List<Monster>() };

    public int GetAllCount()
    {
        int count = 0;
        for (int i = 0; i < listMobs.Length; ++i) //몬스터의 배열의 갯수.
        {
            count += listMobs[i].Count; //카운트 += 리스트에 있는 몹의 i 번째 
        }

        return count;
    }

    int findMyPosIdx(Vector3 pos)
    {
        if (pos.x < 0f && pos.y > 0f)return 0; // 왼쪽

        if (pos.x >= 0f && pos.y > 0f)return 1; // 오른쪽

        if (pos.x < 0f && pos.y <= 0f)return 2; // 왼쪽 아래
        
        return 3; //오른쪽 아래
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
