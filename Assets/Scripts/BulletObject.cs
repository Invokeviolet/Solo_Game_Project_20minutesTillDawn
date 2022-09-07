using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총알 오브젝트 클래스
public class BulletObject : MonoBehaviour
{
    
    [SerializeField] public float AttackSpeed = 10.0f; // 공격 속도
    
    public float BulletDamage = 20f; // 총알 데미지

    [SerializeField] BulletObject BulletPrefab; // 스포너 정보 가져오기


    public Transform myTarget { get; set; }

    

    void Update()
    {
        BulletMove();

    }

    public void BulletMove()
    {
        transform.Translate(Vector3.right * AttackSpeed * Time.deltaTime); // 게임오브젝트를 움직일거야 (방금 계산한 거리 * 시간}       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.tag == "Mob") || (collision.tag == "OutBox"))
        {            
            
            BulletPooling.Instance.DestroyBullet(BulletPrefab); // 자기자신을 풀링에 다시 넣음
        }

    }


}