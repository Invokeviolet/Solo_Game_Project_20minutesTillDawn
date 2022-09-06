using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextMgr : MonoBehaviour 
{
    static DamageTextMgr instance;

    //싱글턴
    #region
    public static DamageTextMgr Inst // 데미지 텍스트를 싱글턴으로 생성
    {
        get 
        {
            if (instance==null) 
            { 
                instance = FindObjectOfType<DamageTextMgr>();
                if (instance==null) 
                {
                    instance = new GameObject("DamageTextMgr").AddComponent<DamageTextMgr>();
                }
            }
            return instance;
        }
    }
    #endregion


    Canvas canvas = null;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }


    [SerializeField] Text prefabDamageText = null; // 데미지 텍스트 프리팹


    public void AddText(float damageValue, Vector3 mousePos, Vector3 offsetPos) // 점수 표시될 값, 표시될 위치값을 받아오자
    {
        Debug.Log("## 데미지 텍스트 떴니?");
        Vector3 screenPos = Camera.main.WorldToScreenPoint(mousePos + offsetPos);

        Text instTxt = Instantiate(prefabDamageText, screenPos, Quaternion.identity, canvas.transform);
    }

}
