using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public Text Time_Text;
    public Text Score_Text;
    public GameObject GameOverPanel;
    public Button RestartButton;
    private float timeLeft = 800.0f;
    public int Score = 0;
    public Image[] Life; // 라이프 이미지 배열

    // Start is called before the first frame update
    void Start()
    {
        GameOverPanel.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        // "다시 시작" 버튼의 클릭 이벤트에 RestartGame 메소드를 연결합니다.
        RestartButton.onClick.AddListener(RestartGame);

        StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        Time_Text.text = ("Time : " + timeLeft.ToString("N2"));
        Score_Text.text = ("Score : " + Score.ToString()); // 점수를 화면에 표시합니다.
        if (timeLeft < 0)
        {
            StopCoroutine("LoseTime");
            Time_Text.text = "Times Up!";
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
            RestartButton.gameObject.SetActive(true);
            
        }
    }

    public void RestartGame()
    {
        // 게임 오버 패널을 비활성화합니다.
        GameOverPanel.SetActive(false);

        // "다시 시작" 버튼을 비활성화합니다.
        RestartButton.gameObject.SetActive(false);

        // 게임 시간을 다시 시작합니다.
        Time.timeScale = 1;

        // 시간을 초기화합니다.
        timeLeft = 800.0f;

        // 점수를 0으로 초기화합니다.
        Score = 0;

        // 생명 이미지의 fillAmount를 모두 1로 설정합니다.
        foreach (var life in Life)
        {
            life.fillAmount = 1;
        }

        // 현재 씬을 다시 로드하여 게임을 다시 시작합니다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }





    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    // 점수를 증가시키는 메소드를 추가합니다.
    public void IncreaseScore(int amount)
    {
        Score += amount;
        Score_Text.text = ("Score : " + Score.ToString());
    }
}
