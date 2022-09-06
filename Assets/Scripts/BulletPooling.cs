using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------    
    //�̱���
    #region �̱���
    static public BulletPooling instance; // �ڽ��� static(��� ������ ��밡���ϵ���) ����
    public static BulletPooling Instance // �ڽ��� �������� �ʾ����� �������ִ� �޼���
    {
        get
        {
            if (instance == null) // �ڱ� �ڽ��� null ���̸�?
            {
                instance = FindObjectOfType<BulletPooling>(); // �ҷ�Ǯ���� ã�Ƽ� �־���.

                if (instance == null) // �׷��� ������?
                {
                    instance = new GameObject("BulletPooling").AddComponent<BulletPooling>(); // ���� ���� ������Ʈ�� ������. �ҷ�Ǯ���̶�� ������Ʈ�� ���ؼ�.
                }
            }
            return instance; // �׸��� ��ȯ
        }
    }
    #endregion
    //
    //---------------------------------------------------------------------------------------------

    Queue<BulletObject> pool = new Queue<BulletObject>(); // �ҷ�������Ʈ ������ Ǯ�� ���� ����.
    [SerializeField] GameObject BulletSpawner; // ������Ʈ ����
    public BulletObject CreateBullet(Vector3 pos, BulletObject bullet) //�ҷ�������Ʈ ������ �Ѿ˻����޼���(�޾ƿ� ��ġ��, �ҷ�������Ʈ ����)
    {
        BulletObject instbullet = null; // �ҷ�������Ʈ�� ��ü null���� �ʱ�ȭ

        if (pool.Count == 0) // Ǯ �ȿ� ������Ʈ�� ������ 0 �̸�
        {
            instbullet = Instantiate(bullet, pos, Quaternion.identity, BulletSpawner.transform); // instbullet�� ��ü�� �������ִ� ���� �Ҵ���
            return instbullet; // ������ ��ü�� ��ȯ -> Ǯ�� ��ȯ?
        }

        instbullet = pool.Dequeue(); // Ǯ���� ��������.
        instbullet.transform.position = pos; // ��ġ�� ������ ������ ��ġ������
        instbullet.transform.rotation = Quaternion.identity; // ȸ������
        instbullet.gameObject.SetActive(true); // ���ӿ�����Ʈ Ȱ��ȭ

        return instbullet; // ��ü�� ��ȯ
    }
    public void DestroyBullet(BulletObject bullobj) // �����Ǵ� �Ѿ� �޼��� ����(�Ѿ� ������Ʈ ���·� �޾ƿ��� �Ű�����)
    {
        bullobj.gameObject.SetActive(false); // �޾ƿ� �Ű������� ������Ʈ�� ��Ȱ��ȭ �Ѵ�.
        pool.Enqueue(bullobj); // ���� �Ѿ��� Enqueue�� ����Ͽ� Ǯ�� �ٽ� �־��ش�.
    }

}
