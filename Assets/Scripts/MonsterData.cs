using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/MobData", fileName = "Mob Data")]
public class MonsterData : ScriptableObject
{
    public float health = 100f; // 체력
    public float damage = 20f; // 공격력
    public float speed = 2f; // 이동 속도

}
