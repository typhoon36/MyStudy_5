using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    float h = 0.0f;
    float v = 0.0f;

    float moveSpeed = 10.0f; //이동 속도

    Vector3 moveDIr = Vector3.zero; //이동 방향


    //카메라 회전 변수
    float rotSpeed = 350.0f; //회전 속도
    Vector3 m_CacVec = Vector3.zero; //카메라 회전 벡터
    //~ 카메라 회전 변수


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 카메라 회전 로직
        if(Input.GetMouseButton(1) == true) //오른쪽 버튼 누르고 있는 동안
        {
           m_CacVec = transform.eulerAngles;
            // 카메라의 xyz를 가져옴

            m_CacVec.y += Input.GetAxisRaw("Mouse X") * rotSpeed * Time.deltaTime;
            m_CacVec.x -= Input.GetAxisRaw("Mouse Y") * rotSpeed * Time.deltaTime;

            if(180.0f < m_CacVec.x && m_CacVec.x < 340.0f)
            
                m_CacVec.x = 340.0f;
           
           if(12.0f < m_CacVec.x && m_CacVec.x <= 180.0f)
            
                m_CacVec.x = 12.0f;
            
            

            transform.eulerAngles = m_CacVec;
        }



        //이동 구현
        h = Input.GetAxis("Horizontal"); //좌우 화살표 키
        v = Input.GetAxis("Vertical");   //상하 화살표 키


        //이동 방향 벡터 계산

        moveDIr = (Vector3.forward * v) + (Vector3.right * h);

        if (1.0f < moveDIr.magnitude)
            moveDIr.Normalize();

        //이동방향 * 속도 * Time.deltaTime* spaceself(로컬좌표)
        transform.Translate(moveDIr * moveSpeed * Time.deltaTime, Space.Self);


        //캐릭터의 높이값 찾기
        transform.position = new Vector3(transform.position.x,
           GameMgr.Inst.m_RefMap.SampleHeight(transform.position)+5.0f,
           transform.position.z);
        //~캐릭터의 높이값 찾기

        //~이동 구현
    }

    public bool IsMove()
    {
        if (h == 0 && v == 0)
            return false;
        else
            return true;
    }

}
