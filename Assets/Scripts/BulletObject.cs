using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField] float ReloadTime = 1.0f;
    public enum State 
    { 
        Ready,
        Empty,
        Reloading
    }
    public State state { get; private set; }
    void Start()
    {

        
    }
    private IEnumerator ShotEffect(Vector2 hitPosition) 
    {
        // 0.03�� ���� ��� ó���� ���
        yield return new WaitForSeconds(ReloadTime);
    }

    
    void Update()
    {
        // Ű�Է��� �������� �Ѿ� ����
        
        if (Input.GetMouseButtonDown(0)) // ���콺Ŭ�� �� �Ѿ� ���
        {
            gameObject.SetActive(true); // �Ѿ� Ȱ��ȭ
            
        }

    }
}
