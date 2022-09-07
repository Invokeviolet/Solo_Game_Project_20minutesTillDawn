using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ʈ�� ������ ���� Ŭ����
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
        if (Input.GetAxis("Horizontal")<0) //���콺��ġ�� �����϶� �÷��̾� ���� ���ư�
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
