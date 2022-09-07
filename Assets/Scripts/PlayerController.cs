using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;
using System.ComponentModel;

public class PlayerController : MonoBehaviour
{
    [Header("[플레이어 정보]")]
    [SerializeField] Text LevelText; // 게임 중 화면 중앙 상단에 표시 될 레벨

    [SerializeField] int maxHp = 4;
    [SerializeField] float MoveSpeed = 6f;

    [SerializeField] GameObject expItemObj; // 경험치 아이템
    [SerializeField] Monster monster;

    public int curHp = 0; // 현재 Hp

    bool isWalk; // 걷는지?
    bool isDead; // 죽었는지?    

    Vector2 movePlayer; // 플레이어의 벡터값
    Animator myAnimator; // 애니메이션    


    SpriteRenderer ColorRenderer;
    Rigidbody2D rigidbody2D;

    float TimeTrigger = 0f; // 현재 충돌 시간
    float TimeRate = 0.5f; // 충돌 후 경과시간

    Wepon_Gun wepon;
    private void Awake()
    {
        curHp = maxHp; // 현재 Hp 값을 최대 값으로 설정
        gameObject.SetActive(true); // 플레이어 오브젝트 활성화

    }

    // 각 컴포넌트 및 오브젝트 찾아오기
    void Start()
    {

        myAnimator = GetComponent<Animator>();
        ColorRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        wepon = FindObjectOfType<Wepon_Gun>();

        isDead = false; // 죽었는지? 값 초기화
        isWalk = false; // 걸었는지? 값 초기화

    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal"); // 수평이동
        float yAxis = Input.GetAxis("Vertical"); // 수직이동

        bool isMove = (xAxis != 0) || (yAxis != 0); // bool 형태로 움직임이 없는 값을 확인

        if (isMove) // 움직임이 있을 때
        {
            movePlayer = (Vector2.right * xAxis + Vector2.up * yAxis).normalized; // 플레이어의 대각선이동을 nomalized 를 사용해 이동값을 모두 1로 받는다.

            rigidbody2D.MovePosition(rigidbody2D.position + (movePlayer * MoveSpeed * Time.deltaTime)); // 이동하는 값 = 리지드바디 위치()

            // 리지드바디.무브포지션으로 포지션값을 잡은 이유?
            // 중력이 없는 움직임에 사용하기에 좋다.
            // 리지드바디 위치로 받으면 충돌체크를 무시하는 경우가 낮아져서 사용

            isWalk = true; // 걷고 있다면? 걷는 애니메이션 true
            myAnimator.SetBool("isWalked", isWalk);
        }
        else
        {
            isWalk = false; // 안 걷고 있다면? 걷는 애니메이션 false
            myAnimator.SetBool("isWalked", isWalk);
        }

    }

    private void OnTriggerStay2D(Collider2D collision) // 콜라이더 스테이 -> 몹
    {

        TimeTrigger += Time.deltaTime; // 충돌 시간 += 시간 초단위로 계산해줌

        if (isDead) { return; } // 죽었을 때는 그냥 반환

        if (!isDead) // 죽지 않았을 때
        {

            if (collision.tag == "Mob") // 몹태그를 가진 오브젝트와 충돌했을 때
            {
                // 경과시간이 충돌 시간보다 클 때
                if (TimeTrigger >= TimeRate) // 자주 중복되어 충돌되는 현상을 제거하기 위해 
                {
                    TimeTrigger = 0; // 충돌시간을 0으로 초기화

                    DamageToMonster(monster.attackPower); // 충돌했을 때 Hp 변화 값을 주고받기 위한 메서드 생성

                }
            }

        }
    }

    public void DamageToMonster(float damageValue) // 대미지 값을 float 형으로 받아옴
    {

        if (isDead == true) return; // 죽었을 때는 그냥 반환


        curHp -= (int)damageValue; //현재 Hp를 int형으로 변환하여 값을 받음

        if (curHp <= 0) // 만약 현재 Hp가 0보다 작거나 같을 때
        {

            curHp = 0; // 현재 Hp를 0으로 제한
            isDead = true; // 죽음 = true
            StartCoroutine(Die_State()); // 죽었을때 플레이어에게 변화를 줄 메서드 소환
        }

    }

    public IEnumerator Die_State()
    {
        ColorRenderer.material.color = Color.red; // 플레이어가 죽으면 색상 변경하기

        wepon.SetOff();

        isDead = true;
        myAnimator.SetBool("isDead", isDead);

        //Vector2 vec = new Vector2(transform.position,transform.rotation);

        yield return new WaitForSeconds(6f);

        // UI 상태를 게임오버상태로 전환
        UIManager.Instance.GameOver();
    }

}
