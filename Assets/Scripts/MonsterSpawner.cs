using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;

    [SerializeField] Monster[] MonsterPrefab;
    [SerializeField] Transform playerPos;
    [SerializeField] float spawnInterval = 10f;
    

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
            //Instantiate(MonsterPrefab[0], transform.position, transform.rotation);
            MonsterPooling.Inst.CreateMonster(instMonster());
        }
    }

    private Vector3 instMonster()
    {
        float Xpos = Random.Range(playerPos.transform.position.x - 10, playerPos.transform.position.x + 10);
        float Ypos = Random.Range(playerPos.transform.position.y - 8, playerPos.transform.position.x + 8);

        Vector3 vecPos = new Vector3(Xpos, Ypos, 0);

        return vecPos;
    }

}
