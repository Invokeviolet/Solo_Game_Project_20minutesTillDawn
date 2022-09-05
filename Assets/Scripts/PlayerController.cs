using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Text LevelText; // 게임 중 화면 중앙 상단에 표시 될 레벨
    [SerializeField] int Level = 1;
    [SerializeField] int LevelUp=1;
    [SerializeField] int maxHp = 4;
    [SerializeField] float MoveSpeed = 100f;
    [SerializeField] public  float absorbArange = 3f;
    [SerializeField] GameObject expItemObj; // 경험치 아이템


    public int curHp = 0;
    public int curExpPoint = 0;
    public int plusExpPoint = 10;

    bool isWalk;
    bool isDead;


    ExpItem expItem;


    Vector2 movePlayer;
    Animator myAnimator;
    Monster myMonster;
    Wepon wepon;
    UIManager uiManager;

    SpriteRenderer ColorRenderer;
    Rigidbody2D rigidbody2D;    

    float TimeTrigger = 0f;
    float TimeRate = 1f;


    private void Awake()
    {
        curHp = maxHp;
        gameObject.SetActive(true);
        Debug.Log("## 경험치 : " + curExpPoint);
    }
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        myMonster = FindObjectOfType<Monster>();
        expItem = FindObjectOfType<ExpItem>();
        myAnimator = GetComponent<Animator>();
        wepon = GetComponent<Wepon>();
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
    private void OnTriggerStay2D(Collider2D collision) // 콜라이더 스테이 -> 몹
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
                                        
                    //넉백당해야함
                    DamageToMonster(myMonster.attackPower);

                }
            }
            
        }
    }

    //경험치 아이템 먹었을 때 경험치 추가 -> 슬라이더에 값 옮겨주기
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Vector2 absorbDistance = transform.position - transform.position;
        Debug.Log("## 경험치 먹었다");
        if (tag == "Exp_Item")
        {
            curExpPoint += expItem.ExpValue;

            //플레이어의 사정거리 범위내에 있을때 아이템 흡수
            Debug.Log("## 경험치 : " + curExpPoint);
            if (curExpPoint >= 100) //Exp가 100이 넘으면 레벨업
            {
                curExpPoint = 0;
                Level += LevelUp;
                LevelText.text = "Level" + "     " + Level;
            }
        }
    }

    void DamageToMonster(float damageValue)
    {
        
        if (isDead == true) return;

        Debug.Log("## curHp : "+ curHp);

        curHp -= (int)damageValue;
        
        if (curHp <= 0)
        {
            Debug.Log("## change curHp : " + curHp);
            curHp = 0;
            isDead = true;
            Die(); // 나중에 Enum 사용하여 정리
        }

    }
    void Die()
    {
        //Debug.Log("## isDead : " + isDead);
        if (isDead == true)
        {
            Debug.Log("## isDead : " + isDead);
            ColorRenderer.material.color = Color.red; //색상 변경하기

            //죽을때? 기본 애니메이션들은 작동하지만 위치이동 불가. 캐릭터 오브젝트가 위에서부터 아래로 점점 사라짐
            gameObject.SetActive(false);
            uiManager.GameOver();
        }

    }

    
}
