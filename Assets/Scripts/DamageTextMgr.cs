using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextMgr : MonoBehaviour //수정필요
{
    static DamageTextMgr instance;
    public static DamageTextMgr Inst
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

    Canvas canvas = null;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }
    [SerializeField] Text prefabDamageText = null; // 데미지 텍스트 프리팹
    public void AddText(float damageValue, Vector3 mousePos, Vector3 offsetPos) // 점수 표시될 값, 표시될 위치값을 받아오자
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(mousePos + offsetPos);

        Text instTxt = Instantiate(prefabDamageText, screenPos, Quaternion.identity, canvas.transform);
    }

}
