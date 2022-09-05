using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    //[SerializeField] Transform TargetPos;
    public int ExpValue = 10; // 경험치 값
    public int TargetMoveSpeed = 3; // 플레이어에게로 가는 이동속도
    Monster monsterInfo;

    void Awake()
    {
        monsterInfo = FindObjectOfType<Monster>();
       

    }

    void Update()
    {
        //플레이어가 가까이 있으면 플레이어에게 이동
        //transform.Translate(transform.position* TargetMoveSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

}
