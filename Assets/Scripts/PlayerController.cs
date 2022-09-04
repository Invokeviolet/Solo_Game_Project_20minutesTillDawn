using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxHp = 4;
    [SerializeField] float MoveSpeed = 100f;
    //[SerializeField] float attackPower = 20f; // ���ݷ�
    //[SerializeField] float attackRange = 0.1f; // ���� ���� ����
    int curHp = 0;
    public int ExpPoint = 0;

    bool isWalk;
    bool isDead;

    Vector3 movePlayer ;
    Animator myAnimator;
    Monster myMonster;
    Wepon wepon;

    SpriteRenderer ColorRenderer;
    Rigidbody2D rigidbody2D;

    private void Awake()
    {
        curHp = maxHp;
    }
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        wepon = GetComponent<Wepon>();
        myMonster = GetComponent<Monster>();
        ColorRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        isDead = false;
        isWalk = false;
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
            rigidbody2D.MovePosition(movePlayer * MoveSpeed * Time.deltaTime);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) { return; }
        if (!isDead)
        {

            if (collision.tag == "Mob")
            {
                curHp--;
                Debug.Log("## curHp : " + curHp);
                //�˹���ؾ���
                transform.position = new Vector3(-0.5f, -1, 0);
                DamageToMonster(myMonster.attackPower); //

            }
        }
    }

    void DamageToMonster(float damageValue)
    {
        if (isDead==true) return;

        curHp -= (int)damageValue;
        if (curHp <= 0)
        {
            curHp = 0;
            isDead = true;
            Die(); // ���߿� Enum ����Ͽ� ����
        }

    }
    void Die()
    {
        Debug.Log("## isDead : " + isDead);
        if (isDead == true)
        {
            Debug.Log("## isDead : " + isDead);
            ColorRenderer.material.color = Color.red; //���� �����ϱ�

            //������? �⺻ �ִϸ��̼ǵ��� �۵������� ��ġ�̵� �Ұ�. ĳ���� ������Ʈ�� ���������� �Ʒ��� ���� �����
            Vector3 movePlayer = new Vector3(0, 0, 0);
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
