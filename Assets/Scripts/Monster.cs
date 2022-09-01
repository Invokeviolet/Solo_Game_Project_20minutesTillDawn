using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{    
    
    [SerializeField] GameObject monster;
    public int rotateSpeed;
    [SerializeField] int MonsterCount = 300; // 웨이브에 따라 값이 바뀌어야 함
    [SerializeField] int maxHp = 100; // 체력
    [SerializeField] float attackPower = 20f; // 공격력
    [SerializeField] float attackRange = 5f; // 공격 가능 범위
    [SerializeField] float speed = 2f; // 이동 속도
    int curHp = 0;
    Vector3 direction;

    Rigidbody2D Rigidbody;
    public GameObject targetPlayer;
    public Transform myTarget { get; set; }
    //Animator myAnimator = null;
    bool isDead { get { return (curHp <= 0); } }

    /*// 몬스터의 초기 데이터를 셋업하는 메서드
    public void Setup(MonsterData monsterData) 
    {
        
    }*/
    private void Awake()
    {
        targetPlayer = FindObjectOfType<PlayerController>().gameObject;
        Rigidbody = GetComponent<Rigidbody2D>();
        //myAnimator = GetComponentInChildren<Animator>();
        
    }
    void Start()
    {
        curHp = maxHp;
        
        
    }
    
    void Update()
    {
        MoveTarget();
        //상태 (숨쉴때,공격할때,공격받았을때,)
        //웨이브에 따라 몹 개체 증가
        //if (myTarget != null)
        //{

            
            

        //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //    Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        //    Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
        //    transform.rotation = rotation;
        //}

    }
    void MoveTarget() 
    {
        direction = (targetPlayer.transform.position - transform.position).normalized;
        //Vector2 moveDir = (targetPlayer.transform.position - transform.position).normalized;
        //transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        gameObject.transform.Translate(direction * Time.deltaTime);
        //transform.position +
        //transform.LookAt(targetPlayer.transform.position);

    }

    /*void OnAttackEvent()
    {
        //Debug.Log("## 몬스터의 공격이벤트 처리함수");
        targetPlayer.SendMessage("TransferDamage", attackPower, SendMessageOptions.DontRequireReceiver);
    }

    void TransferDamage(float damageValue) 
    {
        if (isDead) return;
        //데미지 텍스트 출력
        DamageTextMgr.Inst.AddText(damageValue, transform.position, transform.position);

        curHp -= (int)damageValue;
        if (curHp<=0) 
        {
            curHp = 0;
            Die(); // 나중에 Enum 사용하여 정리
        }
    } */
    /*void Die() 
    { 
        if(isDead == true) 
        {
            Destroy(gameObject);
        }
        
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead!=null) 
        {
            if (tag=="Bullet") 
            {
                Destroy(gameObject);
            }
        }
    }
    
}
