using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;

    [SerializeField] int Maxbullet = 6;
    [SerializeField] int Curbullet = 1;    
    [SerializeField] float Damage = 20f;
    [SerializeField] float AttackSpeed = 4.0f;
    Vector2 BulletPosition_Xpos;
    Vector2 BulletPosition_Ypos;
    Vector2 BulletPosition;

    PlayerController playerController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }
    void Inst_Bullet()
    {

        //권총 위치에서 비활성화되었다가 클릭했을 때만 활성화
    }
    public void MoveBullet()
    {
        //마우스 클릭시 클릭한 방향으로 총알 이동
        
    }

    void Click()
    {
        //마우스 클릭 시, 클릭한 방향으로 이동


        transform.Translate(transform.position);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob" || collision.gameObject.tag == "OutBox")
        {
            Destroy(gameObject); // 나중에 재사용할 부분
        }

    }
    void Bullet_Reload()
    {
        //bullet이 0일때 자동 재장전
        //재장전 애니메이션
    }

}
