using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Ctrl : MonoBehaviour
{
   //## �̵����� ���� �߰�
    Vector3 m_DirVec = Vector3.zero;
    //�Ѿ��� �̵��Ϸ��� ���⺤��
    float m_MoveSpeed = 35.0f;
    //�Ѿ��� �̵��ӵ�
    Vector3 m_StartPos = new Vector3(0, 0, 1);
    //�Ѿ��� ������ġ

    Vector3 m_MoveStep;
    //�����Ӵ� �Ѿ��� �̵� ��� ����

    float m_ShotRange = 30.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_MoveStep = m_DirVec * (m_MoveSpeed * Time.deltaTime);
        //�����Ӵ� �̵��Ÿ� ���
        m_MoveStep.y = 0;
        //y�� �̵��� ����

        transform.Translate(m_MoveStep,Space.World);
        // ����� �̵�


        //## ȭ������� ������ ��������
        Vector3 a_Pos = Camera.main.WorldToViewportPoint(transform.position);
        //ȭ����� ��ġ�� ���

        if(a_Pos.x < -0.1f || a_Pos.x > 1.1f || a_Pos.y < -0.1f || a_Pos.y > 1.1f)
            Destroy(gameObject);
        //ȭ������� ������ ����

        else
        {
            float a_Length = Vector3.Distance(m_StartPos, transform.position);
            //float a_Lenght = (transform.position - m_StartPos).magnitude;
            if(m_ShotRange < a_Length) 
                //�Ѿ��� �����Ÿ��� �Ѿ�� ����(��Ÿ� ����)
                Destroy(gameObject);
        }

    }

    public void BulletSpawn(Vector3 a_OwnPos , Vector3 a_Dirvec,
                                                       float a_ShotRange = 30.0f)
    {
        a_Dirvec.y = 0;
        //y�� �̵��� ����
        m_DirVec = a_Dirvec;
        //�Ѿ��� �̵����� ����
        m_DirVec.Normalize();
        //�̵������� ����ȭ

        m_StartPos = a_OwnPos + m_DirVec * 2.5f;
        //�Ѿ��� ������ġ ����
        m_StartPos.y = transform.position.y;


        transform.position = new Vector3(m_StartPos.x, m_StartPos.y, m_StartPos.z);
        //�Ѿ��� ��ġ�� ������ġ�� ����


        //transform.rotation = Quaternion.LookRotation(m_DirVec);
        //�Ѿ��� ������ �̵��������� ȸ�������̱���ϳ� �Ʒ��� �� ������

        transform.forward = m_DirVec;
        //�Ѿ��� ������ �̵��������� ȸ������


        m_ShotRange = a_ShotRange;

    }
    
}
