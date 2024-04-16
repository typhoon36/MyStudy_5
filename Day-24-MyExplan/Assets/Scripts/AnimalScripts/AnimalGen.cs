using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGen : MonoBehaviour
{
    public GameObject catPrefab; // 고양이 프리팹을 참조하기 위한 변수
    public GameObject LOVEDUCK; //러버덕
    public GameObject PenguinPrefab;//펭귄 프리팹
    public GameObject SheepPrefab; //양 프리팹

    // 스폰 위치의 범위를 설정합니다.
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = 0f;
    public float maxY = 5f;
    public float minZ = -10f;
    public float maxZ = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCat());
        StartCoroutine(SpawnLoveDuck());
        StartCoroutine(SpawnPenguin());
        StartCoroutine(SpawnSheep());



    }



    IEnumerator SpawnSheep()
    {
        while (true)
        {
            // 스폰 위치를 랜덤하게 선택합니다.
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);
            Vector3 spawnPosition = new Vector3(x, y, z);

            // 스폰 위치에 다른 오브젝트가 없는지 확인합니다.
            if (Physics.OverlapSphere(spawnPosition, 1f).Length == 0)
            {
                // 양을 스폰합니다.
                Instantiate(SheepPrefab, spawnPosition, Quaternion.identity);
            }

            // 5초마다 양을 스폰합니다.
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnLoveDuck()
    {
        while (true)
        {
            // 스폰 위치를 랜덤하게 선택합니다.
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);
            Vector3 spawnPosition = new Vector3(x, y, z);

            // 스폰 위치에 다른 오브젝트가 없는지 확인합니다.
            if (Physics.OverlapSphere(spawnPosition, 1f).Length == 0)
            {
                // 러버덕을 스폰합니다.
                Instantiate(LOVEDUCK, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPenguin()
    {
        while (true)
        {
            // 스폰 위치를 랜덤하게 선택합니다.
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);
            Vector3 spawnPosition = new Vector3(x, y, z);

            // 스폰 위치에 다른 오브젝트가 없는지 확인합니다.
            if (Physics.OverlapSphere(spawnPosition, 1f).Length == 0)
            {
                // 펭귄을 스폰합니다.
                Instantiate(PenguinPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnCat()
    {
        while (true)
        {
            // 스폰 위치를 랜덤하게 선택합니다.
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);
            Vector3 spawnPosition = new Vector3(x, y, z);

            // 스폰 위치에 다른 오브젝트가 없는지 확인합니다.
            if (Physics.OverlapSphere(spawnPosition, 1f).Length == 0)
            {
                // 고양이를 스폰합니다.
                Instantiate(catPrefab, spawnPosition, Quaternion.identity);
            }

            // 5초마다 고양이를 스폰합니다.
            yield return new WaitForSeconds(5);
        }
    }
}
