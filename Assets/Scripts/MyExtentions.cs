using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtentions
{
    public static void Recycle(this GameObject go)
    {
        Monster mob = go.GetComponent<Monster>();
        MonsterPooling.Instance.DestroyMonster(mob);
    }
}
