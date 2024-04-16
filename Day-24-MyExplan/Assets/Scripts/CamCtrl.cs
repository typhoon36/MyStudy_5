using System.Security.Cryptography;
using UnityEngine;

public class CamCtrl : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public GameMgr gameMgr; // GameMgr ��ũ��Ʈ ����

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // ī�޶��� �̵��� ���� �������� �߻��ϵ��� �մϴ�.
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // ī�޶��� y ��ǥ�� 5�� �����մϴ�.
        transform.position = new Vector3(transform.position.x, 5, transform.position.z);

        // ī�޶��� z ��ǥ�� 183 �̻��� �� z ��ǥ�� 183���� �����մϴ�.
        if (transform.position.z > 183)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 183);
        }

        if (Input.GetMouseButton(1)) // ���콺 ��Ŭ���� ���� �������� üũ
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            transform.Rotate(0, rotationX, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MummyCtrl>() != null) // �̶�� �浹�ߴ��� üũ
        {
            for (int i = 0; i < gameMgr.Life.Length; i++) // gameMgr.Life �迭 ����
            {
                if (gameMgr.Life[i].fillAmount > 0) // gameMgr.Life �迭 ����
                {
                    gameMgr.Life[i].fillAmount -= 0.5f; // gameMgr.Life �迭 ����
                    break;
                }
            }

            Destroy(other.gameObject); // �̶� ���� ������Ʈ ����

            // ��� ���� �̹����� fillAmount�� 0���� Ȯ��
            bool isGameOver = true;
            foreach (var life in gameMgr.Life)
            {
                if (life.fillAmount > 0)
                {
                    isGameOver = false;
                    break;
                }
            }

            // ���� ���� �г� Ȱ��ȭ
            if (isGameOver)
            {
                gameMgr.GameOverPanel.SetActive(true);
                gameMgr.RestartButton.gameObject.SetActive(true); // "�ٽ� ����" ��ư�� Ȱ��ȭ�մϴ�.
                Time.timeScale = 0; // ���� �Ͻ� ����
            }
        }
    }
}


