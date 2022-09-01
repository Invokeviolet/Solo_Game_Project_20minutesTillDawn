using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //맵 배열 선언 // 여기다가 맵을 넣을거임
    [SerializeField] private GameObject[] LandArray;
    //플레이어 // 플레이어 좌표값을 받아오기위해서 가져온다 
    [SerializeField] private PlayerMove player;
    //타일 크기 // 타일의 크기입니다. 소라님께서는 가로세로 크기가 다르기때문에 변수 두개로 해야됨
    [SerializeField] private float LandSize = 50f;




    private void Update()
    {
        BoundaryCheck();
    }

    //타일의 어디로 옮.길.지 체크
    void BoundaryCheck()
    {
        if (player.transform.position.x - transform.position.x > 25) // 플레이어의 x좌표와 맵의 좌표를 뺐을때 맵의 절반보다 클때 // 오른쪽으로 갈때
        {
            MoveLand(2);
        }
        if (player.transform.position.x - transform.position.x < -25) // 왼쪽으로 갈때
        {
            MoveLand(0);

        }
        if (player.transform.position.z - transform.position.z > 25) // 위로 갈때
        {
            MoveLand(1);

        }
        if (player.transform.position.z - transform.position.z < -25) // 아래로 갈때
        {
            MoveLand(3);
        }
    }

    void MoveLand(int dir)
    {
        //기존 배열을 복사. LandArray는  새로운배열, _LandArray는 기존 배열을 뜻한다.
        GameObject[] _LandArray = new GameObject[9];
        System.Array.Copy(LandArray, _LandArray, 9);

        switch (dir)
        {
            case 0:
                {
                    transform.position += Vector3.left * 50; // 9개의좌표가들어있는 월드맵의 좌표를 50만큼이동함


                    _LandArray[0] = LandArray[2];
                    _LandArray[1] = LandArray[0];
                    _LandArray[2] = LandArray[1];
                    _LandArray[3] = LandArray[5];
                    _LandArray[4] = LandArray[3];
                    _LandArray[5] = LandArray[4];
                    _LandArray[6] = LandArray[8];
                    _LandArray[7] = LandArray[6];
                    _LandArray[8] = LandArray[7];

                    LandArray[2].transform.position += Vector3.left * 150;
                    LandArray[5].transform.position += Vector3.left * 150;
                    LandArray[8].transform.position += Vector3.left * 150;

                    System.Array.Copy(_LandArray, LandArray, 9);


                }
                break;
            case 1:
                {
                    transform.position += Vector3.forward * 50;

                    _LandArray[0] = LandArray[6];
                    _LandArray[1] = LandArray[7];
                    _LandArray[2] = LandArray[8];
                    _LandArray[3] = LandArray[0];
                    _LandArray[4] = LandArray[1];
                    _LandArray[5] = LandArray[2];
                    _LandArray[6] = LandArray[3];
                    _LandArray[7] = LandArray[4];
                    _LandArray[8] = LandArray[5];

                    LandArray[6].transform.position += Vector3.forward * 150;
                    LandArray[7].transform.position += Vector3.forward * 150;
                    LandArray[8].transform.position += Vector3.forward * 150;

                    System.Array.Copy(_LandArray, LandArray, 9);
                }
                break;
            case 2:
                {
                    transform.position += Vector3.right * 50;

                    _LandArray[0] = LandArray[1];
                    _LandArray[1] = LandArray[2];
                    _LandArray[2] = LandArray[0];
                    _LandArray[3] = LandArray[4];
                    _LandArray[4] = LandArray[5];
                    _LandArray[5] = LandArray[3];
                    _LandArray[6] = LandArray[7];
                    _LandArray[7] = LandArray[8];
                    _LandArray[8] = LandArray[6];

                    LandArray[0].transform.position += Vector3.right * 150;
                    LandArray[3].transform.position += Vector3.right * 150;
                    LandArray[6].transform.position += Vector3.right * 150;

                    System.Array.Copy(_LandArray, LandArray, 9);
                }
                break;
            case 3:
                {
                    transform.position += Vector3.back * 50;

                    _LandArray[0] = LandArray[3];
                    _LandArray[1] = LandArray[4];
                    _LandArray[2] = LandArray[5];
                    _LandArray[3] = LandArray[6];
                    _LandArray[4] = LandArray[7];
                    _LandArray[5] = LandArray[8];
                    _LandArray[6] = LandArray[0];
                    _LandArray[7] = LandArray[1];
                    _LandArray[8] = LandArray[2];

                    LandArray[0].transform.position += Vector3.back * 150;
                    LandArray[1].transform.position += Vector3.back * 150;
                    LandArray[2].transform.position += Vector3.back * 150;

                    System.Array.Copy(_LandArray, LandArray, 9);
                }
                break;

        }
    }

}




