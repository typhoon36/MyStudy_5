using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyCtrl : MonoBehaviour
{
    public Transform cameraTransform; // ī�޶��� ��ȯ�� ���� ����
    public float speed = 2.0f; // �̶� ī�޶� ���󰡴� �ӵ�
    public float speedIncreaseRate = 0.01f; // �ӵ� ������

    // �����Ӹ��� �� ���� ȣ��˴ϴ�
    void Update()
    {
        // �̶��� �ӵ��� ������ŵ�ϴ�
        speed += speedIncreaseRate * Time.deltaTime;

        // ī�޶� ���� ������ ����մϴ�
        Vector3 direction = (cameraTransform.position - transform.position).normalized;
        direction.y = 0; // ������ y ������ �����մϴ�

        // �̶��� �� ��ġ�� ����մϴ�
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        // �̶��� ��ġ�� ������Ʈ�մϴ�
        transform.position = newPosition;

        // �̶� ī�޶� �ٶ󺸰� �մϴ�
        transform.LookAt(cameraTransform.position);
    }
}
