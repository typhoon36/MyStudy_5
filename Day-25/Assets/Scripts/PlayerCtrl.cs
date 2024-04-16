using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    float h = 0.0f;
    float v = 0.0f;

    float moveSpeed = 10.0f; //�̵� �ӵ�

    Vector3 moveDIr = Vector3.zero; //�̵� ����


    //ī�޶� ȸ�� ����
    float rotSpeed = 350.0f; //ȸ�� �ӵ�
    Vector3 m_CacVec = Vector3.zero; //ī�޶� ȸ�� ����
    //~ ī�޶� ȸ�� ����


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶� ȸ�� ����
        if(Input.GetMouseButton(1) == true) //������ ��ư ������ �ִ� ����
        {
           m_CacVec = transform.eulerAngles;
            // ī�޶��� xyz�� ������

            m_CacVec.y += Input.GetAxisRaw("Mouse X") * rotSpeed * Time.deltaTime;
            m_CacVec.x -= Input.GetAxisRaw("Mouse Y") * rotSpeed * Time.deltaTime;

            if(180.0f < m_CacVec.x && m_CacVec.x < 340.0f)
            
                m_CacVec.x = 340.0f;
           
           if(12.0f < m_CacVec.x && m_CacVec.x <= 180.0f)
            
                m_CacVec.x = 12.0f;
            
            

            transform.eulerAngles = m_CacVec;
        }



        //�̵� ����
        h = Input.GetAxis("Horizontal"); //�¿� ȭ��ǥ Ű
        v = Input.GetAxis("Vertical");   //���� ȭ��ǥ Ű


        //�̵� ���� ���� ���

        moveDIr = (Vector3.forward * v) + (Vector3.right * h);

        if (1.0f < moveDIr.magnitude)
            moveDIr.Normalize();

        //�̵����� * �ӵ� * Time.deltaTime* spaceself(������ǥ)
        transform.Translate(moveDIr * moveSpeed * Time.deltaTime, Space.Self);


        //ĳ������ ���̰� ã��
        transform.position = new Vector3(transform.position.x,
           GameMgr.Inst.m_RefMap.SampleHeight(transform.position)+5.0f,
           transform.position.z);
        //~ĳ������ ���̰� ã��

        //~�̵� ����
    }

    public bool IsMove()
    {
        if (h == 0 && v == 0)
            return false;
        else
            return true;
    }

}
