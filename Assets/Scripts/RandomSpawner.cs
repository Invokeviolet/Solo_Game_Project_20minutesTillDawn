using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    //�̱���
    //
    #region �̱���

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
            // spawnInterval ��ŭ ��ٸ��� 
            yield return new WaitForSeconds(spawnInterval);

            int randomvalue = Random.Range(0, 3); // int �� ������ ���� ���� ���� ����
            RandomObj obj = RandomSpawner.instance.CreateObject(instRandomPos(), prefabObj[randomvalue]);
        }
    }

    private Vector3 instRandomPos()
    {
        //Debug.Log("## �����Ȱž� ģ����?");
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
    //������ƮǮ��
    //
    #region ������Ʈ Ǯ��
    public RandomObj CreateObject(Vector3 pos, RandomObj name)
    {
        RandomObj instObj = null; // ������ ���ÿ� ����ó���� ����
        
        if (pool.Count == 0)
        {

            instObj = Instantiate(name, Vector3.zero, Quaternion.identity, randomObj.transform);

            // �ε��� �������� �̿��ؼ� �ν���Ʈ ��ü �Ѱ��� �����.

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
        pool.Enqueue(obj); // pool �� 1�� �þ��.
    }

    #endregion
    //
    //--------------------------------------------------------------------------------------------------------------
}
