using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HeroCtrl : MonoBehaviour
{
    //## 키보드 이동 관련 변수 선언
    float h, v;                 //키보드 입력값을 받기 위한 변수
    float m_MoveSpeed = 10.0f;  //초당 10m 이동속도

    Vector3 m_DirVec;           //이동하려는 방향 벡터 변수
   

    //## 좌표 계산용 변수들...
    Vector3 m_CurPos;
    Vector3 m_CacEndVec;
    

    //##총알 발사 관련 변수 선언
    float m_AttSpeed   = 0.1f;  //공격속도(공속)
    float m_CacAtTick  = 0.0f;  //기관총 발사 주기 만들기..
    float m_ShootRange = 30.0f; //사거리
   

    //## 마우스 클릭 이동 관련 변수 
    [HideInInspector] 
    public bool m_bMoveOnOff = false; //현재 마우스 클릭 이동 중인지 여부
    Vector3 m_TargetPos;    //마우스 피킹 목표점
    float m_CacStep;        //걸음 계산용 변수

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

        //## 총알 발사 코드
        if (0.0f < m_CacAtTick)
            m_CacAtTick -= Time.deltaTime;
        //마우스 오른쪽 버튼 클릭시
        if (Input.GetMouseButton(1) == true) 
        {
            if(m_CacAtTick <= 0.0f)
            {
                Shoot_Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));    

                m_CacAtTick = m_AttSpeed;
            }
        }
       
    }

    //##키보드 이동처리
    void KeyBDUpdate()  
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if(h != 0.0f || v != 0.0f)  //이동 키보드를 조작하고 있으면...
        {
            m_DirVec = (Vector3.right * h) + (Vector3.forward * v);
            if (1.0f < m_DirVec.magnitude)
                m_DirVec.Normalize();

            transform.Translate(m_DirVec * m_MoveSpeed * Time.deltaTime);
        }
    }

    #region # 마우스 클릭 이동

    float m_Tick = 0.0f;

    void MousePickCtrl()
    {
        //##주기 발생
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

        //##마우스 클릭 이동
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
        //키보드 이동시 마우스 클릭 이동을 중지시킨다.

        if (m_bMoveOnOff == true)
        {
            m_CacStep = Time.deltaTime * m_MoveSpeed;
            //걸음수 계산

            Vector3 a_CacEndVec = m_TargetPos - transform.position;
            a_CacEndVec.y = 0.0f;
            //목표점까지의 벡터를 계산

            if (a_CacEndVec.magnitude <= m_CacStep)
            {   
                //목표점까지의 거리보다 보폭이 크거나 같으면 도착판정
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
    //마우스 클릭 위치를 받아서 총알을 발사합니다.
    {  

        GameObject a_Obj = Instantiate(Game_Mgr.m_BulletPrefab);
        //오브젝트의 클론(복사체) 생성 

        m_CacEndVec = a_Pos - transform.position;
        m_CacEndVec.y = 0.0f;

        BulletCtrl a_BulletSc = a_Obj.GetComponent<BulletCtrl>();
        a_BulletSc.BulletSpawn(transform.position, m_CacEndVec.normalized, m_ShootRange);
    }
}
