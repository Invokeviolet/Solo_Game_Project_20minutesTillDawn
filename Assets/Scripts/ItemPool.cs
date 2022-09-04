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

    [SerializeField] Monster itemSpawner;    

    Queue<ExpItem> Itempooling = new Queue<ExpItem>();
   

    public ExpItem CreateMonster(Vector3 pos, ExpItem expItem)
    {

        ExpItem instItem = null;
        //처음에는 아무것도 없으니 생성하자
        if (Itempooling.Count == 0)
        {
            //Debug.Log("## 몬스터 생성중...");

            instItem = Instantiate(expItem, Vector3.zero, Quaternion.identity, itemSpawner.transform);

            // 로드한 프리팹을 이용해서 인스턴트 객체 한개를 만든다.

            //Debug.Log("## instMob : "+ instMob.name);
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
