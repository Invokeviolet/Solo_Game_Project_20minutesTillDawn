using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObj : MonoBehaviour
{

    [SerializeField] GameObject[] MapObjPrefab;
    [SerializeField] Transform TargetCheckPos;
    SpriteRenderer ColorRenderer;
    Animator myAnimator;

    int MapObjectCount = 0;
    int DamagePower = 1;
    float InitDamageRange; //�ʱ�ȭ ���� ����
    bool isClosed;
    float DamageRange { get; set; } // ���ظ� �� ���� ����
    float Xpos;
    float Ypos;

    private void Awake()
    {
        ColorRenderer = GetComponent<SpriteRenderer>();
        isClosed = false;
        Xpos = Random.Range(TargetCheckPos.position.x - 8, TargetCheckPos.position.x + 8);
        Ypos = Random.Range(TargetCheckPos.position.y - 4, TargetCheckPos.position.y + 4);
    }
    void Start()
    {
        Vector3 RandomPos = Vector3.zero;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 RandomPos = new Vector3(Xpos, Ypos, 0);

    }


    public void SetTarget(Transform TargetCheckPos)
    {
        if (TargetCheckPos == null)
        {
            float distance = Vector3.Distance(transform.position, TargetCheckPos.transform.position);
            DamageRange = InitDamageRange;
            DamageRange += distance;
            TargetCheckPos = TargetCheckPos;
            myAnimator.SetBool("isClosed", isClosed);
        }
    }
    public bool CheckPos
    {
        get
        {
            return Vector3.Distance(transform.position, TargetCheckPos.transform.position) > DamageRange;
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if ((tag == "Player") || (tag == "Bullet"))
        {

            //��� ��������� ��¦���ٰ� �ٽ� ���ƿ� �����?
            //ColorRenderer.material.color = Color.yellow;
        }
    }
}
