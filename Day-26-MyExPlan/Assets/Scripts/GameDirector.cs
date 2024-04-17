using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    GameObject pointText;
    float time = 30.0f;
    int point = 0;
    GameObject generator;

    //게임오버 관련
    public GameObject GameOverPanel;
    public Text GameOver_Text;
    public Text Score_Text;
    public Button ReplayBtn;
    //~게임오버 관련



    // Start is called before the first frame update
    void Start()
    {
        this.timerText = GameObject.Find("Timer");
        this.pointText = GameObject.Find("Point");
        this.generator = GameObject.Find("ItemGenerator");

        //리스타트버튼
        ReplayBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
            Time.timeScale = 1.0f;
        });


    }

    // Update is called once per frame
    void Update()
    {
        this.time -= Time.deltaTime;

        if (this.time < 0)
        {
            this.time = 0;
            this.generator.GetComponent<ItemGenerator>().SetParameter(10000.0f, 0, 0);
            Time.timeScale = 0.0f;
            GameOverPanel.SetActive(true);
            Score_Text.text = "Score : " + point.ToString();
        }
        else if (0 <= this.time && this.time < 5)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.9f, -0.04f, 3);
        }
        else if (5 <= this.time && this.time < 10)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.4f, -0.06f, 6);
        }
        else if (10 <= this.time && this.time < 20)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.7f, -0.04f, 4);
        }
        else if (20 <= this.time && this.time < 30)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f, -0.03f, 2);
        }

        this.timerText.GetComponent<Text>().text = this.time.ToString("F1");
        this.pointText.GetComponent<Text>().text = this.point.ToString() + " point";
        this.Score_Text.text = "Score : " + this.point.ToString();
      
    }

    public void GetApple()
    {
        this.point += 100;
    }

    public void GetBomb()
    {
        this.point /= 2;
    }
}
