using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField] float ReloadTime = 1.0f;
    [SerializeField] int Maxbullet = 6;
    [SerializeField] public int Curbullet = 0;
    [SerializeField] float Damage = 20f;
    [SerializeField] float AttackSpeed = 4.0f;
   
    Rigidbody2D Rigidbody;
    

    Vector2 targetPos;
    public Transform myTarget { get; set; }
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        targetPos = myTarget.position - transform.position;

    }
    void Update()
    {
        // 키입력이 없을때는 총알 없기
        Shoot();
    }
    public void Shoot()
    {

        //gameObject.SetActive(true); // 총알 활성화
        transform.Translate(transform.right * AttackSpeed*Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob" || collision.gameObject.tag == "OutBox")
        {
            Destroy(gameObject); // 나중에 재사용할 부분
        }

    }
}
