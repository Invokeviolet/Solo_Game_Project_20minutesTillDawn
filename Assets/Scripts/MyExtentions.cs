using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtentions
{
    public static void Recycle(this GameObject go) //리사이클 메서드를 사용 (게임오브젝트형태의 )
    {
        Monster mob = go.GetComponent<Monster>(); // 몬스터의 객체 생성 = 받아온 게임오브젝트의 컴포넌트를 가져온다.
        MonsterPooling.Instance.DestroyMonster(mob); // 몬스터 풀링에서 죽었던 몬스터 객체 생성
    }

    
}
