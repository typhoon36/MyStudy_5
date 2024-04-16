using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCtrl : MonoBehaviour
{
    private GameMgr gamemgr; // GameMgr ��ũ��Ʈ�� �����ϱ� ���� ����

    void Start()
    {
        // GameMgr ��ũ��Ʈ�� �ν��Ͻ��� ã�Ƽ� gamemgr ������ �Ҵ��մϴ�.
        gamemgr = GameObject.FindObjectOfType<GameMgr>();
    }

    void OnCollisionEnter(Collision coll)
    {
        // ����̰� ����̿� �¾��� ��
        if (coll.gameObject.CompareTag("Bamsongi"))
        {
            // ������ ������ŵ�ϴ�.
            gamemgr.IncreaseScore(10);

            // ����̸� �����մϴ�.
            Destroy(gameObject);
        }
    }
}
