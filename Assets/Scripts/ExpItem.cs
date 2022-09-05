using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    [SerializeField] int ExpValue; //일반몹이 드롭
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
