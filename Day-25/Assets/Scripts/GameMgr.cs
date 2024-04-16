using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    //�� �̰� ã��
    public Terrain m_RefMap = null;


    //UI ���� ����

    int hp = 3;
    public Image[] hpImage; //UI �̹��� �迭

    //~ UI ���� ����


    //���� ���� ����

    public GameObject Mummy_Root; //���� ��Ʈ ������Ʈ ����

    float Span = 1.0f; //���� ���� �ֱ�
    float delta = 0.0f; //���� �ð�


    float m_MvSpeedCtrl = 13.0f; //���� �̵��ӵ� ����

    //~ ���� ���� ����




    PlayerCtrl PlayerCtrl = null;//���ΰ� ���� -- ȥ�ڰ� �ƴϱ⶧���� �̱������� ����������



    //���Ӿ� �ȿ����� ���Ǵ� �̱��� ���� ����

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

        //���ΰ��� ������������ ����
        if (PlayerCtrl == null)
            return;
        if(PlayerCtrl.IsMove() == true)
        {
            this.delta = 0.0f;
            return;
        }

        //���̵� ����
        m_MvSpeedCtrl += Time.deltaTime * 0.5f;
        if (35 < m_MvSpeedCtrl)
            m_MvSpeedCtrl = 35.0f;
        
        Span -= Time.deltaTime * 0.03f; //�����ֱ� ����
        if(Span < 0.2f)
            Span = 0.2f;
        //~���̵� ����

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
            //���ӿ���
            SceneManager.LoadScene("GameOverScene");

        }


    }

}
