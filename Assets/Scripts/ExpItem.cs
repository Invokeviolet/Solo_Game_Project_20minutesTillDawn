using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    [SerializeField] int ExpValue; //�Ϲݸ��� ���
    PlayerController player;

    
    void Awake()
    {
        ExpValue = 10;
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
