using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPooling : MonoBehaviour
{
    private MonsterPooling() { }
   static MonsterPooling instance = null;
    public static MonsterPooling Instance
    {
        get 
        {
            if (instance==null) 
            { 
                instance = FindObjectOfType<MonsterPooling>();
                if (instance==null) 
                {
                    instance = new GameObject("Game").AddComponent<MonsterPooling>();
                }
            }
            return instance; 
        }
    }

    Queue<Monster> pool = new Queue<Monster>();
    [SerializeField] Monster prefab;

    public Monster Create() 
    {
        Monster obj = Instantiate(prefab);
        return obj;
    }
    public Monster GetMonster() 
    {

        if (pool.Count > 0)
        {
            //pool.Dequeue();
            return pool.Dequeue();
        }
        else 
        { 
           
           return Create();
           
        }
        
    }



}
