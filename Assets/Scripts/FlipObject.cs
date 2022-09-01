using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipObject : MonoBehaviour
{
    public SpriteRenderer renderer;
    
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 flipMove = Vector2.zero;
        if (Input.GetAxis("Horizontal")<0) 
        { 
            flipMove = Vector2.left;
            renderer.flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            flipMove = Vector2.right;
            renderer.flipX = false;
        }
    }
}
