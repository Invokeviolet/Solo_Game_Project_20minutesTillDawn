using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    //public static ItemSpawner instItem;
    [SerializeField] Transform TargetSpawnPos; // �� ��ġ�� ������ ���� -> ���� ��ǥ��ġ�� ������ ��
    [SerializeField] ExpItem[] Item;

    Monster monsterInfo;

    public int ItemCount;    
    
    //���۰� ���ÿ� ���� ��ġ�� ������ ����

    private void Awake()
    {
        monsterInfo = FindObjectOfType<Monster>();        
        
        //Item.SetActive(false);
    }

    void Start()
    {
        ItemCount = 30;
    }

    
    void Update()
    {
        //DropTheItem(monsterPos.transform.position, ItemCount); //���� ����ŭ ������ ������ ����
    }

    public void ItemSpawn(Vector2 pos) 
    {
        Debug.Log("## ������ ����111111111111111111111");
        ItemPool.Instance.CreateItem(pos); // ���� ���� ��ġ���� �����Ϸ���?
    }
}