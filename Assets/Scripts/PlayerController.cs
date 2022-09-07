using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("[�÷��̾� ����]")]
    [SerializeField] Text LevelText; // ���� �� ȭ�� �߾� ��ܿ� ǥ�� �� ����
  
    [SerializeField] int maxHp = 4;
    [SerializeField] float MoveSpeed = 6f;
    
    [SerializeField] GameObject expItemObj; // ����ġ ������
    [SerializeField] Monster monster;

    public int curHp = 0; // ���� Hp
    
    bool isWalk; // �ȴ���?
    bool isDead; // �׾�����?


    Vector2 movePlayer; // �÷��̾��� ���Ͱ�
    Animator myAnimator; // �ִϸ��̼�    
    UIManager uiManager; // UI �Ŵ���

    SpriteRenderer ColorRenderer;
    Rigidbody2D rigidbody2D;

    float TimeTrigger = 0f; // ���� �浹 �ð�
    float TimeRate = 0.5f; // �浹 �� ����ð�


    private void Awake()
    {
        curHp = maxHp; // ���� Hp ���� �ִ� ������ ����
        gameObject.SetActive(true); // �÷��̾� ������Ʈ Ȱ��ȭ
        
    }

    // �� ������Ʈ �� ������Ʈ ã�ƿ���
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();        
        myAnimator = GetComponent<Animator>();
        ColorRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        isDead = false; // �׾�����? �� �ʱ�ȭ
        isWalk = false; // �ɾ�����? �� �ʱ�ȭ

    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal"); // �����̵�
        float yAxis = Input.GetAxis("Vertical"); // �����̵�

        bool isMove = (xAxis != 0) || (yAxis != 0); // bool ���·� �������� ���� ���� Ȯ��

        if (isMove) // �������� ���� ��
        {            
            movePlayer = (Vector2.right * xAxis + Vector2.up * yAxis).normalized; // �÷��̾��� �밢���̵��� nomalized �� ����� �̵����� ��� 1�� �޴´�.

            rigidbody2D.MovePosition(rigidbody2D.position + (movePlayer * MoveSpeed * Time.deltaTime)); // �̵��ϴ� �� = ������ٵ� ��ġ()

            // ������ٵ�.�������������� �����ǰ��� ���� ����?
            // �߷��� ���� �����ӿ� ����ϱ⿡ ����.
            // ������ٵ� ��ġ�� ������ �浹üũ�� �����ϴ� ��찡 �������� ���

            isWalk = true; // �Ȱ� �ִٸ�? �ȴ� �ִϸ��̼� true
            myAnimator.SetBool("isWalked", isWalk);
        }
        else
        {
            isWalk = false; // �� �Ȱ� �ִٸ�? �ȴ� �ִϸ��̼� false
            myAnimator.SetBool("isWalked", false);
        }

    }

    private void OnTriggerStay2D(Collider2D collision) // �ݶ��̴� ������ -> ��
    {

        TimeTrigger += Time.deltaTime; // �浹 �ð� += �ð� �ʴ����� �������

        if (isDead) { return; } // �׾��� ���� �׳� ��ȯ

        if (!isDead) // ���� �ʾ��� ��
        {

            if (collision.tag == "Mob") // ���±׸� ���� ������Ʈ�� �浹���� ��
            {
                // ����ð��� �浹 �ð����� Ŭ ��
                if (TimeTrigger >= TimeRate) // ���� �ߺ��Ǿ� �浹�Ǵ� ������ �����ϱ� ���� 
                {
                    TimeTrigger = 0; // �浹�ð��� 0���� �ʱ�ȭ

                    DamageToMonster(monster.attackPower); // �浹���� �� Hp ��ȭ ���� �ְ�ޱ� ���� �޼��� ����

                }
            }

        }
    }

    public void DamageToMonster(float damageValue) // ����� ���� float ������ �޾ƿ�
    {

        if (isDead == true) return; // �׾��� ���� �׳� ��ȯ


        curHp -= (int)damageValue; //���� Hp�� int������ ��ȯ�Ͽ� ���� ����

        if (curHp <= 0) // ���� ���� Hp�� 0���� �۰ų� ���� ��
        {

            curHp = 0; // ���� Hp�� 0���� ����
            isDead = true; // ���� = true
            Die(); // �׾����� �÷��̾�� ��ȭ�� �� �޼��� ��ȯ
        }

    }
    void Die() // �׾��� ��
    {
        
        ColorRenderer.material.color = Color.red; // �÷��̾��� ���� �����ϱ�

        gameObject.SetActive(false); // ���ӿ�����Ʈ ��Ȱ��ȭ
        uiManager.GameOver(); // UI ���¸� ���ӿ������·� ��ȯ

    }


}
