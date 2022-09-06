using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField] float ReloadTime = 1.0f; // 재장전 시간
    [SerializeField] public int Maxbullet = 6; // 최대 총알 갯수
    [SerializeField] public int Curbullet = 0; // 현재 총알 갯수
    [SerializeField] float AttackSpeed = 10.0f; // 공격 속도
    public float BulletDamage = 20f; // 총알 데미지

    [SerializeField] Monster monster; // 몬스터 정보를 받아올 몬스터 선언
    
    [SerializeField] BulletObject BulletPrefab; // 스포너 정보 가져오기

    bool isReload = false; // 재장전 중인지?
    bool isShoot = true; // 발사중인가?
    private void Start()
    {
        
        monster = FindObjectOfType<Monster>(); // 몬스터 스크립트를 찾아와        
        Curbullet = Maxbullet; // 현재 불릿 갯수를 최대 갯수로 초기화
    }


    void Update()
    {
        BulletMove();

        if (Curbullet <= Maxbullet)
        {
            //StartCoroutine(ReloadBullet());
            isReload = true;
        }
        else
        {
            isReload = false;
        }
    }

    public void BulletMove()
    {        
        
        gameObject.transform.Translate(Vector3.right * AttackSpeed * Time.deltaTime); // 게임오브젝트를 움직일거야 (방금 계산한 거리 * 시간}

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.tag == "Mob") || (collision.tag == "OutBox"))
        {
            //Debug.Log("## 총알 데미지 들어가는 곳");

            Curbullet--; // 현재 총알 갯수 -1

            BulletPooling.Instance.DestroyBullet(BulletPrefab); // 자기자신을 풀링에 다시 넣음

        }

    }


    /*IEnumerator ReloadBullet()
    {
        if (isReload == true)
        {
            yield return new WaitForSeconds(ReloadTime);
        }
    }*/
}