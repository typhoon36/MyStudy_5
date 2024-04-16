using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    //높 이값 찾기
    public Terrain m_RefMap = null;


    //UI 관련 변수

    int hp = 3;
    public Image[] hpImage; //UI 이미지 배열

    //~ UI 관련 변수


    //몬스터 관련 변수

    public GameObject Mummy_Root; //몬스터 루트 오브젝트 연결

    float Span = 1.0f; //몬스터 생성 주기
    float delta = 0.0f; //누적 시간


    float m_MvSpeedCtrl = 13.0f; //몬스터 이동속도 제어

    //~ 몬스터 관련 변수




    PlayerCtrl PlayerCtrl = null;//주인공 변수 -- 혼자가 아니기때문에 싱글톤으로 만들지못함



    //게임씬 안에서만 사용되는 싱글톤 패턴 구현

    public static GameMgr Inst = null;


    void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerCtrl = FindObjectOfType<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        MummyGenerator();

    }

    void MummyGenerator()
    {

        //주인공이 멈춰잇을때만 스폰
        if (PlayerCtrl == null)
            return;
        if(PlayerCtrl.IsMove() == true)
        {
            this.delta = 0.0f;
            return;
        }

        //난이도 설정
        m_MvSpeedCtrl += Time.deltaTime * 0.5f;
        if (35 < m_MvSpeedCtrl)
            m_MvSpeedCtrl = 35.0f;
        
        Span -= Time.deltaTime * 0.03f; //스폰주기 감소
        if(Span < 0.2f)
            Span = 0.2f;
        //~난이도 설정

        this.delta += Time.deltaTime;
        if (Span < delta)
        {
            delta = 0.0f;

            Vector3 CamForW = Camera.main.transform.forward;

            CamForW.y = 0.0f;

            CamForW.Normalize();

            Vector3 a_Norm = CamForW.normalized;

            CamForW = CamForW *(float)Random.Range(50.0f, 52.0f);

            Vector3 CacX = Camera.main.transform.right;
            CacX.y = 0.0f;
            CacX.Normalize();
            CacX = CacX * (float)Random.Range(-36.0f, 36.0f);

            Vector3 SpPos = Camera.main.transform.position + CamForW + CacX;

            GameObject go = Instantiate(Mummy_Root);
            go.transform.position = SpPos;
            go.GetComponent<Mummy_Ctrl>().m_MoveVelocity = m_MvSpeedCtrl;

        }
    }

    public void DecreaseHp()
    {
        hp--;
        if (hp < 0)
        {
            hp = 0;
        }

        for (int i = 0; i<3; i++)
        {
            if (i < hp)
            {
                hpImage[i].gameObject.SetActive(true);
            }
            else
            {
                hpImage[i].gameObject.SetActive(false);
            }
        }

        if (hp <= 0)
        {
            //게임오버
            SceneManager.LoadScene("GameOverScene");

        }


    }

}
