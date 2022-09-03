using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxHp = 4;
    [SerializeField] float MoveSpeed = 3.0f;
    [SerializeField] float AttackSpeed = 1.8f;
    [SerializeField] float attackPower = 20f; // 공격력
    [SerializeField] float attackRange = 0.1f; // 공격 가능 범위
    int curHp = 0;

    bool isWalk = false;
    bool isDead = false;


    Vector3 movePlayer = Vector3.zero;
    CharacterController myCC = null;
    Animator myAnimator;
    Monster myMonster;
    Wepon wepon;

    SpriteRenderer ColorRenderer;

    void Start()
    {
        myCC = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
        wepon = GetComponent<Wepon>();
        myMonster = GetComponent<Monster>();
        ColorRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        bool isMove = (xAxis != 0) || (yAxis != 0);

        if (isMove)
        {
            //대각선이동
            movePlayer = Vector3.right * xAxis + Vector3.up * yAxis;
            myCC.Move(movePlayer * MoveSpeed * Time.deltaTime);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead != null)
        {
            if (tag == "Mob")
            {
                ColorRenderer.material.color = Color.white; //색상 변경하기
                DamageToMonster(myMonster.attackPower);
                
            }
        }
    }    

    void DamageToMonster(float damageValue)
    {
        if (isDead) return;

        curHp -= (int)damageValue;
        if (curHp <= 0)
        {
            curHp = 0;
            Die(); // 나중에 Enum 사용하여 정리
        }

    }
    void Die()
    {
        if (isDead == true)
        {
            ColorRenderer.material.color = Color.red; //색상 변경하기
            
            //죽을때? 기본 애니메이션들은 작동하지만 위치이동 불가. 캐릭터 오브젝트가 위에서부터 아래로 점점 사라짐

            Destroy(gameObject);

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
