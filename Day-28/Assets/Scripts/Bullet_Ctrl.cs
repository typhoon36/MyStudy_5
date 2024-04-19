using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Ctrl : MonoBehaviour
{
   //## 이동관련 변수 추가
    Vector3 m_DirVec = Vector3.zero;
    //총알이 이동하려는 방향벡터
    float m_MoveSpeed = 35.0f;
    //총알의 이동속도
    Vector3 m_StartPos = new Vector3(0, 0, 1);
    //총알의 시작위치

    Vector3 m_MoveStep;
    //프레임당 총알의 이동 계산 변수

    float m_ShotRange = 30.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_MoveStep = m_DirVec * (m_MoveSpeed * Time.deltaTime);
        //프레임당 이동거리 계산
        m_MoveStep.y = 0;
        //y축 이동은 없음

        transform.Translate(m_MoveStep,Space.World);
        // 월드상 이동


        //## 화면밖으로 나가면 삭제로직
        Vector3 a_Pos = Camera.main.WorldToViewportPoint(transform.position);
        //화면상의 위치를 계산

        if(a_Pos.x < -0.1f || a_Pos.x > 1.1f || a_Pos.y < -0.1f || a_Pos.y > 1.1f)
            Destroy(gameObject);
        //화면밖으로 나가면 삭제

        else
        {
            float a_Length = Vector3.Distance(m_StartPos, transform.position);
            //float a_Lenght = (transform.position - m_StartPos).magnitude;
            if(m_ShotRange < a_Length) 
                //총알의 사정거리를 넘어가면 삭제(사거리 제한)
                Destroy(gameObject);
        }

    }

    public void BulletSpawn(Vector3 a_OwnPos , Vector3 a_Dirvec,
                                                       float a_ShotRange = 30.0f)
    {
        a_Dirvec.y = 0;
        //y축 이동은 없음
        m_DirVec = a_Dirvec;
        //총알의 이동방향 설정
        m_DirVec.Normalize();
        //이동방향을 정규화

        m_StartPos = a_OwnPos + m_DirVec * 2.5f;
        //총알의 시작위치 설정
        m_StartPos.y = transform.position.y;


        transform.position = new Vector3(m_StartPos.x, m_StartPos.y, m_StartPos.z);
        //총알의 위치를 시작위치로 설정


        //transform.rotation = Quaternion.LookRotation(m_DirVec);
        //총알의 방향을 이동방향으로 회전설정이기는하나 아래가 더 간단함

        transform.forward = m_DirVec;
        //총알의 방향을 이동방향으로 회전설정


        m_ShotRange = a_ShotRange;

    }
    
}
