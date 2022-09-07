using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트가 마우스를 따라 방향을 전환하는 클래스
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
        if (dir.x < 0) //총의 위치가 음수일때 캐릭터만 뒤집기
        {
            //Debug.Log("## 뒤집기");
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