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

    // 미라의 생성 위치 범위를 설정합니다.
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
                // 미라의 위치를 랜덤으로 설정합니다.
                float x = Random.Range(minX, maxX);
                float z = Random.Range(minZ, maxZ);

                // 미라의 y 좌표를 땅의 높이로 설정합니다.
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
