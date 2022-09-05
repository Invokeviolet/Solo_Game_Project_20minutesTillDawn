using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------
    //싱글톤
    #region 싱글턴
    
    static ItemPool instance = null;
    public static ItemPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemPool>();
                if (instance == null)
                {
                    instance = new GameObject("MonsterPool").AddComponent<ItemPool>();
                }
            }
            return instance;
        }
    }
    #endregion
    //
    //---------------------------------------------------------------------------------------------

    [SerializeField] ExpItem expItemPrefab;    

    Queue<ExpItem> Itempooling = new Queue<ExpItem>();

    [SerializeField] Transform TargetObject; // 아이템이 어느 위치에 생성될지 // 아이템 스포너 역할

    public ExpItem CreateItem(Vector3 pos)
    {

        ExpItem instItem = null;
        //처음에는 아무것도 없으니 생성하자
        if (Itempooling.Count == 0)
        {

            instItem = Instantiate(expItemPrefab, TargetObject.transform.position, Quaternion.identity);

            return instItem;

        }


        instItem = Itempooling.Dequeue();
        //instMob.transform.parent = null;
        instItem.transform.position = pos;
        instItem.transform.rotation = Quaternion.identity;
        instItem.gameObject.SetActive(true);

        //if (pool.Count > 0)
        return instItem;
    }

    public void DestroyItem(ExpItem expItem)
    {

        expItem.gameObject.SetActive(false);
        Itempooling.Enqueue(expItem); // pool 에 1개 늘어난다.
    }
}
