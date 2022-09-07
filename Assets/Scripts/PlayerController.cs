using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Text LevelText; // ���� �� ȭ�� �߾� ��ܿ� ǥ�� �� ����
    [SerializeField] int Level = 1;
    [SerializeField] int LevelUp = 1;
    [SerializeField] int maxHp = 4;
    [SerializeField] float MoveSpeed = 6f;
    //[SerializeField] public float absorbArange = 3f; // ���� üũ
    [SerializeField] GameObject expItemObj; // ����ġ ������
    [SerializeField] Monster monster;

    public int curHp = 0;
    public float curExpPoint = 0;
    public float plusExpPoint = 10f;

    bool isWalk;
    bool isDead;


    ExpItem expItem;


    Vector2 movePlayer;
    Animator myAnimator;
    Monster myMonster;    
    UIManager uiManager;

    SpriteRenderer ColorRenderer;
    Rigidbody2D rigidbody2D;

    float TimeTrigger = 0f;
    float TimeRate = 1f;


    private void Awake()
    {
        curHp = maxHp;
        gameObject.SetActive(true);
        Debug.Log("## ����ġ : " + curExpPoint);
    }
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        myMonster = FindObjectOfType<Monster>();
        expItem = FindObjectOfType<ExpItem>();
        myAnimator = GetComponent<Animator>();        
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
            movePlayer = Vector2.right * xAxis + Vector2.up * yAxis;
            rigidbody2D.MovePosition(rigidbody2D.position + (movePlayer * MoveSpeed * Time.deltaTime));
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
    private void OnTriggerStay2D(Collider2D collision) // �ݶ��̴� ������ -> ��
    {

        TimeTrigger += Time.deltaTime;

        if (isDead) { return; }

        if (!isDead)
        {
            // ���ο� ���� ����� �� �� Hp �����ϴ� ���� üũ���־�� ��. -> ���Ϸ� ����
            if (collision.tag == "Mob")
            {
                if (TimeTrigger >= TimeRate)
                {
                    TimeTrigger = 0;


                    //�˹���ؾ���
                    DamageToMonster(monster.attackPower);

                }
            }

        }
    }  
   
    public void DamageToMonster(float damageValue)
    {

        if (isDead == true) return;

        //Debug.Log("## curHp : "+ curHp);

        curHp -= (int)damageValue;

        if (curHp <= 0)
        {
            //Debug.Log("## change curHp : " + curHp);
            curHp = 0;
            isDead = true;
            Die(); // ���߿� Enum ����Ͽ� ����
        }

    }
    void Die()
    {
        //Debug.Log("## isDead : " + isDead);
        if (isDead == true)
        {
            Debug.Log("## isDead : " + isDead);
            ColorRenderer.material.color = Color.red; //���� �����ϱ�

            //������? �⺻ �ִϸ��̼ǵ��� �۵������� ��ġ�̵� �Ұ�. ĳ���� ������Ʈ�� ���������� �Ʒ��� ���� �����
            gameObject.SetActive(false);
            uiManager.GameOver();
        }

    }


}
