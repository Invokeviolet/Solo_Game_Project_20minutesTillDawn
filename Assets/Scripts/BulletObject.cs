using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField] float ReloadTime = 1.0f;
    [SerializeField] public int Maxbullet = 6;
    [SerializeField] public int Curbullet = 0;
    [SerializeField] float AttackSpeed = 10.0f;
    public float BulletDamage = 20f;

    [SerializeField] Monster monster;

    Rigidbody2D Rigidbody;
    Animator BulletAnimator;

    bool isReload=false;
    //Vector2 targetMonsterPos;

    public Transform myTarget { get; set; }
    private void Start()
    {
        monster = FindObjectOfType<Monster>();
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
            //BulletAnimator.SetBool("isReload", isReload);

        }
        else 
        {
            isReload = false;
            //BulletAnimator.SetBool("isReload", isReload);

        }
    }


    public void Shoot()
    {

        //gameObject.SetActive(true); // �Ѿ� Ȱ��ȭ
        transform.Translate(Vector3.right * AttackSpeed * Time.deltaTime);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mob")
        {
            Debug.Log("## �Ѿ� ������ ���� ��");

            Curbullet--;
            Destroy(gameObject);
        }
        if (collision.tag == "OutBox")
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
