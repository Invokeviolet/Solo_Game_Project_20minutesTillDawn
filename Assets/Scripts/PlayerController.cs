using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;
using System.ComponentModel;

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


    SpriteRenderer ColorRenderer;
    Rigidbody2D rigidbody2D;

    float TimeTrigger = 0f; // ���� �浹 �ð�
    float TimeRate = 0.5f; // �浹 �� ����ð�

    Wepon_Gun wepon;
    private void Awake()
    {
        curHp = maxHp; // ���� Hp ���� �ִ� ������ ����
        gameObject.SetActive(true); // �÷��̾� ������Ʈ Ȱ��ȭ

    }

    // �� ������Ʈ �� ������Ʈ ã�ƿ���
    void Start()
    {

        myAnimator = GetComponent<Animator>();
        ColorRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        wepon = FindObjectOfType<Wepon_Gun>();

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
            myAnimator.SetBool("isWalked", isWalk);
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
            StartCoroutine(Die_State()); // �׾����� �÷��̾�� ��ȭ�� �� �޼��� ��ȯ
        }

    }

    public IEnumerator Die_State()
    {
        ColorRenderer.material.color = Color.red; // �÷��̾ ������ ���� �����ϱ�

        wepon.SetOff();

        isDead = true;
        myAnimator.SetBool("isDead", isDead);

        //Vector2 vec = new Vector2(transform.position,transform.rotation);

        yield return new WaitForSeconds(6f);

        // UI ���¸� ���ӿ������·� ��ȯ
        UIManager.Instance.GameOver();
    }

}
