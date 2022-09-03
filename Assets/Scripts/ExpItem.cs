using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    [SerializeField] int ExpPiece; //일반몹이 드롭
    PlayerController player;
    void Awake()
    {
        ExpPiece = 10;
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Player") { player.ExpPoint++; }
    }
}
