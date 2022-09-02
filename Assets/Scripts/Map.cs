using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //�� �迭 ���� // ����ٰ� ���� ��������
    [SerializeField] private GameObject[] LandArray;
    //�÷��̾� // �÷��̾� ��ǥ���� �޾ƿ������ؼ� �����´� 
    [SerializeField] private Transform player;
    //Ÿ�� ũ�� // Ÿ���� ũ���Դϴ�. �Ҷ�Բ����� ���μ��� ũ�Ⱑ �ٸ��⶧���� ���� �ΰ��� �ؾߵ�
    [SerializeField] private float LandSize = 20;


    private void Update()
    {
        BoundaryCheck();
    }

    //Ÿ���� ���� ��.��.�� üũ
    void BoundaryCheck() // ť�� ���α��� / 2 
    {
        if (player.transform.position.x - transform.position.x > LandSize / 2) // �÷��̾��� x��ǥ�� ���� ��ǥ�� ������ ���� ���ݺ��� Ŭ�� // ���������� ����
        {
            MoveLand(2);
        }
        if (player.transform.position.x - transform.position.x < -LandSize / 2) // �������� ����
        {
            MoveLand(0);

        }
        if (player.transform.position.y - transform.position.y > LandSize / 2) // ���� ����
        {
            MoveLand(1);

        }
        if (player.transform.position.y - transform.position.y < -LandSize / 2) // �Ʒ��� ���� 
        {
            MoveLand(3);
        }
    }

    void MoveLand(int dir)
    {
        //���� �迭�� ����. LandArray��  ���ο�迭, _LandArray�� ���� �迭�� ���Ѵ�.
        GameObject[] _LandArray = new GameObject[9];
        System.Array.Copy(LandArray, _LandArray, 9);

        switch (dir)
        {
            case 0:
                {
                    transform.position += Vector3.left * 20; // 9������ǥ������ִ� ������� ��ǥ�� ť�� ������ ��ŭ�̵���


                    _LandArray[0] = LandArray[2];
                    _LandArray[1] = LandArray[0];
                    _LandArray[2] = LandArray[1];
                    _LandArray[3] = LandArray[5];
                    _LandArray[4] = LandArray[3];
                    _LandArray[5] = LandArray[4];
                    _LandArray[6] = LandArray[8];
                    _LandArray[7] = LandArray[6];
                    _LandArray[8] = LandArray[7];

                    LandArray[2].transform.position += Vector3.left * LandSize * 3; // ť�� ���α��� * 3��
                    LandArray[5].transform.position += Vector3.left * LandSize * 3;
                    LandArray[8].transform.position += Vector3.left * LandSize * 3;

                    System.Array.Copy(_LandArray, LandArray, 9);


                }
                break;
            case 1:
                {
                    transform.position += Vector3.up * LandSize; // ť�� ���α��� * 1��

                    _LandArray[0] = LandArray[6];
                    _LandArray[1] = LandArray[7];
                    _LandArray[2] = LandArray[8];
                    _LandArray[3] = LandArray[0];
                    _LandArray[4] = LandArray[1];
                    _LandArray[5] = LandArray[2];
                    _LandArray[6] = LandArray[3];
                    _LandArray[7] = LandArray[4];
                    _LandArray[8] = LandArray[5];

                    LandArray[6].transform.position += Vector3.up * LandSize * 3;
                    LandArray[7].transform.position += Vector3.up * LandSize * 3;
                    LandArray[8].transform.position += Vector3.up * LandSize * 3;

                    System.Array.Copy(_LandArray, LandArray, 9);
                }
                break;
            case 2:
                {
                    transform.position += Vector3.right * LandSize;

                    _LandArray[0] = LandArray[1];
                    _LandArray[1] = LandArray[2];
                    _LandArray[2] = LandArray[0];
                    _LandArray[3] = LandArray[4];
                    _LandArray[4] = LandArray[5];
                    _LandArray[5] = LandArray[3];
                    _LandArray[6] = LandArray[7];
                    _LandArray[7] = LandArray[8];
                    _LandArray[8] = LandArray[6];

                    LandArray[0].transform.position += Vector3.right * LandSize * 3;
                    LandArray[3].transform.position += Vector3.right * LandSize * 3;
                    LandArray[6].transform.position += Vector3.right * LandSize * 3;

                    System.Array.Copy(_LandArray, LandArray, 9);
                }
                break;
            case 3:
                {
                    transform.position += Vector3.down * LandSize;

                    _LandArray[0] = LandArray[3];
                    _LandArray[1] = LandArray[4];
                    _LandArray[2] = LandArray[5];
                    _LandArray[3] = LandArray[6];
                    _LandArray[4] = LandArray[7];
                    _LandArray[5] = LandArray[8];
                    _LandArray[6] = LandArray[0];
                    _LandArray[7] = LandArray[1];
                    _LandArray[8] = LandArray[2];

                    LandArray[0].transform.position += Vector3.down * LandSize * 3;
                    LandArray[1].transform.position += Vector3.down * LandSize * 3;
                    LandArray[2].transform.position += Vector3.down * LandSize * 3;

                    System.Array.Copy(_LandArray, LandArray, 9);
                }
                break;

        }
    }

}




