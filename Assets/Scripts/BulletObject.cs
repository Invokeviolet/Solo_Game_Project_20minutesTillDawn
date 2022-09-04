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
        // Ű�Է��� �������� �Ѿ� ����
        Shoot();

        if (Curbullet <= Maxbullet)
        {
            ReloadBullet();

            isReload = true;
            //������ �ִϸ��̼�
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

        //gameObject.SetActive(true); // �Ѿ� Ȱ��ȭ
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
            Destroy(gameObject); // ���߿� ������ �κ�
        }

    }


    //Coroutine ReloadCoroutine = null;

    IEnumerator ReloadBullet() 
    {
        while (isReload == true)
        {
            yield return new WaitForSeconds(ReloadTime);
            
            //������ �����̴� �����̱�
        }
    }
}
