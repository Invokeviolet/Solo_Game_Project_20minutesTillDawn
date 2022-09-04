using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxHp = 4;
    [SerializeField] float MoveSpeed = 100f;
    //[SerializeField] float attackPower = 20f; // 공격력
    //[SerializeField] float attackRange = 0.1f; // 공격 가능 범위
    int curHp = 0;
    public int ExpPoint = 0;

    bool isWalk;
    bool isDead;

    Vector2 movePlayer;
    Animator myAnimator;
    Monster myMonster;
    Wepon wepon;

    SpriteRenderer ColorRenderer;
    Rigidbody2D rigidbody2D;


    float TimeTrigger = 0f;
    float TimeRate = 1f;


    private void Awake()
    {
        curHp = maxHp;
    }
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        wepon = GetComponent<Wepon>();
        myMonster = GetComponent<Monster>();
        ColorRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        isDead = false;
        isWalk = false;

    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        bool isMove = (xAxis != 0) || (yAxis != 0);

        if (isMove)
        {
            //대각선이동
            movePlayer = Vector2.right * xAxis + Vector2.up * yAxis;
            rigidbody2D.MovePosition(rigidbody2D.position + (movePlayer * MoveSpeed * Time.deltaTime));
            isWalk = true;
            myAnimator.SetBool("isWalked", isWalk);
        }
        else
        {
            isWalk = false;
            myAnimator.SetBool("isWalked", false);
        }

    }

    //이동할때 마우스가 x < 0 이면 그대로 보던 방향을 보고
    //이동없이 마우스가 x < 0 이면 뒤집어주기
    private void OnTriggerStay2D(Collider2D collision)
    {

        TimeTrigger += Time.deltaTime;

        if (isDead) { return; }

        if (!isDead)
        {
            // 새로운 적이 닿았을 때 또 Hp 감소하는 것을 체크해주어야 함. -> 유니런 참고
            if (collision.tag == "Mob")
            {
                if (TimeTrigger >= TimeRate) 
                {
                    TimeTrigger = 0;

                    curHp--;
                    Debug.Log("## curHp : " + curHp);
                    //넉백당해야함                
                    DamageToMonster(myMonster.attackPower);

                }
            }

        }
    }

    void DamageToMonster(float damageValue)
    {
        if (isDead == true) return;

        curHp -= (int)damageValue;
        if (curHp <= 0)
        {
            curHp = 0;
            isDead = true;
            Die(); // 나중에 Enum 사용하여 정리
        }

    }
    void Die()
    {
        Debug.Log("## isDead : " + isDead);
        if (isDead == true)
        {
            Debug.Log("## isDead : " + isDead);
            ColorRenderer.material.color = Color.red; //색상 변경하기

            //죽을때? 기본 애니메이션들은 작동하지만 위치이동 불가. 캐릭터 오브젝트가 위에서부터 아래로 점점 사라짐
            Vector3 movePlayer = new Vector3(0, 0, 0);
        }

    }

    void GameOver()
    {
        //점수 표시되는 창 생성 -> 재시작 또는 홈으로 돌아가는 버튼
        //재시작 누르면 게임씬 새로 다시 출력
        //홈으로 누르면 타이틀씬 출력
        //데이터에 포인트 값 누적해서 저장되어야 하고, 그 값이
    }
}
