using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepCtrl : MonoBehaviour
{
    private GameMgr gamemgr; // GameMgr ��ũ��Ʈ�� �����ϱ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        // GameMgr ��ũ��Ʈ�� �ν��Ͻ��� ã�Ƽ� gamemgr ������ �Ҵ��մϴ�.
        gamemgr = Object.FindObjectOfType<GameMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bamsongi"))
        {
            // ������ ������ŵ�ϴ�.
            gamemgr.Score += 30;

            // ���� �����մϴ�.
            Destroy(gameObject);
        }
    }


}
