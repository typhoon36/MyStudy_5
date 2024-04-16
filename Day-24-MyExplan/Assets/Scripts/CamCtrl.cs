using System.Security.Cryptography;
using UnityEngine;

public class CamCtrl : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public GameMgr gameMgr; // GameMgr 스크립트 참조

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // 카메라의 이동이 월드 공간에서 발생하도록 합니다.
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // 카메라의 y 좌표를 5로 설정합니다.
        transform.position = new Vector3(transform.position.x, 5, transform.position.z);

        // 카메라의 z 좌표가 183 이상일 때 z 좌표를 183으로 설정합니다.
        if (transform.position.z > 183)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 183);
        }

        if (Input.GetMouseButton(1)) // 마우스 우클릭을 누른 상태인지 체크
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            transform.Rotate(0, rotationX, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MummyCtrl>() != null) // 미라와 충돌했는지 체크
        {
            for (int i = 0; i < gameMgr.Life.Length; i++) // gameMgr.Life 배열 참조
            {
                if (gameMgr.Life[i].fillAmount > 0) // gameMgr.Life 배열 참조
                {
                    gameMgr.Life[i].fillAmount -= 0.5f; // gameMgr.Life 배열 참조
                    break;
                }
            }

            Destroy(other.gameObject); // 미라 게임 오브젝트 제거

            // 모든 생명 이미지의 fillAmount가 0인지 확인
            bool isGameOver = true;
            foreach (var life in gameMgr.Life)
            {
                if (life.fillAmount > 0)
                {
                    isGameOver = false;
                    break;
                }
            }

            // 게임 오버 패널 활성화
            if (isGameOver)
            {
                gameMgr.GameOverPanel.SetActive(true);
                gameMgr.RestartButton.gameObject.SetActive(true); // "다시 시작" 버튼을 활성화합니다.
                Time.timeScale = 0; // 게임 일시 정지
            }
        }
    }
}


