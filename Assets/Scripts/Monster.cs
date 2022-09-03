using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("[���� ����]")]
    [SerializeField] GameObject monster;
    [SerializeField] int maxHp = 100; // �ִ� ü��
    [SerializeField] public float attackPower = 20f; // ���ݷ�
    [SerializeField] float attackRange = 0.1f; // ���� ���� ����
    [SerializeField] float speed = 4f; // �̵� �ӵ�
    [SerializeField] public int MonsterCount = 10; // ���̺꿡 ���� ���� �ٲ��� ��

    int curHp = 0; // ���� ü��
    bool isDead { get { return (curHp <= 0); } }

    Vector3 direction; //������ ��ġ���� �Ҵ��ϱ� ���� ����

    //public GameObject targetPlayer;
    PlayerController targetPlayer = null;
    Animator myAnimator = null;
    CapsuleCollider2D monCC = null;
    BulletObject bulletobj;
    SpriteRenderer renderer;
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
        ColorRenderer.material.color = Color.white; //���� �����ϱ�
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
