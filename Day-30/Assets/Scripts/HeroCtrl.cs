using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HeroCtrl : MonoBehaviour
{
    //## Ű���� �̵� ���� ���� ����
    float h, v;                 //Ű���� �Է°��� �ޱ� ���� ����
    float m_MoveSpeed = 10.0f;  //�ʴ� 10m �̵��ӵ�

    Vector3 m_DirVec;           //�̵��Ϸ��� ���� ���� ����
   

    //## ��ǥ ���� ������...
    Vector3 m_CurPos;
    Vector3 m_CacEndVec;
    

    //##�Ѿ� �߻� ���� ���� ����
    float m_AttSpeed   = 0.1f;  //���ݼӵ�(����)
    float m_CacAtTick  = 0.0f;  //����� �߻� �ֱ� �����..
    float m_ShootRange = 30.0f; //��Ÿ�
   

    //## ���콺 Ŭ�� �̵� ���� ���� 
    [HideInInspector] 
    public bool m_bMoveOnOff = false; //���� ���콺 Ŭ�� �̵� ������ ����
    Vector3 m_TargetPos;    //���콺 ��ŷ ��ǥ��
    float m_CacStep;        //���� ���� ����

    Vector3 m_PickVec = Vector3.zero;
    public ClickMark m_ClickMark = null;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MousePickCtrl();

        KeyBDUpdate();
        MousePickUpdate();

        //## �Ѿ� �߻� �ڵ�
        if (0.0f < m_CacAtTick)
            m_CacAtTick -= Time.deltaTime;
        //���콺 ������ ��ư Ŭ����
        if (Input.GetMouseButton(1) == true) 
        {
            if(m_CacAtTick <= 0.0f)
            {
                Shoot_Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));    

                m_CacAtTick = m_AttSpeed;
            }
        }
       
    }

    //##Ű���� �̵�ó��
    void KeyBDUpdate()  
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if(h != 0.0f || v != 0.0f)  //�̵� Ű���带 �����ϰ� ������...
        {
            m_DirVec = (Vector3.right * h) + (Vector3.forward * v);
            if (1.0f < m_DirVec.magnitude)
                m_DirVec.Normalize();

            transform.Translate(m_DirVec * m_MoveSpeed * Time.deltaTime);
        }
    }

    #region # ���콺 Ŭ�� �̵�

    float m_Tick = 0.0f;

    void MousePickCtrl()
    {
        //##�ֱ� �߻�
        //if (0.0f < m_Tick)
        //    m_Tick -= Time.deltaTime;

        //if (m_Tick <= 0.0f)
        //{
        //    if (Input.GetMouseButton(0) == true) 
        //    {
        //        m_PickVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        SetMsPicking(m_PickVec);
        //        m_Tick = 0.1f;
        //    }
        //}

        //##���콺 Ŭ�� �̵�
        if (Input.GetMouseButton(0) == true) 
        {
            m_PickVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SetMsPicking(m_PickVec);

            if (m_ClickMark != null)
                m_ClickMark.PlayEff(m_PickVec, this);
        }

    }


    void SetMsPicking(Vector3 a_Pos)
    {
        Vector3 a_CacVec = a_Pos - this.transform.position;
        a_CacVec.y = 0.0f;
        if (a_CacVec.magnitude < 1.0f)
            return;

        m_bMoveOnOff = true;

        m_DirVec = a_CacVec;
        m_DirVec.Normalize();
        m_TargetPos = new Vector3(a_Pos.x, transform.position.y, a_Pos.z);
    }

    void MousePickUpdate()
    {
        if (h != 0.0f || v != 0.0f) 
            m_bMoveOnOff = false;
        //Ű���� �̵��� ���콺 Ŭ�� �̵��� ������Ų��.

        if (m_bMoveOnOff == true)
        {
            m_CacStep = Time.deltaTime * m_MoveSpeed;
            //������ ���

            Vector3 a_CacEndVec = m_TargetPos - transform.position;
            a_CacEndVec.y = 0.0f;
            //��ǥ�������� ���͸� ���

            if (a_CacEndVec.magnitude <= m_CacStep)
            {   
                //��ǥ�������� �Ÿ����� ������ ũ�ų� ������ ��������
                //transform.position = m_TargetPos;
                m_bMoveOnOff = false;
            }
            else
            {
                m_DirVec = a_CacEndVec;
                m_DirVec.Normalize();
                transform.Translate(m_DirVec * m_CacStep, Space.World);
            }
        }
    }

#endregion


    public void Shoot_Fire(Vector3 a_Pos)
    //���콺 Ŭ�� ��ġ�� �޾Ƽ� �Ѿ��� �߻��մϴ�.
    {  

        GameObject a_Obj = Instantiate(Game_Mgr.m_BulletPrefab);
        //������Ʈ�� Ŭ��(����ü) ���� 

        m_CacEndVec = a_Pos - transform.position;
        m_CacEndVec.y = 0.0f;

        BulletCtrl a_BulletSc = a_Obj.GetComponent<BulletCtrl>();
        a_BulletSc.BulletSpawn(transform.position, m_CacEndVec.normalized, m_ShootRange);
    }
}
