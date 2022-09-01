using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/WeponData",fileName ="Wepon Data")]
public class GunData : ScriptableObject
{
    //소리
    //public AudioClip shotClip;

    [SerializeField] float ReloadTime = 1.0f; // 재장전 시간
    [SerializeField] int Maxbullet = 6; //최대 탄창 용량
    [SerializeField] int Startbullet = 6; //처음에 주어지는 탄창
    [SerializeField] float Damage = 20f; // 공격력
    [SerializeField] float AttackSpeed = 4.0f; //공격 속도
}
