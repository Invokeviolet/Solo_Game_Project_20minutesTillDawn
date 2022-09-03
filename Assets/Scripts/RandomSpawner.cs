using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    //싱글턴
    //
    #region 싱글턴

    private RandomSpawner() { }
    static RandomSpawner instance;
    public static RandomSpawner Inst
    {
        get
        {
            if (instance == null)
            {

                instance = FindObjectOfType<RandomSpawner>();
                if (instance == null)
                {
                    instance = new GameObject("RandomSpawner").AddComponent<RandomSpawner>();
                }

            }
            return instance;
        }
    }

    #endregion
    //
    //------------------------------------------------------------------------------------------


    [SerializeField] RandomSpawner ObjSpawner;
    [SerializeField] RandomObj[] prefabObj = null;
    [SerializeField] Transform playerPos;

    Queue<RandomObj> pool = new Queue<RandomObj>();
    RandomObj randomObj;

    float spawnInterval;

    void Awake()
    {
        randomObj = FindObjectOfType<RandomObj>();
        StartCoroutine(processSpawn());
    }

    void Update()
    {


    }

    IEnumerator processSpawn()
    {
        spawnInterval = Random.RandomRange(3f, 10f);
        while (true)
        {
            // spawnInterval 만큼 기다리기 
            yield return new WaitForSeconds(spawnInterval);

            int randomvalue = Random.Range(0, 3); // int 는 마지막 값이 포함 되지 않음
            RandomObj obj = RandomSpawner.instance.CreateObject(instRandomPos(), prefabObj[randomvalue]);
        }
    }

    private Vector3 instRandomPos()
    {
        //Debug.Log("## 생성된거야 친구야?");
        float Xpos = Random.Range(playerPos.transform.position.x - 20, playerPos.transform.position.x + 20);
        float Ypos = Random.Range(playerPos.transform.position.y - 20, playerPos.transform.position.y + 20);


        if ((Xpos >= (playerPos.position.x - 10)) || (Xpos <= (playerPos.position.x + 10)) || (Ypos >= (playerPos.position.x - 10)) || (Ypos <= (playerPos.position.x + 10)))
        {
            Xpos = Random.Range(playerPos.transform.position.x - 10, playerPos.transform.position.x + 10);
            Ypos = Random.Range(playerPos.transform.position.y - 20, playerPos.transform.position.y + 20);
        }

        Vector3 RandomPos = new Vector3(Xpos, Ypos, 0);
        return RandomPos;
    }


    //--------------------------------------------------------------------------------------------------------------
    //오브젝트풀링
    //
    #region 오브젝트 풀링
    public RandomObj CreateObject(Vector3 pos, RandomObj name)
    {
        RandomObj instObj = null; // 생성과 동시에 예외처리를 해줌
        
        if (pool.Count == 0)
        {

            instObj = Instantiate(name, Vector3.zero, Quaternion.identity, randomObj.transform);

            // 로드한 프리팹을 이용해서 인스턴트 객체 한개를 만든다.

            return instObj;

        }


        instObj = pool.Dequeue();
        //instMob.transform.parent = null;
        instObj.transform.position = pos;
        instObj.transform.rotation = Quaternion.identity;
        instObj.gameObject.SetActive(true);

        //if (pool.Count > 0)
        return instObj;
    }

    public void DestroyObject(RandomObj obj)
    {        
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj); // pool 에 1개 늘어난다.
    }

    #endregion
    //
    //--------------------------------------------------------------------------------------------------------------
}
