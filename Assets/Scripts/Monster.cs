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
    bool isDead { get { return (curHp <= 0); } }
    //bool isAttacked ; // �¾ҳ�?

    Vector3 direction; //������ ��ġ���� �Ҵ��ϱ� ���� ����

    PlayerController targetPlayer = null;    
    CapsuleCollider2D monCC = null;
    BulletObject bulletobj;
    SpriteRenderer MonsterRenderer;
    Rigidbody2D Rigidbody;
    
     SpriteRenderer ColorRenderer; // ���¸� ������ �� ����ϱ� ���� ��������Ʈ ������
   

    private void Awake()
    {
        bulletobj = FindObjectOfType<BulletObject>();
        targetPlayer = FindObjectOfType<PlayerController>();        
        monCC = GetComponent<CapsuleCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        MonsterRenderer = GetComponent<SpriteRenderer>();
        itemSpawner= FindObjectOfType<ItemSpawner>();
    }

    private void OnEnable()
    {
        curHp = maxHp;
        
        
    }
    void Start()
    {
        MonsterCount = 0;
    }
    void Update()
    {
        MoveTarget();
        if (isDead) return;
    }

    void MoveTarget()
    {
        if (targetPlayer == null) { return; }
        direction = (targetPlayer.transform.position - transform.position).normalized; //��ǥ ��ġ - ���� ��ġ. ����ȭ       
        gameObject.transform.Translate(direction * speed * Time.deltaTime); // ���ӿ�����Ʈ�� �����ϰž� (��� ����� �Ÿ� * �ð�)

        if (targetPlayer == null)
        {
            gameObject.transform.Translate(-direction * speed * Time.deltaTime); // Ÿ���� ���� ��� �ٽ� �ǵ��ư�

        }
        if (direction.x < 0)
        {
            MonsterRenderer.flipX = true;
        }
        else
        {
            MonsterRenderer.flipX = false;
        }
    }

    
    void onAttackEvent()
    {
        if (targetPlayer==null) { return; }
        //Debug.Log("## ������ �����̺�Ʈ ó���Լ�");
        targetPlayer.SendMessage("TransferDamage", attackPower, SendMessageOptions.DontRequireReceiver);
    }

    public void TransferDamage(float dmgInfo) //�޼��� (�Ű�����)
    {
        if (isDead) return;

        //������ �������� ������ HP�� ����
        curHp -= (int)dmgInfo;


        //������ �ؽ�Ʈ ���
        //DamageTextMgr.Inst.AddText(dmgInfo, transform.position, transform.position); // �ؽ�Ʈ�� ������ ��ġ, ����� ��ġ
        
        if (curHp <= 0)
        {            
            curHp = 0;
            
            nextState(State.DEATH);
        }
        else
        {
            nextState(State.HIT);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet") 
        {
            //Debug.Log("## ��!!!!!!!!!!!!!!!!!!!!!!!!!!");
            direction = new Vector3 (-0.1f, -0.1f, 0); // �˹� -> �Ѿ� �������� �ڷ� �з����� ��
            gameObject.transform.Translate(direction * speed * Time.deltaTime);

            TransferDamage(20f); // �Ѿ� ������ ��

        }
    }
    //�������̽��� �Ἥ ���ݸ޼ҵ� ���� �����


    //---------------------------------------------------------------------------------------
    //
    // ���¸� ��Ÿ���� �ڷ�ƾ
    // ������ ���� ( ���, �̵�, ����, �ǰ�, ���� )
    //

    public enum State
    {
        NONE,
        IDLE,
        MOVE,
        HIT,        
        DEATH,
        Restore

    }

    Coroutine prevCoroutine = null;
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
       
        while (isDead == false)
        {
            //targetPlayer = FindObjectOfType<PlayerController>();
            if (Vector3.Distance(targetPlayer.transform.position, transform.position) <= attackRange)
            {
                //myAnimator.SetBool("move", false);
                nextState(State.MOVE);
                yield break;
            }
            yield return null;
        }

    }

    IEnumerator HIT_State() // �ǰ� ����
    {
        // �ǰ� ����Ʈ ��� : �Ͼ��
        //ColorRenderer.material.color = Color.white; //���� �����ϱ�
        nextState(State.IDLE);
        yield return null;
    }
   
    IEnumerator DEATH_State() // ���� ����
    {
        MonsterCount++; // ���� ���� �� üũ
        //����ġ ������ ������
        //ItemSpawner.instItem.ItemSpawn(transform.position);
        itemSpawner.ItemSpawn(transform.position);
        Debug.Log("## ������ ����2222222222222222222222222");

        // ������? �����
        gameObject.SetActive(false);
        yield return null;

        MonsterCount++; // ���Ͱ� ������ ī��Ʈ + 1 -> ���߿� ����üũ�� ����

        gameObject.Recycle(); // ���͸� ������

        //MonsterPooling.Instance.DestroyMonster(this);
    }
    IEnumerator Restore_State()
    {
        //�÷��̾ ������ ���ʹ� �ڱⰡ ������ ������ �ٽ� �̵�
        Debug.Log("## �� �׾���");
        yield return null;
    }

}
