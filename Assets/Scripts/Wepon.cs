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

        //���� ��ġ���� ��Ȱ��ȭ�Ǿ��ٰ� Ŭ������ ���� Ȱ��ȭ
    }
    public void MoveBullet()
    {
        //���콺 Ŭ���� Ŭ���� �������� �Ѿ� �̵�
        
    }

    void Click()
    {
        //���콺 Ŭ�� ��, Ŭ���� �������� �̵�


        transform.Translate(transform.position);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob" || collision.gameObject.tag == "OutBox")
        {
            Destroy(gameObject); // ���߿� ������ �κ�
        }

    }
    void Bullet_Reload()
    {
        //bullet�� 0�϶� �ڵ� ������
        //������ �ִϸ��̼�
    }

}
