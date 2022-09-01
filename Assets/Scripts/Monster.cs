using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{    
    
    [SerializeField] GameObject monster;
    public int rotateSpeed;
    [SerializeField] int MonsterCount = 300; // ���̺꿡 ���� ���� �ٲ��� ��
    [SerializeField] int maxHp = 100; // ü��
    [SerializeField] float attackPower = 20f; // ���ݷ�
    [SerializeField] float attackRange = 5f; // ���� ���� ����
    [SerializeField] float speed = 2f; // �̵� �ӵ�
    int curHp = 0;
    Vector3 direction;

    Rigidbody2D Rigidbody;
    public GameObject targetPlayer;
    public Transform myTarget { get; set; }
    //Animator myAnimator = null;
    bool isDead { get { return (curHp <= 0); } }

    /*// ������ �ʱ� �����͸� �¾��ϴ� �޼���
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
        //���� (������,�����Ҷ�,���ݹ޾�����,)
        //���̺꿡 ���� �� ��ü ����
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
        //Debug.Log("## ������ �����̺�Ʈ ó���Լ�");
        targetPlayer.SendMessage("TransferDamage", attackPower, SendMessageOptions.DontRequireReceiver);
    }

    void TransferDamage(float damageValue) 
    {
        if (isDead) return;
        //������ �ؽ�Ʈ ���
        DamageTextMgr.Inst.AddText(damageValue, transform.position, transform.position);

        curHp -= (int)damageValue;
        if (curHp<=0) 
        {
            curHp = 0;
            Die(); // ���߿� Enum ����Ͽ� ����
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
