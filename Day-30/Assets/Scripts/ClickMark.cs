using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMark : MonoBehaviour
{
    HeroCtrl m_RefHero = null;
    //히어로 스크립트 연결용 변수
    Vector3 m_CacVLen = Vector3.zero;

    float m_AddTimer = 0.0f;
    bool  m_IsOnOff = true;
    Renderer m_RefRender;
    //Color32 m_WtColor = new Color32(255, 255, 255, 200);
    Color32 m_WtColor = new Color32(255, 247, 119, 45);
    Color32 m_BrColor = new Color32(0, 130, 255, 45);

    // Start is called before the first frame update
    void Start()
    {
        m_RefRender = gameObject.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //## 깜빡임 연출
        if(m_RefRender != null)
        {
            m_AddTimer += Time.deltaTime;   
            if(0.25f <= m_AddTimer)
            {
                m_IsOnOff = !m_IsOnOff;
                if (m_IsOnOff == true)
                    m_RefRender.material.SetColor("_TintColor", m_WtColor);
                else
                    m_RefRender.material.SetColor("_TintColor", m_BrColor);

                m_AddTimer = 0.0f;
            }
        }
        
    }

    public void PlayEff(Vector3 a_PickVec, HeroCtrl a_RefHero)
    {
        m_RefHero = a_RefHero;

        transform.position = new Vector3(a_PickVec.x, 0.8f, a_PickVec.z);
        gameObject.SetActive(true);
    }

    public void ClickMarkOnOff(bool val)
    {
        gameObject.SetActive(val);
    }
}
