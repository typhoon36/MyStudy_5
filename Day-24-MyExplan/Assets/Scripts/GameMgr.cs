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
    public Image[] Life; // ������ �̹��� �迭

    // Start is called before the first frame update
    void Start()
    {
        GameOverPanel.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        // "�ٽ� ����" ��ư�� Ŭ�� �̺�Ʈ�� RestartGame �޼ҵ带 �����մϴ�.
        RestartButton.onClick.AddListener(RestartGame);

        StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        Time_Text.text = ("Time : " + timeLeft.ToString("N2"));
        Score_Text.text = ("Score : " + Score.ToString()); // ������ ȭ�鿡 ǥ���մϴ�.
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
        // ���� ���� �г��� ��Ȱ��ȭ�մϴ�.
        GameOverPanel.SetActive(false);

        // "�ٽ� ����" ��ư�� ��Ȱ��ȭ�մϴ�.
        RestartButton.gameObject.SetActive(false);

        // ���� �ð��� �ٽ� �����մϴ�.
        Time.timeScale = 1;

        // �ð��� �ʱ�ȭ�մϴ�.
        timeLeft = 800.0f;

        // ������ 0���� �ʱ�ȭ�մϴ�.
        Score = 0;

        // ���� �̹����� fillAmount�� ��� 1�� �����մϴ�.
        foreach (var life in Life)
        {
            life.fillAmount = 1;
        }

        // ���� ���� �ٽ� �ε��Ͽ� ������ �ٽ� �����մϴ�.
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

    // ������ ������Ű�� �޼ҵ带 �߰��մϴ�.
    public void IncreaseScore(int amount)
    {
        Score += amount;
        Score_Text.text = ("Score : " + Score.ToString());
    }
}
