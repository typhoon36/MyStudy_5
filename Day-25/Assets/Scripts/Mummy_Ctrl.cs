using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy_Ctrl : MonoBehaviour
{
    Transform playerTr;
    [HideInInspector] public float m_MoveVelocity = 13.0f;//�ʴ� �̵��ӵ�

    

    // Start is called before the first frame update
    void Start()
    {
        //����ī�޶� ��������
        playerTr = GameObject.Find("Main Camera").GetComponent<Transform>();
        //playerTr = Camera.main.transform; //��ġ�� ȸ������ ������



    }

    // Update is called once per frame
    void Update()
    {
        //���� �̵� ����

        Vector3 a_MoveDir = playerTr.position - this.transform.position;
        a_MoveDir.y= 0.0f;
        transform.forward = a_MoveDir.normalized;
        Vector3 a_StepVec = transform.forward * m_MoveVelocity * Time.deltaTime;
        transform.Translate(a_StepVec, Space.World);


        float a_CacPosY = GameMgr.Inst.m_RefMap.SampleHeight(transform.position);
        transform.position = new Vector3(transform.position.x, a_CacPosY, transform.position.z);

        if (a_MoveDir.magnitude < 5.0f)
        {
            GameMgr.Inst.DecreaseHp();
            Destroy(gameObject);
        }
        //���� �̵� ���� ��

    }
}
