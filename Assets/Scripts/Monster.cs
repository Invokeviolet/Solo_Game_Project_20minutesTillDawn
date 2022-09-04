using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("[���� ����]")]
    [SerializeField] GameObject monster;
    [SerializeField] int maxHp = 30; // �ִ� ü��
    [SerializeField] public float attackPower = 20f; // ���ݷ�
    [SerializeField] float attackRange = 0.1f; // ���� ���� ����
    [SerializeField] float speed = 1.5f; // �̵� �ӵ�
    

    public int curHp = 0; // ���� ü��
    bool isDead { get { return (curHp <= 0); } }
    //bool isAttacked ; // �¾ҳ�?

    Vector3 direction; //������ ��ġ���� �Ҵ��ϱ� ���� ����

    //public GameObject targetPlayer;
    PlayerController targetPlayer = null;
    Animator myAnimator = null;
    CapsuleCollider2D monCC = null;
    BulletObject bulletobj;
    SpriteRenderer MonsterRenderer;
    Rigidbody2D Rigidbody;

     SpriteRenderer ColorRenderer; // ���¸� ������ �� ����ϱ� ���� ��������Ʈ ������
    //public int MyPosIdx { get; set; } = -1; // �� ������ �ε���

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
        monCC = GetComponent<CapsuleCollider2D>();
        targetPlayer = FindObjectOfType<PlayerController>();
        Rigidbody = GetComponent<Rigidbody2D>();
        bulletobj = GetComponent<BulletObject>();

        //isAttacked = false;
        //myAnimator.SetBool("isAttacked", isAttacked);
    }

    private void OnEnable()
    {
        curHp = maxHp;
        MonsterRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
       
    }
    void Update()
    {
        MoveTarget();
        if (isDead) return;
    }
    void MoveTarget()
    {
        direction = (targetPlayer.transform.position - transform.position).normalized; //��ǥ ��ġ - ���� ��ġ. ����ȭ       
        gameObject.transform.Translate(direction * speed * Time.deltaTime); // ���ӿ�����Ʈ�� �����ϰž� (��� ����� �Ÿ� * �ð�)

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
        //Debug.Log("## ������ �����̺�Ʈ ó���Լ�");
        targetPlayer.SendMessage("TransferDamage", attackPower, SendMessageOptions.DontRequireReceiver);
    }

    public void TransferDamage(float dmgInfo) //�޼��� (�Ű�����)
    {
        if (isDead) return;

        //������ �������� ������ HP�� ����
        curHp -= (int)dmgInfo;

        Debug.Log("## ������ ���Դ�?");

        //������ �ؽ�Ʈ ���
        DamageTextMgr.Inst.AddText(dmgInfo, transform.position, transform.position); // �ؽ�Ʈ�� ������ ��ġ, ����� ��ġ
        
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
            direction = new Vector3 (-0.1f, -0.1f, 0); // �˹�
            gameObject.transform.Translate(direction * speed * Time.deltaTime);

            //isAttacked = true;

            //myAnimator.SetBool("isAttacked", isAttacked);
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
        //myAnimator.SetBool("move",false);

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
        //myAnimator.SetBool("move", true);
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
        // ������? �����

        yield return null;

        //Recycle(gameObject);
        gameObject.Recycle();

        //MonsterPooling.Instance.DestroyMonster(this);
    }
    IEnumerator Restore_State()
    {
        //�÷��̾ ������ ���ʹ� �ڱⰡ ������ ������ �ٽ� �̵�

        yield return null;
    }

}
