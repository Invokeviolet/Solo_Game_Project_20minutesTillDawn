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
        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(ReloadTime);
    }

    
    void Update()
    {
        // 키입력이 없을때는 총알 없기
        
        if (Input.GetMouseButtonDown(0)) // 마우스클릭 시 총알 뱉기
        {
            gameObject.SetActive(true); // 총알 활성화
            
        }

    }
}
