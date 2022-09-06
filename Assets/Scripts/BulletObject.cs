using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField] public float ReloadTime = 1.0f; // 재장전 시간
    [SerializeField] public int Maxbullet = 6; // 최대 총알 갯수
    [SerializeField] public int Curbullet = 0; // 현재 총알 갯수
    [SerializeField] public float AttackSpeed = 10.0f; // 공격 속도
    public float BulletDamage = 20f; // 총알 데미지
      

    [SerializeField] BulletObject BulletPrefab; // 스포너 정보 가져오기


    public Transform myTarget { get; set; }

           
    private void Start()
    {        
        Curbullet = Maxbullet; // 현재 불릿 갯수를 최대 갯수로 초기화
    }


    void Update()
    {
        BulletMove();

        if (Curbullet <= 0)
        {
            StartCoroutine(ReloadBullet());
            UIManager.Instance.bulletCheck(ReloadTime);
        }

    }

    public void BulletMove()
    {
        transform.Translate(Vector3.right * AttackSpeed * Time.deltaTime); // 게임오브젝트를 움직일거야 (방금 계산한 거리 * 시간}       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.tag == "Mob") || (collision.tag == "OutBox"))
        {
            Debug.Log("사용된 총알 갯수 : "+Curbullet);

            Curbullet--; // 현재 총알 갯수 -1
            UIManager.Instance.BulletCount(Curbullet);

            Debug.Log("사용된 총알 갯수 : " + Curbullet);
            BulletPooling.Instance.DestroyBullet(BulletPrefab); // 자기자신을 풀링에 다시 넣음

        }

    }


    IEnumerator ReloadBullet()
    {
        yield return new WaitForSeconds(ReloadTime);
    }
}