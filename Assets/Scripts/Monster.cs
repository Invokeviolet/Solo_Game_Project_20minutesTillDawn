using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("[몬스터 정보]")]
    [SerializeField] GameObject monster; // 몬스터
    [SerializeField] int maxHp = 30; // 최대 체력
    [SerializeField] float attackRange = 0.1f; // 공격 가능 범위
    [SerializeField] float speed = 1.5f; // 이동 속도
    [SerializeField] ItemSpawner itemSpawner = null;

    public float attackPower = 1f; // 공격력

    int MonsterCount; // 몬스터의 카운트를 세어주는 역할 -> 최종 점수 체크때 사용 필요    

    public int curHp = 0; // 현재 체력

    bool isDead { get { return (curHp <= 0); } } // 죽었는지? bool 형 프로퍼티로 체력이 0이 되었을때 그냥 반환한다.    

    Vector3 direction; //움직일 위치값을 할당하기 위한 선언

    PlayerController targetPlayer = null; // 타겟 플레이어의 정보를 가져옴        
    SpriteRenderer MonsterRenderer; // 몬스터 스프라이트 렌더러

    // 위에서 선언한 정보 가져오기
    private void Awake()
    {
        targetPlayer = FindObjectOfType<PlayerController>();
        itemSpawner = FindObjectOfType<ItemSpawner>();        
        MonsterRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        curHp = maxHp; // 현재 체력을 최대 체력으로 갱신

    }
    void Start()
    {
        MonsterCount = 0; // 몬스터 카운트 = 0 으로 초기화
    }
    void Update()
    {
        if (isDead) return; // 죽었을때 반환
        MoveTarget(); // 타겟을 향해 자동으로 움직이는 메서드
        BoundaryCheck(); // 플레이어가 이동하면 몬스터가 그 근처로 이동하는 메서드
    }

    void MoveTarget()
    {
        if (targetPlayer == null) { return; } // 타겟 플레이어가 null 이면 그냥 반환
        direction = (targetPlayer.transform.position - transform.position).normalized; //목표 위치 - 나의 위치. 평준화       
        gameObject.transform.Translate(direction * speed * Time.deltaTime); // 게임오브젝트를 움직일거야 (방금 계산한 거리 * 시간)

        if (direction.x < 0) // 왼쪽
        {
            MonsterRenderer.flipX = true; // 왼쪽으로 뒤집기
        }
        else // 오른쪽
        {
            MonsterRenderer.flipX = false; // 오른쪽으로 뒤집기
        }
    }


    void onAttackEvent()
    {
        if (targetPlayer == null) { return; } // 플레이어가 null일때는 그냥 반환

        // 타겟 플레이어에게 메세지를 보냄(TransferDamage메서드 실행, 공격력 만큼, 메시지 옵션.메서드 반환자가 없는지 체크할건지?)
        targetPlayer.SendMessage("TransferDamage", attackPower, SendMessageOptions.DontRequireReceiver); 
    }

    public void TransferDamage(float dmgInfo) //메서드 (매개변수)
    {
        if (isDead) return; // 죽었다면 그냥 반환
                
        curHp -= (int)dmgInfo; //데미지 영향으로 본인의 HP가 변경

        if (curHp <= 0) // 현재 체력이 0 이하일 때
        {
            curHp = 0; // 현재 체력을 0 으로 제한

            nextState(State.DEATH); // 죽음 상태로 전환
        }
        else
        {
            nextState(State.HIT); // 맞는 상태로 전환
        }

    }

    void BoundaryCheck()
    {
        if (targetPlayer.transform.position.x - transform.position.x > 10) // 목표지점-나의지점이 > 뭐보다 클때 // 오른쪽으로 갈때
        {
            MoveMonster(2);
        }
        if (targetPlayer.transform.position.x - transform.position.x < -10) // 왼쪽으로 갈때
        {
            MoveMonster(0);
        }
        if (targetPlayer.transform.position.y - transform.position.y > 10) // 위로 갈때
        {
            MoveMonster(1);
        }
        if (targetPlayer.transform.position.y - transform.position.y < -10) // 아래로 갈때 
        {
            MoveMonster(3);
        }
    }


    void MoveMonster(int dir)
    {
        switch (dir)
        {
            case 0:
                transform.position += Vector3.left * 20; //왼쪽
                break;
            case 1:
                transform.position += Vector3.up * 20; // 위
                break;
            case 2:
                transform.position += Vector3.right * 20; // 오른쪽
                break;
            case 3:
                transform.position += Vector3.down * 20; // 아래
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet") // Bullet 이라는 태그를 가진 오브젝트와 충돌할때 넉백 효과
        {            
            direction = new Vector3(-0.1f, -0.1f, 0); 
            gameObject.transform.Translate(direction * speed * Time.deltaTime); // 게임오브젝트가 정해진 벡터 방향으로 이동됨

            TransferDamage(20f); // 총알 데미지 값이 여기로 들어옴

        }
    }
    //인터페이스를 써서 공격메소드 따로 만들것


    //---------------------------------------------------------------------------------------
    //
    // 상태를 나타내는 코루틴
    // 몬스터의 상태 ( 대기, 이동, 공격, 피격, 죽음 )
    //

    public enum State // 상태
    {
        NONE,
        IDLE,
        MOVE,
        HIT,
        DEATH,
        Restore

    }

    Coroutine prevCoroutine = null; // 코루틴 초기화
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
        while (!isDead) // 죽지 않았을때
        {
            // 현재 벡터 이동값을 플레이어 방향과 내 방향 거리가 공격범위 안에 있을 때
            if (Vector3.Distance(targetPlayer.transform.position, transform.position) <= attackRange) 
            {
                nextState(State.MOVE); // 이동 상태로 전환
                yield break;
            }
            yield return null;
        }

    }

    IEnumerator HIT_State() // 피격 상태
    {
        // 피격 이펙트 출력 : 하얀색으로 반짝 거림
        
        nextState(State.IDLE); // 일반 상태
        yield return null; // null 반환
    }

    IEnumerator DEATH_State() // 죽음 상태
    {
        MonsterCount++; // 몬스터 잡은 수 체크

        itemSpawner.ItemSpawn(transform.position); //경험치 아이템 떨구고
                
        gameObject.SetActive(false); // 죽으면 비활성화
        yield return null; // null 반환

        MonsterCount++; // 몬스터가 죽으면 카운트 + 1 -> 나중에 점수체크때 사용됨

        gameObject.Recycle(); // 몬스터를 재사용함
        
    }
    IEnumerator Restore_State() // 재생성할 때
    {
        //플레이어가 죽으면 몬스터는 자기가 스폰된 곳으로 다시 이동
        
        yield return null; // null 반환
    }

}
