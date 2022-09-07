using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("[���� ����]")]
    [SerializeField] GameObject monster; // ����
    [SerializeField] int maxHp = 30; // �ִ� ü��
    [SerializeField] float attackRange = 0.1f; // ���� ���� ����
    [SerializeField] float speed = 1.5f; // �̵� �ӵ�
    [SerializeField] ItemSpawner itemSpawner = null;

    public float attackPower = 1f; // ���ݷ�

    int MonsterCount; // ������ ī��Ʈ�� �����ִ� ���� -> ���� ���� üũ�� ��� �ʿ�    

    public int curHp = 0; // ���� ü��

    bool isDead { get { return (curHp <= 0); } } // �׾�����? bool �� ������Ƽ�� ü���� 0�� �Ǿ����� �׳� ��ȯ�Ѵ�.    

    Vector3 direction; //������ ��ġ���� �Ҵ��ϱ� ���� ����

    PlayerController targetPlayer = null; // Ÿ�� �÷��̾��� ������ ������        
    SpriteRenderer MonsterRenderer; // ���� ��������Ʈ ������

    // ������ ������ ���� ��������
    private void Awake()
    {
        targetPlayer = FindObjectOfType<PlayerController>();
        itemSpawner = FindObjectOfType<ItemSpawner>();        
        MonsterRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        curHp = maxHp; // ���� ü���� �ִ� ü������ ����

    }
    void Start()
    {
        MonsterCount = 0; // ���� ī��Ʈ = 0 ���� �ʱ�ȭ
    }
    void Update()
    {
        if (isDead) return; // �׾����� ��ȯ
        MoveTarget(); // Ÿ���� ���� �ڵ����� �����̴� �޼���
        BoundaryCheck(); // �÷��̾ �̵��ϸ� ���Ͱ� �� ��ó�� �̵��ϴ� �޼���
    }

    void MoveTarget()
    {
        if (targetPlayer == null) { return; } // Ÿ�� �÷��̾ null �̸� �׳� ��ȯ
        direction = (targetPlayer.transform.position - transform.position).normalized; //��ǥ ��ġ - ���� ��ġ. ����ȭ       
        gameObject.transform.Translate(direction * speed * Time.deltaTime); // ���ӿ�����Ʈ�� �����ϰž� (��� ����� �Ÿ� * �ð�)

        if (direction.x < 0) // ����
        {
            MonsterRenderer.flipX = true; // �������� ������
        }
        else // ������
        {
            MonsterRenderer.flipX = false; // ���������� ������
        }
    }


    void onAttackEvent()
    {
        if (targetPlayer == null) { return; } // �÷��̾ null�϶��� �׳� ��ȯ

        // Ÿ�� �÷��̾�� �޼����� ����(TransferDamage�޼��� ����, ���ݷ� ��ŭ, �޽��� �ɼ�.�޼��� ��ȯ�ڰ� ������ üũ�Ұ���?)
        targetPlayer.SendMessage("TransferDamage", attackPower, SendMessageOptions.DontRequireReceiver); 
    }

    public void TransferDamage(float dmgInfo) //�޼��� (�Ű�����)
    {
        if (isDead) return; // �׾��ٸ� �׳� ��ȯ
                
        curHp -= (int)dmgInfo; //������ �������� ������ HP�� ����

        if (curHp <= 0) // ���� ü���� 0 ������ ��
        {
            curHp = 0; // ���� ü���� 0 ���� ����

            nextState(State.DEATH); // ���� ���·� ��ȯ
        }
        else
        {
            nextState(State.HIT); // �´� ���·� ��ȯ
        }

    }

    void BoundaryCheck()
    {
        if (targetPlayer.transform.position.x - transform.position.x > 10) // ��ǥ����-���������� > ������ Ŭ�� // ���������� ����
        {
            MoveMonster(2);
        }
        if (targetPlayer.transform.position.x - transform.position.x < -10) // �������� ����
        {
            MoveMonster(0);
        }
        if (targetPlayer.transform.position.y - transform.position.y > 10) // ���� ����
        {
            MoveMonster(1);
        }
        if (targetPlayer.transform.position.y - transform.position.y < -10) // �Ʒ��� ���� 
        {
            MoveMonster(3);
        }
    }


    void MoveMonster(int dir)
    {
        switch (dir)
        {
            case 0:
                transform.position += Vector3.left * 20; //����
                break;
            case 1:
                transform.position += Vector3.up * 20; // ��
                break;
            case 2:
                transform.position += Vector3.right * 20; // ������
                break;
            case 3:
                transform.position += Vector3.down * 20; // �Ʒ�
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet") // Bullet �̶�� �±׸� ���� ������Ʈ�� �浹�Ҷ� �˹� ȿ��
        {            
            direction = new Vector3(-0.1f, -0.1f, 0); 
            gameObject.transform.Translate(direction * speed * Time.deltaTime); // ���ӿ�����Ʈ�� ������ ���� �������� �̵���

            TransferDamage(20f); // �Ѿ� ������ ���� ����� ����

        }
    }
    //�������̽��� �Ἥ ���ݸ޼ҵ� ���� �����


    //---------------------------------------------------------------------------------------
    //
    // ���¸� ��Ÿ���� �ڷ�ƾ
    // ������ ���� ( ���, �̵�, ����, �ǰ�, ���� )
    //

    public enum State // ����
    {
        NONE,
        IDLE,
        MOVE,
        HIT,
        DEATH,
        Restore

    }

    Coroutine prevCoroutine = null; // �ڷ�ƾ �ʱ�ȭ
    State curState = State.NONE; // �⺻ ����


    void nextState(State newState) // ���� ���·� �Ѿ�� (���¸� �Ű������� ����)
    {
        if (newState == curState) return;
        if (prevCoroutine != null) StopCoroutine(prevCoroutine);

        curState = newState;
        prevCoroutine = StartCoroutine(newState.ToString() + "_State");

    }

    IEnumerator IDLE_State() // ��� ����
    {


        while (isDead == false)
        {
            targetPlayer = FindObjectOfType<PlayerController>();
            if (targetPlayer != null)
            {
                nextState(State.MOVE);
                yield break;
            }
            yield return null;
        }

    }
    IEnumerator MOVE_State() // �̵� ����
    {
        while (!isDead) // ���� �ʾ�����
        {
            // ���� ���� �̵����� �÷��̾� ����� �� ���� �Ÿ��� ���ݹ��� �ȿ� ���� ��
            if (Vector3.Distance(targetPlayer.transform.position, transform.position) <= attackRange) 
            {
                nextState(State.MOVE); // �̵� ���·� ��ȯ
                yield break;
            }
            yield return null;
        }

    }

    IEnumerator HIT_State() // �ǰ� ����
    {
        // �ǰ� ����Ʈ ��� : �Ͼ������ ��¦ �Ÿ�
        
        nextState(State.IDLE); // �Ϲ� ����
        yield return null; // null ��ȯ
    }

    IEnumerator DEATH_State() // ���� ����
    {
        MonsterCount++; // ���� ���� �� üũ

        itemSpawner.ItemSpawn(transform.position); //����ġ ������ ������
                
        gameObject.SetActive(false); // ������ ��Ȱ��ȭ
        yield return null; // null ��ȯ

        MonsterCount++; // ���Ͱ� ������ ī��Ʈ + 1 -> ���߿� ����üũ�� ����

        gameObject.Recycle(); // ���͸� ������
        
    }
    IEnumerator Restore_State() // ������� ��
    {
        //�÷��̾ ������ ���ʹ� �ڱⰡ ������ ������ �ٽ� �̵�
        
        yield return null; // null ��ȯ
    }

}
