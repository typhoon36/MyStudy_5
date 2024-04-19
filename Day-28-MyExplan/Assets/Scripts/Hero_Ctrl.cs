using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Ctrl : MonoBehaviour
{
   //## 키보드 이동 
    float h, v;
    float m_MoveSpeed = 10.0f;
    //초당 10미터 이동속도에 대한 변수

    Vector3 m_DirVec;
    //이동하려는 방향벡터의 변수


    //## 좌표계산변수
    Vector3 m_CurPos;
    Vector3 m_CurEndVec;


    //## 총알발사관련 변수
    float m_AttSpeed = 0.1f;
    //총알발사속도(초당 0.1발)
    float m_CacAtTick = 0.0f;
    //총알발사시간계산
    float m_ShotRange = 30.0f;
    //총알의 사정거리


    // Start is called before the first frame update
    void Start()
    {
     
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyBDUpdate();

     
            Vector3 a_Pos = Camera.main.WorldToViewportPoint(transform.position);
        //화면상의 위치를 계산

        if (a_Pos.x < 0.0f || a_Pos.x > 1.0f || a_Pos.y < 0.0f || a_Pos.y > 1.0f)
        {
            a_Pos.x = Mathf.Clamp(a_Pos.x, 0.0f, 1.0f);
            a_Pos.y = Mathf.Clamp(a_Pos.y, 0.0f, 1.0f);

            // 뷰포트 좌표를 월드 좌표로 변환하여 플레이어의 위치를 업데이트합니다.
            transform.position = Camera.main.ViewportToWorldPoint(a_Pos);
        }
          




        //## 총알 발사 로직

        if (m_CacAtTick > 0.0f)
            m_CacAtTick -= Time.deltaTime;
        //총알발사시간을 계산


        if(Input.GetMouseButton(1) == true) //마우스 오른쪽 버튼 클릭시
        {
            if(m_CacAtTick <= 0.0f) //총알발사시간이 0이하일때
            {
                Shoot_Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                //총알발사함수 호출(월드 좌표로 바꿔서)

                m_CacAtTick = m_AttSpeed;
                //총알발사시간을 설정
            }
        }

    }
   
    //## 키보드 이동 함수
    void KeyBDUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if(h != 0 || v != 0) 
            //키보드 입력값을 받아서 방향벡터를 설정
        {
            m_DirVec = new Vector3(h, 0, v);
           
            if(1.0f < m_DirVec.magnitude) 
                //대각선 이동속도를 1로 제한
            
                m_DirVec.Normalize();
            
            transform.Translate(m_DirVec * m_MoveSpeed * Time.deltaTime); 
            //이동
        }
    }


    //## 클릭이벤트 발생시 호출
    public void Shoot_Fire(Vector3 a_Pos)
    {
       
        GameObject a_Obj = Instantiate(Game_Mgr.m_BulletPrefab) as GameObject;
        //총알 생성(clone)

        m_CurEndVec = a_Pos - transform.position;
        //주인공과 목표점의 차이를 구함
        m_CurEndVec.y = 0;
        //y축 이동은 없음

        Bullet_Ctrl a_BulletSC = a_Obj.GetComponent<Bullet_Ctrl>();
        //총알의 스크립트를 가져옴
        a_BulletSC.BulletSpawn(transform.position, m_CurEndVec.normalized,m_ShotRange);
        //총알의 생성위치와 이동방향을 설정

    }
}
