using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Monster[] MonsterPrefab;
    public static MonsterSpawner instance;

    [SerializeField] float spawnInterval = 10f;
    //[SerializeField] Monster Mobprefab;   // ���� ������

    //public Queue<GameObject> m_queue = new Queue<GameObject>();
   
    void Start()
    {
        StartCoroutine(processSpawn());
    }

    IEnumerator processSpawn()
    {
        while (true)
        {
            // spawnInterval ��ŭ ��ٸ��� 
            yield return new WaitForSeconds(spawnInterval);

            // ���͸� ���� spawnPoint �������� �����Ѵ�.
            Instantiate(MonsterPrefab[0], transform.position, transform.rotation);
        }
    }

    private void Update()
    {

    }
    

    /* public void InsertQueue(GameObject p_object) 
     { 
         m_queue.Enqueue (p_object); //���ӿ�����Ʈ�� ť�� ��
         p_object.SetActive(false);
     }
     public GameObject GetQueue()
     {
         GameObject m_object = m_queue.Dequeue(); //���ӿ�����Ʈ�� ť�� ����
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
