using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("[몬스터 정보]")]
    [SerializeField] GameObject monster;
    [SerializeField] int maxHp = 30; // 최대 체력
    [SerializeField] public float attackPower = 20f; // 공격력
    [SerializeField] float attackRange = 0.1f; // 공격 가능 범위
    [SerializeField] float speed = 1.5f; // 이동 속도
    

    public int curHp = 0; // 현재 체력
    bool isDead { get { return (curHp <= 0); } }
    //bool isAttacked ; // 맞았나?

    Vector3 direction; //움직일 위치값을 할당하기 위한 선언

    //public GameObject targetPlayer;
    PlayerController targetPlayer = null;
    Animator myAnimator = null;
    CapsuleCollider2D monCC = null;
    BulletObject bulletobj;
    SpriteRenderer MonsterRenderer;
    Rigidbody2D Rigidbody;

     SpriteRenderer ColorRenderer; // 상태를 변경할 때 사용하기 위한 스프라이트 렌더러
    //public int MyPosIdx { get; set; } = -1; // 내 영역의 인덱스

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
        direction = (targetPlayer.transform.position - transform.position).normalized; //목표 위치 - 나의 위치. 평준화       
        gameObject.transform.Translate(direction * speed * Time.deltaTime); // 게임오브젝트를 움직일거야 (방금 계산한 거리 * 시간)

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
        //Debug.Log("## 몬스터의 공격이벤트 처리함수");
        targetPlayer.SendMessage("TransferDamage", attackPower, SendMessageOptions.DontRequireReceiver);
    }

    public void TransferDamage(float dmgInfo) //메서드 (매개변수)
    {
        if (isDead) return;

        //데미지 영향으로 본인의 HP가 변경
        curHp -= (int)dmgInfo;

        Debug.Log("## 데미지 들어왔니?");

        //데미지 텍스트 출력
        DamageTextMgr.Inst.AddText(dmgInfo, transform.position, transform.position); // 텍스트가 생성될 위치, 사라질 위치
        
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
            //Debug.Log("## 아!!!!!!!!!!!!!!!!!!!!!!!!!!");
            direction = new Vector3 (-0.1f, -0.1f, 0); // 넉백
            gameObject.transform.Translate(direction * speed * Time.deltaTime);

            //isAttacked = true;

            //myAnimator.SetBool("isAttacked", isAttacked);
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
        Restore

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
    IEnumerator MOVE_State() // 이동 상태
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

    IEnumerator HIT_State() // 피격 상태
    {
        // 피격 이펙트 출력 : 하얀색
        //ColorRenderer.material.color = Color.white; //색상 변경하기
        nextState(State.IDLE);
        yield return null;
    }
    IEnumerator DEATH_State() // 죽음 상태
    {
        // 죽으면? 사라짐

        yield return null;

        //Recycle(gameObject);
        gameObject.Recycle();

        //MonsterPooling.Instance.DestroyMonster(this);
    }
    IEnumerator Restore_State()
    {
        //플레이어가 죽으면 몬스터는 자기가 스폰된 곳으로 다시 이동

        yield return null;
    }

}
