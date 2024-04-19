using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Ctrl : MonoBehaviour
{
   //## Ű���� �̵� 
    float h, v;
    float m_MoveSpeed = 10.0f;
    //�ʴ� 10���� �̵��ӵ��� ���� ����

    Vector3 m_DirVec;
    //�̵��Ϸ��� ���⺤���� ����


    //## ��ǥ��꺯��
    Vector3 m_CurPos;
    Vector3 m_CurEndVec;


    //## �Ѿ˹߻���� ����
    float m_AttSpeed = 0.1f;
    //�Ѿ˹߻�ӵ�(�ʴ� 0.1��)
    float m_CacAtTick = 0.0f;
    //�Ѿ˹߻�ð����
    float m_ShotRange = 30.0f;
    //�Ѿ��� �����Ÿ�


    // Start is called before the first frame update
    void Start()
    {
     
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyBDUpdate();


        //## �Ѿ� �߻� ����

        if(m_CacAtTick > 0.0f)
            m_CacAtTick -= Time.deltaTime;
        //�Ѿ˹߻�ð��� ���


        if(Input.GetMouseButton(1) == true) //���콺 ������ ��ư Ŭ����
        {
            if(m_CacAtTick <= 0.0f) //�Ѿ˹߻�ð��� 0�����϶�
            {
                Shoot_Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                //�Ѿ˹߻��Լ� ȣ��(���� ��ǥ�� �ٲ㼭)

                m_CacAtTick = m_AttSpeed;
                //�Ѿ˹߻�ð��� ����
            }
        }

    }
   
    //## Ű���� �̵� �Լ�
    void KeyBDUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if(h != 0 || v != 0) 
            //Ű���� �Է°��� �޾Ƽ� ���⺤�͸� ����
        {
            m_DirVec = new Vector3(h, 0, v);
           
            if(1.0f < m_DirVec.magnitude) 
                //�밢�� �̵��ӵ��� 1�� ����
            
                m_DirVec.Normalize();
            
            transform.Translate(m_DirVec * m_MoveSpeed * Time.deltaTime); 
            //�̵�
        }
    }


    //## Ŭ���̺�Ʈ �߻��� ȣ��
    public void Shoot_Fire(Vector3 a_Pos)
    {
       
        GameObject a_Obj = Instantiate(Game_Mgr.m_BulletPrefab) as GameObject;
        //�Ѿ� ����(clone)

        m_CurEndVec = a_Pos - transform.position;
        //���ΰ��� ��ǥ���� ���̸� ����
        m_CurEndVec.y = 0;
        //y�� �̵��� ����

        Bullet_Ctrl a_BulletSC = a_Obj.GetComponent<Bullet_Ctrl>();
        //�Ѿ��� ��ũ��Ʈ�� ������
        a_BulletSC.BulletSpawn(transform.position, m_CurEndVec.normalized,m_ShotRange);
        //�Ѿ��� ������ġ�� �̵������� ����

    }
}
