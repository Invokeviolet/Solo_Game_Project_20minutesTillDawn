using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ʈ�� ���콺�� ���� ������ ��ȯ�ϴ� Ŭ����
public class RotateFunction : MonoBehaviour
{
    
    SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer PlayerspriteRenderer;
    FlipObject flipObj;
    float angle;
    public Vector2 dir;
    float rad;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flipObj = GetComponent<FlipObject>();
    }
    private void Update()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = (mouse - transform.position).normalized;

        rad = Mathf.Atan2(dir.y, dir.x);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Flip();
    }
    void Flip() 
    {
        if (dir.x < 0) //���� ��ġ�� �����϶� ĳ���͸� ������
        {
            //Debug.Log("## ������");
            spriteRenderer.flipY = true;
            PlayerspriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipY = false;
            PlayerspriteRenderer.flipX = false;
        }
    }
}