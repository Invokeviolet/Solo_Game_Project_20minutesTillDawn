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
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Player") 
        { 
            player.ExpPoint+= ExpValue;
            
        }
    }
}
