using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextMgr : MonoBehaviour 
{
    static DamageTextMgr instance;

    //�̱���
    #region
    public static DamageTextMgr Inst // ������ �ؽ�Ʈ�� �̱������� ����
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


    [SerializeField] Text prefabDamageText = null; // ������ �ؽ�Ʈ ������


    public void AddText(float damageValue, Vector3 mousePos, Vector3 offsetPos) // ���� ǥ�õ� ��, ǥ�õ� ��ġ���� �޾ƿ���
    {
        Debug.Log("## ������ �ؽ�Ʈ ����?");
        Vector3 screenPos = Camera.main.WorldToScreenPoint(mousePos + offsetPos);

        Text instTxt = Instantiate(prefabDamageText, screenPos, Quaternion.identity, canvas.transform);
    }

}
