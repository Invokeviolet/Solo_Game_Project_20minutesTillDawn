using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트를 뒤집기 위한 클래스
public class FlipObject : MonoBehaviour
{
    SpriteRenderer FlipRenderer;    

    void Start()
    {
        FlipRenderer = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        
        Vector2 flipMove = Vector2.zero;
        if (Input.GetAxis("Horizontal")<0) //마우스위치가 음수일때 플레이어 몸도 돌아감
        { 
            flipMove = Vector2.left;
            FlipRenderer.flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            flipMove = Vector2.right;
            FlipRenderer.flipX = false;
        }
    }
}
