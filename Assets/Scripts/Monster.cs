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

    public bool CheckWithCollider(SphereCollider otherCollider)
    {
        monCC.bounds.Intersects(otherCollider.bounds); // bounds(���ѵȹڽ�)�� ��ġ�� ���� �ִ��� ����� ��
        return false;
    }
    void onAttackEvent()
    {
        //Debug.Log("## ������ �����̺�Ʈ ó���Լ�");
        targetPlayer.SendMessage("TransferDamage", attackPower, SendMessageOptions.DontRequireReceiver);
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
            // ����޽���
            //dmgInfo.Attacker.SendMessage("ExpPoint", 10, SendMessageOptions.DontRequireReceiver);
            curHp = 0;
            nextState(State.DEATH);
        }
        else 
        { 
            nextState(State.HIT);
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
                //nextState(State.ATTACK);
                yield break;
            }
            yield return null;
        }
        
    }
    
    IEnumerator HIT_State() // �ǰ� ����
    {
        //��¦�� ȿ��
        // �ǰ� ����Ʈ ���
        GameObject instObj = Instantiate(ResDataObj.EffHit, transEff.position, Quaternion.identity);
        Destroy(instObj, 2f); // 2�� �ڿ� �����ȴ�.

        // �ǰ� �ִϸ��̼� ���
        myAnimator.SetTrigger("hit");

        nextState(STATE.IDLE);

    }
    IEnumerator DEATH_State() // ���� ����
    {
        //����� ȿ��
        // �׾��ٸ� �״¾ִϸ��̼� ȣ��
        myAnimator.SetTrigger("death");
        yield return null;

        //
        //Recycle(gameObject);
        //gameObject.Recycle();

        MonsterPool.Inst.DestroyMonster(this);
    }

}
