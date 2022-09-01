using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Monster[] MonsterPrefab;
    public static MonsterSpawner instance;

    [SerializeField] float spawnInterval = 10f;
    //[SerializeField] Monster Mobprefab;   // 몬스터 프리팹

    //public Queue<GameObject> m_queue = new Queue<GameObject>();
   
    void Start()
    {
        StartCoroutine(processSpawn());
    }

    IEnumerator processSpawn()
    {
        while (true)
        {
            // spawnInterval 만큼 기다리기 
            yield return new WaitForSeconds(spawnInterval);

            // 몬스터를 현재 spawnPoint 기준으로 생성한다.
            Instantiate(MonsterPrefab[0], transform.position, transform.rotation);
        }
    }

    private void Update()
    {

    }
    

    /* public void InsertQueue(GameObject p_object) 
     { 
         m_queue.Enqueue (p_object); //게임오브젝트가 큐로 들어감
         p_object.SetActive(false);
     }
     public GameObject GetQueue()
     {
         GameObject m_object = m_queue.Dequeue(); //게임오브젝트가 큐로 나옴
         m_object.SetActive(true);

         return m_object;
     }
     IEnumerator MonsterSpawn() 
     {
         while (true) 
         {
             if (m_queue.Count != 0) 
             {
                 Init();
                 GameObject t_object = GetQueue();
                 MovePos = new Vector2(xPos, zPos);
                 t_object.transform.position = gameObject.transform.position;
             }
             yield return new WaitForSeconds(1f);
         }
     }*/
}
