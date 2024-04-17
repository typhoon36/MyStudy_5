using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy_Ctrl : MonoBehaviour
{
    Transform playerTr;
    [HideInInspector] public float m_MoveVelocity = 13.0f; //초당 이동 속도

    // Start is called before the first frame update
    void Start()
    {
        //playerTr = GameObject.Find("Main Camera").GetComponent<Transform>();
        playerTr = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //--- 추적 이동 구현
        Vector3 a_MoveDir = playerTr.position - this.transform.position;
        a_MoveDir.y = 0.0f;

        transform.forward = a_MoveDir.normalized;
        Vector3 a_StepVec = transform.forward * m_MoveVelocity * Time.deltaTime;
        transform.Translate(a_StepVec, Space.World);

        //Vector3 a_StepVec = Vector3.forward * m_MoveVelocity * Time.deltaTime;
        //transform.Translate(a_StepVec); //Space.Self <-- 기본값

        float a_CacPosY = GameMgr.Inst.m_RefMap.SampleHeight(transform.position);
        transform.position = new Vector3(transform.position.x, 
                                        a_CacPosY,
                                        transform.position.z);

        if (a_MoveDir.magnitude < 5.0f) //주인공과 부딪힌 상황
        {
            GameMgr.Inst.DecreaseHp();  //주인공 하트 감소 시키기...
            Destroy(gameObject);  //주인공 하트 감소 시키기...
        }
        //--- 추적 이동 구현
    }//void Update()
}
