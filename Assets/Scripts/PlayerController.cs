using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    [SerializeField] int maxHp =4;
    [SerializeField] float MoveSpeed=3.0f;
    [SerializeField] float AttackSpeed=1.8f;
    int curHp=0;

    Vector3 movePlayer = Vector3.zero;
    CharacterController myCC = null;
    Wepon wepon;
    void Start()
    {
        myCC = GetComponent<CharacterController>();
        wepon = GetComponent<Wepon>();
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
        }



        // 마우스로 적 조준하고 왼쪽키로 총알 발사
        if (Input.GetMouseButtonDown(0)) 
        {
            wepon.MoveBullet();
            Debug.Log("## 총알발사!!!");
        }
    }
}
