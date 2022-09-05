using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instItem;
    [SerializeField] Transform monsterPos;
    [SerializeField] ExpItem[] Item;

    Monster monsterInfo;

    public int ItemCount;    
    
    //시작과 동시에 몬스터 위치에 아이템 생성

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
        //DropTheItem(monsterPos.transform.position, ItemCount); //몬스터 수만큼 생성할 값으로 수정
    }

    public void ItemSpawn() 
    {
        Debug.Log("## 아이템 생성111111111111111111111");
        ItemPool.Instance.CreateItem(monsterPos.transform.position);
    }
}
