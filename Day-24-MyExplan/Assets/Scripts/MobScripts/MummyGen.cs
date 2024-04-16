using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyGen : MonoBehaviour
{
    public GameObject mummyPrefab;
    public float spawnInterval = 1.0f;
    private float timer = 0.0f;
    public GameObject mainCamera;
    private Vector3 lastPosition;

    // �̶��� ���� ��ġ ������ �����մϴ�.
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;

    void Start()
    {
        lastPosition = mainCamera.transform.position;
    }

    void Update()
    {
        float cameraSpeed = (mainCamera.transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = mainCamera.transform.position;

        if (Mathf.Approximately(cameraSpeed, 0f))
        {
            timer += Time.deltaTime;

            if (timer > spawnInterval)
            {
                // �̶��� ��ġ�� �������� �����մϴ�.
                float x = Random.Range(minX, maxX);
                float z = Random.Range(minZ, maxZ);

                // �̶��� y ��ǥ�� ���� ���̷� �����մϴ�.
                float y = 0;

                GameObject mummy = Instantiate(mummyPrefab, new Vector3(x, y, z), Quaternion.identity);
                MummyCtrl mummyCtrl = mummy.GetComponent<MummyCtrl>();
                mummyCtrl.cameraTransform = Camera.main.transform;
                timer = 0.0f;
            }
        }
        else
        {
            timer = 0.0f;
        }
    }
}
