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

    public bool CheckWithCollider(SphereCollider otherCollider)
    {
        monCC.bounds.Intersects(otherCollider.bounds); // bounds(제한된박스)와 겹치는 곳이 있는지 물어보는 것
        return false;
    }
    void onAttackEvent()
    {
        //Debug.Log("## 몬스터의 공격이벤트 처리함수");
        targetPlayer.SendMessage("TransferDamage", attackPower, SendMessageOptions.DontRequireReceiver);
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
            // 보상메시지
            //dmgInfo.Attacker.SendMessage("ExpPoint", 10, SendMessageOptions.DontRequireReceiver);
            curHp = 0;
            nextState(State.DEATH);
        }
        else 
        { 
            nextState(State.HIT);
        }

    }
   

    //인터페이스를 써서 공격메소드 따로 만들것


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
        HIT,
        DEATH,

    }

    Coroutine prevCoroutine = null;
    State curState = State.NONE; // 기본 상태
    void nextState(State newState) // 다음 상태로 넘어갈때 (상태를 매개변수로 받음)
    {
        if (newState == curState) return;
        if (prevCoroutine != null) StopCoroutine(prevCoroutine);

        curState = newState;
        prevCoroutine = StartCoroutine(newState.ToString() + "_State");
        
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
                //nextState(State.ATTACK);
                yield break;
            }
            yield return null;
        }
        
    }
    
    IEnumerator HIT_State() // 피격 상태
    {
        //반짝임 효과
        // 피격 이펙트 출력
        GameObject instObj = Instantiate(ResDataObj.EffHit, transEff.position, Quaternion.identity);
        Destroy(instObj, 2f); // 2초 뒤에 삭제된다.

        // 피격 애니메이션 출력
        myAnimator.SetTrigger("hit");

        nextState(STATE.IDLE);

    }
    IEnumerator DEATH_State() // 죽음 상태
    {
        //사라짐 효과
        // 죽었다면 죽는애니메이션 호출
        myAnimator.SetTrigger("death");
        yield return null;

        //
        //Recycle(gameObject);
        //gameObject.Recycle();

        MonsterPool.Inst.DestroyMonster(this);
    }

}
