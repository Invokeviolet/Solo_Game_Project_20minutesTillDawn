using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������
[CreateAssetMenu(menuName = "Scriptable/MobData", fileName = "Mob Data")]
public class MonsterData : ScriptableObject
{
    public float health = 100f; // ü��
    public float damage = 20f; // ���ݷ�
    public float speed = 2f; // �̵� �ӵ�

}
