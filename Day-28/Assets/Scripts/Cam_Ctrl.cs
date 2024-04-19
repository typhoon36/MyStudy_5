using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cam_Ctrl : MonoBehaviour
{
    //## 카메라 줌인 줌아웃 변수
    float maxDist = 20.0f;
    //최대 줌인 거리
    float minDist = 5.0f;
    //최소 줌아웃 거리
    float zoomSpeed = 1.0f;
    //줌인,줌아웃 속도
    float distance = 15.0f;
    //현재 거리
    Camera RefCam = null;
    //카메라 참조 변수
    public Cinemachine.CinemachineVirtualCamera m_vcam;

    // Start is called before the first frame update
    void Start()
    {
        RefCam = GetComponent<Camera>();
        //if(RefCam != null)
        //    distance = RefCam.orthographicSize;
        //카메라의 오쏘그래픽 사이즈를 거리로 설정(씨네머신을 사용하지않음)



        if (m_vcam != null)
            distance = m_vcam.m_Lens.OrthographicSize;
        //씨네머신을 사용할때는 렌즈의 오쏘그래픽 사이즈를 거리로 설정
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //## pc에서만 작동되는 기능
        if (Input.GetAxis("Mouse ScrollWheel") <0 && distance < maxDist)
            //마우스 휠을 아래로 내렸을때 -0.1f(size up) 
          
        {
            distance += zoomSpeed;
            //줌인
          

            //RefCam.orthographicSize = distance;
            //카메라의 오쏘그래픽 사이즈를 거리로 설정(씨네머신을 사용하지않음)
            m_vcam.m_Lens.OrthographicSize = distance;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && distance > minDist)
        {
            //마우스 휠을 위로 올렸을때 0.1f(size down)
            distance -= zoomSpeed;
            //줌아웃
          

            //RefCam.orthographicSize = distance;
            //카메라의 오쏘그래픽 사이즈를 거리로 설정(씨네머신을 사용하지않음)
            m_vcam.m_Lens.OrthographicSize = distance;
        }
    }
}
