using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] GameObject monster;
    [SerializeField] int MonsterCount = 10; // ���̺꿡 ���� ���� �ٲ��� ��
    [SerializeField] int maxHp = 100; // ü��
    [SerializeField] public float attackPower = 20f; // ���ݷ�
    [SerializeField] float attackRange = 5f; // ���� ���� ����
    [SerializeField] float speed = 5f; // �̵� �ӵ�

    int curHp = 0;
    bool isDead { get { return (curHp <= 0); } }

    Vector3 direction; //������ ��ġ���� �Ҵ��ϱ� ���� ����

    public GameObject targetPlayer;
    Animator myAnimator = null;
    CapsuleCollider2D monCC = null;
    BulletObject bulletobj;
    SpriteRenderer renderer;
    Rigidbody2D Rigidbody;

    //public int MyPosIdx { get; set; } = -1; // �� ������ �ε���

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
        monCC = GetComponent<CapsuleCollider2D>();
        targetPlayer = FindObjectOfType<PlayerController>().gameObject;
        Rigidbody = GetComponent<Rigidbody2D>();
        bulletobj = GetComponent<BulletObject>();
    }

    private void OnEnable()
    {
        curHp = maxHp;
        renderer = GetComponent<SpriteRenderer>();
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
        gameObject.transform.Translate(direction * Time.deltaTime); // ���ӿ�����Ʈ�� �����ϰž� (��� ����� �Ÿ� * �ð�)

        if (direction.x < 0)
        {
            renderer.flipX = true;
        }
        else
        {
            renderer.flipX = false;
        }
    }

    void onAttackEvent()
    {
        Debug.Log("## ������ �����̺�Ʈ ó���Լ�");
        targetPlayer.SendMessage("TransferDamage", attackPower, SendMessageOptions.DontRequireReceiver);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead != null)
        {
            if (tag == "Bullet")
            {
                TransferDamage(bulletobj.Damage);
            }
        }
    }

    void TransferDamage(DamageInfo dmgInfo)
    {
        if (isDead) return;

        //������ �������� ������ HP�� ����
        curHp -= (int)dmgInfo.AttackPower;

        //������ �ؽ�Ʈ ���
        DamageTextMgr.Inst.AddText(dmgInfo.AttackPower, transform.position, transform.position);

        if (curHp <= 0)
        {
            dmgInfo.Attacker.SendMessage("ExpPoint", 10, SendMessageOptions.DontRequireReceiver);
            curHp = 0;
            nextState(State.DEATH);
        }
        else 
        { 
            nextState(State.HIT);
        }

    }
   


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
        ATTACK,
        HIT,
        DEATH,

    }

    Coroutine prevCoroutine = null;
    State curState = State.NONE; // �⺻ ����
    void nextState(State newState) // ���� ���·� �Ѿ�� (���¸� �Ű������� ����)
    {

    }

    IEnumerator IDLE_State() // ��� ����
    {
        myAnimator.SetBool("move",false);

        while (isDead==false) 
        {
            //targetPlayer = FindObjectOfType<PlayerController>();
            if (targetPlayer!=null) 
            {
                nextState(State.MOVE);
                yield break;
            }
            yield return null;
        }
        
    }
    IEnumerator MOVE_State() // �̵� ����
    {
        myAnimator.SetBool("move", true);
        while (isDead == false)
        {
            //targetPlayer = FindObjectOfType<PlayerController>();
            if (Vector3.Distance(targetPlayer.transform.position,transform.position)<=attackRange)
            {
                myAnimator.SetBool("move", false);
                nextState(State.ATTACK);
                yield break;
            }
            yield return null;
        }
        
    }
    IEnumerator ATTACK_State() // ���� ����
    {
        
        
    }
    IEnumerator HIT_State() // �ǰ� ����
    {
        //��¦�� ȿ��
        
    }
    IEnumerator DEATH_State() // ���� ����
    {
        //����� ȿ��
        
    }

}
