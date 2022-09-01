using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] float NormalmobExp = 10;
    [SerializeField] float ElitemobExp = 50;
    [SerializeField] float BossmobExp = 100;

    void Start()
    {
        
    }
        
    void Update()
    {
        //상태 (숨쉴때,공격할때,공격받았을때,)
        //웨이브에 따라 몹 개체 증가
    }
}
