using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField] float ReloadTime = 1.0f;
    [SerializeField] public int Maxbullet = 6;
    [SerializeField] public int Curbullet = 0;
    [SerializeField] public float Damage = 20f;
    [SerializeField] float AttackSpeed = 10.0f;
    
    [SerializeField] Monster monster;

    Rigidbody2D Rigidbody;
    Animator BulletAnimator;

    bool isReload=false;
    //Vector2 targetMonsterPos;

    public Transform myTarget { get; set; }
    private void Start()
    {
        monster = GetComponent<Monster>();
        Rigidbody = GetComponent<Rigidbody2D>();
        BulletAnimator = GetComponent<Animator>();
        
        Curbullet = Maxbullet;
        
    }


    void Update()
    {
        // 키입력이 없을때는 총알 없기
        Shoot();

        if (Curbullet <= Maxbullet)
        {
            ReloadBullet();

            isReload = true;
            //재장전 애니메이션
            BulletAnimator.SetBool("isReload", isReload);

        }
        else 
        {
            isReload = false;
            BulletAnimator.SetBool("isReload", isReload);

        }
    }


    public void Shoot()
    {

        //gameObject.SetActive(true); // 총알 활성화
        transform.Translate(Vector3.right * AttackSpeed * Time.deltaTime);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            monster.curHp--;

        }
        if (collision.gameObject.tag == "OutBox")
        {
            Destroy(gameObject); // 나중에 재사용할 부분
        }

    }


    //Coroutine ReloadCoroutine = null;

    IEnumerator ReloadBullet() 
    {
        while (isReload == true)
        {
            yield return new WaitForSeconds(ReloadTime);
            
            //재장전 슬라이더 움직이기
        }
    }
}
