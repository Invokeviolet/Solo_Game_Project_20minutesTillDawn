using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //타일을 담아줄 배열. 내부 구조는 아래와 같다.
    //[0][0][0]
    //[0][0][0]
    //[0][0][0]

    [SerializeField] public GameObject[] landArray;

    //주인공 오브젝트
    [SerializeField] public GameObject hero;

    //타일 하나의 크기 = 2
    [SerializeField] public float UnitSize;

    //주인공의 이동 속도
    readonly float speed = 50f;

    //시야. 이 시야 밖에 타일이 없으면 타일을 갱신한다.
    readonly float halfSight = 2;

    //전체 타일 크기. 순서대로 왼쪽-위 좌표, 오른쪽-아래 좌표가 들어간다.
    Vector2[] border;


    void Start()
    {
        //border 초기화. 주인공이 (0,0)에 있으므로 전체 타일의 왼쪽 끝 좌표는 UnitSize*1.5이다.
        //둘을 더하면 UnitSize*3이며 이는 전체 타일의 크기와 같다(UnitSize인 타일이 3개)
        //수직 방향 값도 같은 원리로 초기화한다.
        border = new Vector2[]
        {
            new Vector2 (-UnitSize*1.5f,UnitSize*1.5f),
            new Vector2 (UnitSize*1.5f,-UnitSize*1.5f)
        };

    }


    void Update()
    {
        if (!Input.anyKey) return;
        //이동 방향을 정한다.
        Vector3 delta;
        switch (Input.inputString)
        {
            case "w":
                delta = Vector2.up;
                break;
            case "a":
                delta = Vector2.left;
                break;
            case "s":
                delta = Vector2.down;
                break;
            case "d":
                delta = Vector2.right;
                break;
            default :
                return;
        }
        //지금 프레임에 이동할 거리를 구한다.
        delta *= Time.deltaTime * speed;

        //주인공의 위치를 업데이트한다.
        hero.transform.position += delta;

        //카메라의 위치를 업데이트한다.
        Camera.main.transform.position += delta;

        //시야 영역 중 타일이 없는 경우를 체크한다.
        BoundaryCheck();
    }
    void BoundaryCheck() 
    { 
        
    }



}
