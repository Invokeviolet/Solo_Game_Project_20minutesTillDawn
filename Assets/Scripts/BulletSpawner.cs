using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;    
    bool isShoot = true;
    BulletObject bulletObject;
    public GameObject mousetransform;
    
    void Start()
    {
        bulletObject = GetComponent<BulletObject>();
        
    }
    void Update()
    {
        ShootControl();

    }
    
    
    void ShootControl()
    {
        if (isShoot)
        {
            if (Input.GetMouseButtonDown(0)) // 마우스클릭 시 총알 뱉기
            {                
                GameObject bullet =Instantiate(BulletPrefab, transform.position, transform.rotation);
               // bullet.transform.LookAt(mousetransform.transform);
                
                
                /*if (bulletObject.Curbullet < 0) 
                {
                    //재장전
                }*/
            }
        }

    }
}
