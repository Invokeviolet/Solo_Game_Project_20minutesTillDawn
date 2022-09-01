using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //Ÿ���� ����� �迭. ���� ������ �Ʒ��� ����.
    //[0][0][0]
    //[0][0][0]
    //[0][0][0]

    [SerializeField] public GameObject[] landArray;

    //���ΰ� ������Ʈ
    [SerializeField] public GameObject hero;

    //Ÿ�� �ϳ��� ũ�� = 2
    [SerializeField] public float UnitSize;

    //���ΰ��� �̵� �ӵ�
    readonly float speed = 50f;

    //�þ�. �� �þ� �ۿ� Ÿ���� ������ Ÿ���� �����Ѵ�.
    readonly float halfSight = 2;

    //��ü Ÿ�� ũ��. ������� ����-�� ��ǥ, ������-�Ʒ� ��ǥ�� ����.
    Vector2[] border;


    void Start()
    {
        //border �ʱ�ȭ. ���ΰ��� (0,0)�� �����Ƿ� ��ü Ÿ���� ���� �� ��ǥ�� UnitSize*1.5�̴�.
        //���� ���ϸ� UnitSize*3�̸� �̴� ��ü Ÿ���� ũ��� ����(UnitSize�� Ÿ���� 3��)
        //���� ���� ���� ���� ������ �ʱ�ȭ�Ѵ�.
        border = new Vector2[]
        {
            new Vector2 (-UnitSize*1.5f,UnitSize*1.5f),
            new Vector2 (UnitSize*1.5f,-UnitSize*1.5f)
        };

    }


    void Update()
    {
        if (!Input.anyKey) return;
        //�̵� ������ ���Ѵ�.
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
        //���� �����ӿ� �̵��� �Ÿ��� ���Ѵ�.
        delta *= Time.deltaTime * speed;

        //���ΰ��� ��ġ�� ������Ʈ�Ѵ�.
        hero.transform.position += delta;

        //ī�޶��� ��ġ�� ������Ʈ�Ѵ�.
        Camera.main.transform.position += delta;

        //�þ� ���� �� Ÿ���� ���� ��츦 üũ�Ѵ�.
        BoundaryCheck();
    }
    void BoundaryCheck() 
    { 
        
    }



}
