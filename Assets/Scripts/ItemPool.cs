using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------
    //�̱���
    #region �̱���
    
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

    [SerializeField] Transform TargetObject; // �������� ��� ��ġ�� �������� // ������ ������ ����

    public ExpItem CreateItem(Vector3 pos)
    {

        ExpItem instItem = null;
        //ó������ �ƹ��͵� ������ ��������
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
        Itempooling.Enqueue(expItem); // pool �� 1�� �þ��.
    }
}
