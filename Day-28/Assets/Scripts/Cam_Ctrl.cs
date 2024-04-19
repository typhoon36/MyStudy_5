using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cam_Ctrl : MonoBehaviour
{
    //## ī�޶� ���� �ܾƿ� ����
    float maxDist = 20.0f;
    //�ִ� ���� �Ÿ�
    float minDist = 5.0f;
    //�ּ� �ܾƿ� �Ÿ�
    float zoomSpeed = 1.0f;
    //����,�ܾƿ� �ӵ�
    float distance = 15.0f;
    //���� �Ÿ�
    Camera RefCam = null;
    //ī�޶� ���� ����
    public Cinemachine.CinemachineVirtualCamera m_vcam;

    // Start is called before the first frame update
    void Start()
    {
        RefCam = GetComponent<Camera>();
        //if(RefCam != null)
        //    distance = RefCam.orthographicSize;
        //ī�޶��� ����׷��� ����� �Ÿ��� ����(���׸ӽ��� �����������)



        if (m_vcam != null)
            distance = m_vcam.m_Lens.OrthographicSize;
        //���׸ӽ��� ����Ҷ��� ������ ����׷��� ����� �Ÿ��� ����
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //## pc������ �۵��Ǵ� ���
        if (Input.GetAxis("Mouse ScrollWheel") <0 && distance < maxDist)
            //���콺 ���� �Ʒ��� �������� -0.1f(size up) 
          
        {
            distance += zoomSpeed;
            //����
          

            //RefCam.orthographicSize = distance;
            //ī�޶��� ����׷��� ����� �Ÿ��� ����(���׸ӽ��� �����������)
            m_vcam.m_Lens.OrthographicSize = distance;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && distance > minDist)
        {
            //���콺 ���� ���� �÷����� 0.1f(size down)
            distance -= zoomSpeed;
            //�ܾƿ�
          

            //RefCam.orthographicSize = distance;
            //ī�޶��� ����׷��� ����� �Ÿ��� ����(���׸ӽ��� �����������)
            m_vcam.m_Lens.OrthographicSize = distance;
        }
    }
}
