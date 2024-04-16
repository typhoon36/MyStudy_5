using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGen : MonoBehaviour
{
    public GameObject catPrefab; // ����� �������� �����ϱ� ���� ����
    public GameObject LOVEDUCK; //������
    public GameObject PenguinPrefab;//��� ������
    public GameObject SheepPrefab; //�� ������

    // ���� ��ġ�� ������ �����մϴ�.
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
            // ���� ��ġ�� �����ϰ� �����մϴ�.
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);
            Vector3 spawnPosition = new Vector3(x, y, z);

            // ���� ��ġ�� �ٸ� ������Ʈ�� ������ Ȯ���մϴ�.
            if (Physics.OverlapSphere(spawnPosition, 1f).Length == 0)
            {
                // ���� �����մϴ�.
                Instantiate(SheepPrefab, spawnPosition, Quaternion.identity);
            }

            // 5�ʸ��� ���� �����մϴ�.
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnLoveDuck()
    {
        while (true)
        {
            // ���� ��ġ�� �����ϰ� �����մϴ�.
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);
            Vector3 spawnPosition = new Vector3(x, y, z);

            // ���� ��ġ�� �ٸ� ������Ʈ�� ������ Ȯ���մϴ�.
            if (Physics.OverlapSphere(spawnPosition, 1f).Length == 0)
            {
                // �������� �����մϴ�.
                Instantiate(LOVEDUCK, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPenguin()
    {
        while (true)
        {
            // ���� ��ġ�� �����ϰ� �����մϴ�.
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);
            Vector3 spawnPosition = new Vector3(x, y, z);

            // ���� ��ġ�� �ٸ� ������Ʈ�� ������ Ȯ���մϴ�.
            if (Physics.OverlapSphere(spawnPosition, 1f).Length == 0)
            {
                // ����� �����մϴ�.
                Instantiate(PenguinPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnCat()
    {
        while (true)
        {
            // ���� ��ġ�� �����ϰ� �����մϴ�.
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);
            Vector3 spawnPosition = new Vector3(x, y, z);

            // ���� ��ġ�� �ٸ� ������Ʈ�� ������ Ȯ���մϴ�.
            if (Physics.OverlapSphere(spawnPosition, 1f).Length == 0)
            {
                // ����̸� �����մϴ�.
                Instantiate(catPrefab, spawnPosition, Quaternion.identity);
            }

            // 5�ʸ��� ����̸� �����մϴ�.
            yield return new WaitForSeconds(5);
        }
    }
}
