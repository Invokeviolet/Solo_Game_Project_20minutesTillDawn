using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/WeponData",fileName ="Wepon Data")]
public class GunData : ScriptableObject
{
    //�Ҹ�
    //public AudioClip shotClip;

    [SerializeField] float ReloadTime = 1.0f; // ������ �ð�
    [SerializeField] int Maxbullet = 6; //�ִ� źâ �뷮
    [SerializeField] int Startbullet = 6; //ó���� �־����� źâ
    [SerializeField] float Damage = 20f; // ���ݷ�
    [SerializeField] float AttackSpeed = 4.0f; //���� �ӵ�
}
