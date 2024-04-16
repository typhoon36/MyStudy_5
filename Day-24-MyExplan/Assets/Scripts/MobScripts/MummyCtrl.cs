using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyCtrl : MonoBehaviour
{
    public Transform cameraTransform; // 카메라의 변환에 대한 참조
    public float speed = 2.0f; // 미라가 카메라를 따라가는 속도
    public float speedIncreaseRate = 0.01f; // 속도 증가율

    // 프레임마다 한 번씩 호출됩니다
    void Update()
    {
        // 미라의 속도를 증가시킵니다
        speed += speedIncreaseRate * Time.deltaTime;

        // 카메라를 향한 방향을 계산합니다
        Vector3 direction = (cameraTransform.position - transform.position).normalized;
        direction.y = 0; // 원래의 y 방향을 유지합니다

        // 미라의 새 위치를 계산합니다
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        // 미라의 위치를 업데이트합니다
        transform.position = newPosition;

        // 미라가 카메라를 바라보게 합니다
        transform.LookAt(cameraTransform.position);
    }
}
