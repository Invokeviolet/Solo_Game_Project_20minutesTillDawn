using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxHp = 4;
    [SerializeField] float MoveSpeed = 3.0f;
    [SerializeField] float AttackSpeed = 1.8f;
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
            Destroy(gameObject);
            //죽음 애니메이션 출력
        }

    }
}
