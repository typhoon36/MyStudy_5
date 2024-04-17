using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//���̵�� : Ÿ�� ���潺 ó�� ���� ��Ƽ�� �� ������ ��

public class GameMgr : MonoBehaviour
{
    //--- ���̰� ã�� ���� ����
    public Terrain m_RefMap = null;
    //--- ���̰� ã�� ���� ����

    //--- UI ���� ����
    int hp = 3;
    public Image[] hpImage;

    float m_Timer = 60.0f;
    public static int Score = 0;
    public Text timerText;
    public Text scoreText;
    //--- UI ���� ����

    //--- ���̶� ���� ���� ����
    public GameObject Mummy_Root;   //���̶� ������ ���� ����
    float span = 1.0f;      //���̶� ���� �ֱ�
    float delta = 0.0f;     //���̶� ���� �ֱ� ���� ����

    float m_MvSpeedCtrl = 13.0f;  //��ü ���̶� �̵� �ӵ��� �����ϱ� ���� ����
    //--- ���̶� ���� ���� ����  

    PlayerController PlayerCtrl = null; //���ΰ� ����

    public GameObject[] AnimalArr;
    public Transform  AnimalGroup;

    //--- GameScene �ȿ����� ���Ǵ� �̱��� ����
    public static GameMgr Inst = null;

    void Awake()
    {
        Inst = this;    
    }
    //--- GameScene �ȿ����� ���Ǵ� �̱��� ����

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;          // static ���� �ʱ�ȭ
        Time.timeScale = 1.0f;  //���� �⺻ �ӵ��� ��������...

        PlayerCtrl = FindObjectOfType<PlayerController>();

        AnimalRandGen();
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer -= Time.deltaTime;
        timerText.text = m_Timer.ToString("N1");
        
        if(m_Timer <= 0)
        {
            Time.timeScale = 0; //�Ͻ����� ȿ��
            SceneManager.LoadScene("GameOver");
        }

        MummyGenerator();
    }

    void MummyGenerator() //ĳ���Ͱ� ������ �� �� �տ� �����ϰ� ���͸� ������ �ִ� �Լ�
    {
        //--- ���ΰ��� ���� ���� ���� ���̶� ���Ͱ� �����ǰ� �ϱ� ���� �ڵ�
        if (PlayerCtrl == null)
            return;

        if(PlayerCtrl.IsMove() == true)
        {
            this.delta = 0.0f;
            return;
        }
        //--- ���ΰ��� ���� ���� ���� ���̶� ���Ͱ� �����ǰ� �ϱ� ���� �ڵ�

        //--- ���̵� ����
        m_MvSpeedCtrl += (Time.deltaTime * 0.5f);  //�̵� �ӵ� �� �� �������� �ϱ�...
        if (35.0f < m_MvSpeedCtrl)
            m_MvSpeedCtrl = 35.0f;

        span -= (Time.deltaTime * 0.03f);   //���� �ֱ� �� �� ª������ �ϱ�...
        if (span < 0.2f)
            span = 0.2f;
        //--- ���̵� ����

        this.delta += Time.deltaTime;
        if(span < delta)
        {
            delta = 0.0f;

            Vector3 CamForW = Camera.main.transform.forward;
            CamForW.y = 0.0f;
            CamForW.Normalize();
            CamForW = CamForW * Random.Range(50.0f, 52.0f);

            Vector3 CacX = Camera.main.transform.right;
            CacX.y = 0.0f;
            CacX.Normalize();
            CacX = CacX * Random.Range(-36.0f, 36.0f);

            Vector3 SpPos = Camera.main.transform.position + CamForW + CacX;
            SpPos.y = 0.0f;
            GameObject go = Instantiate(Mummy_Root);
            go.transform.position = SpPos;
            go.GetComponent<Mummy_Ctrl>().m_MoveVelocity = m_MvSpeedCtrl;

        }//if(span < delta)
    }//void MummyGenerator() 

    public void DecreaseHp()
    {
        hp--;
        if (hp < 0)
            hp = 0;

        for(int i = 0; i < hpImage.Length; i++)
        {
            if(i < hp)
                hpImage[i].gameObject.SetActive(true);
            else
                hpImage[i].gameObject.SetActive(false);
        }//for(int i = 0; i < hpImage.Length; i++)

        if(hp <= 0)
        {
            //GameOver ó��
            SceneManager.LoadScene("GameOver");
        }

    }// public void DecreaseHp()

    public void AddScore(int Value = 10)
    {
        Score += Value;

        if (scoreText != null)
            scoreText.text = "Score : " + Score.ToString();
    }

    void AnimalRandGen()
    {
        for(int i = 0; i < 200; i++)
        {
            Vector3 RandomXYZ = new Vector3(
                                    Random.Range(-250.0f, 250.0f),
                                    10.0f,
                                    Random.Range(-250.0f, 250.0f));
            RandomXYZ.y = m_RefMap.SampleHeight(RandomXYZ) + Random.Range(0.0f, 15.0f);

            int Kind = Random.Range(0, AnimalArr.Length);
            GameObject go = Instantiate(AnimalArr[Kind]);
            go.transform.SetParent(AnimalGroup);
            go.transform.position = RandomXYZ;
            go.transform.eulerAngles = new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
        }
    }//void AnimalRandGen()
}
