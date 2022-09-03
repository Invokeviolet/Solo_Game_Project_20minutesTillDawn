using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxHp = 4;
    [SerializeField] float MoveSpeed = 3.0f;
    [SerializeField] float AttackSpeed = 1.8f;
    [SerializeField] float attackPower = 20f; // ���ݷ�
    [SerializeField] float attackRange = 0.1f; // ���� ���� ����
    int curHp = 0;

    bool isWalk = false;
    bool isDead = false;


    Vector3 movePlayer = Vector3.zero;
    CharacterController myCC = null;
    Animator myAnimator;
    Monster myMonster;
    Wepon wepon;

    SpriteRenderer ColorRenderer;

    void Start()
    {
        myCC = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
        wepon = GetComponent<Wepon>();
        myMonster = GetComponent<Monster>();
        ColorRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        bool isMove = (xAxis != 0) || (yAxis != 0);

        if (isMove)
        {
            //�밢���̵�
            movePlayer = Vector3.right * xAxis + Vector3.up * yAxis;
            myCC.Move(movePlayer * MoveSpeed * Time.deltaTime);
            isWalk = true;
            myAnimator.SetBool("isWalked", isWalk);
        }
        else
        {
            isWalk = false;
            myAnimator.SetBool("isWalked", false);
        }

    }

    //�̵��Ҷ� ���콺�� x < 0 �̸� �״�� ���� ������ ����
    //�̵����� ���콺�� x < 0 �̸� �������ֱ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead != null)
        {
            if (tag == "Mob")
            {
                ColorRenderer.material.color = Color.white; //���� �����ϱ�
                DamageToMonster(myMonster.attackPower);
                
            }
        }
    }    

    void DamageToMonster(float damageValue)
    {
        if (isDead) return;

        curHp -= (int)damageValue;
        if (curHp <= 0)
        {
            curHp = 0;
            Die(); // ���߿� Enum ����Ͽ� ����
        }

    }
    void Die()
    {
        if (isDead == true)
        {
            ColorRenderer.material.color = Color.red; //���� �����ϱ�
            
            //������? �⺻ �ִϸ��̼ǵ��� �۵������� ��ġ�̵� �Ұ�. ĳ���� ������Ʈ�� ���������� �Ʒ��� ���� �����

            Destroy(gameObject);

        }

    }

    void GameOver() 
    { 
        //���� ǥ�õǴ� â ���� -> ����� �Ǵ� Ȩ���� ���ư��� ��ư
        //����� ������ ���Ӿ� ���� �ٽ� ���
        //Ȩ���� ������ Ÿ��Ʋ�� ���
        //�����Ϳ� ����Ʈ �� �����ؼ� ����Ǿ�� �ϰ�, �� ����
    }
}
