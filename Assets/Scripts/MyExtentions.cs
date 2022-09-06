using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtentions
{
    public static void Recycle(this GameObject go) //������Ŭ �޼��带 ��� (���ӿ�����Ʈ������ )
    {
        Monster mob = go.GetComponent<Monster>(); // ������ ��ü ���� = �޾ƿ� ���ӿ�����Ʈ�� ������Ʈ�� �����´�.
        MonsterPooling.Instance.DestroyMonster(mob); // ���� Ǯ������ �׾��� ���� ��ü ����
    }

    public static void Reload(this GameObject go) 
    {
        BulletObject bull = go.GetComponent<BulletObject>();
        BulletPooling.Instance.DestroyBullet(bull);
    }
}
