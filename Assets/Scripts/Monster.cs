using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] GameObject monster;
    [SerializeField] int MonsterCount = 10; // 웨이브에 따라 값이 바뀌어야 함
    [SerializeField] int maxHp = 100; // 체력
    [SerializeField] public float attackPower = 20f; // 공격력
    [SerializeField] float attackRange = 5f; // 공격 가능 범위
    [SerializeField] float speed = 5f; // 이동 속도

    int curHp = 0;
    bool isDead { get { return (curHp <= 0); } }

    Vector3 direction; //움직일 위치값을 할당하기 위한 선언

    public GameObject targetPlayer;
    Animator myAnimator = null;
    CapsuleCollider2D monCC = null;
    BulletObject bulletobj;
    SpriteRenderer renderer;
    Rigidbody2D Rigidbody;

    //public int MyPosIdx { get; set; } = -1; // 내 영역의 인덱스

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
        direction = (targetPlayer.transform.position - transform.position).normalized; //목표 위치 - 나의 위치. 평준화       
        gameObject.transform.Translate(direction * Time.deltaTime); // 게임오브젝트를 움직일거야 (방금 계산한 거리 * 시간)

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
        Debug.Log("## 몬스터의 공격이벤트 처리함수");
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

        //데미지 영향으로 본인의 HP가 변경
        curHp -= (int)dmgInfo.AttackPower;

        //데미지 텍스트 출력
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
    // 상태를 나타내는 코루틴
    // 몬스터의 상태 ( 대기, 이동, 공격, 피격, 죽음 )
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
    State curState = State.NONE; // 기본 상태
    void nextState(State newState) // 다음 상태로 넘어갈때 (상태를 매개변수로 받음)
    {

    }

    IEnumerator IDLE_State() // 대기 상태
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
    IEnumerator MOVE_State() // 이동 상태
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
    IEnumerator ATTACK_State() // 공격 상태
    {
        
        
    }
    IEnumerator HIT_State() // 피격 상태
    {
        //반짝임 효과
        
    }
    IEnumerator DEATH_State() // 죽음 상태
    {
        //사라짐 효과
        
    }

}
