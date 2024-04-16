using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy_Ctrl : MonoBehaviour
{
    Transform playerTr;
    [HideInInspector] public float m_MoveVelocity = 13.0f;//초당 이동속도

    

    // Start is called before the first frame update
    void Start()
    {
        //메인카메라 가져오기
        playerTr = GameObject.Find("Main Camera").GetComponent<Transform>();
        //playerTr = Camera.main.transform; //위치와 회전값을 가져옴



    }

    // Update is called once per frame
    void Update()
    {
        //추적 이동 로직

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
        //추적 이동 로직 끝

    }
}
