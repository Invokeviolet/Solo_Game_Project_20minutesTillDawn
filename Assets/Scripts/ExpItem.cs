using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    [SerializeField] ExpItem expItemPrefab;
    public int ExpValue = 10; // 경험치 값
    public int TargetMoveSpeed = 3; // 이동속도
    [SerializeField]Transform monsterInfo; //  아이템을 해당 (스포너)몬스터로 이동시키기 위해 몬스터의 좌표값을 받아옴

    void Awake()
    {
       
    }

    void Update()
    {
        //플레이어가 가까이 있으면 플레이어에게 이동
       // transform.Translate(monsterInfo.position* TargetMoveSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("먹었니?");
            gameObject.SetActive(false);
            ItemPool.Instance.DestroyItem(expItemPrefab);
        }
    }

}
