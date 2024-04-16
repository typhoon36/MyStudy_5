using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepCtrl : MonoBehaviour
{
    private GameMgr gamemgr; // GameMgr 스크립트를 참조하기 위한 변수

    // Start is called before the first frame update
    void Start()
    {
        // GameMgr 스크립트의 인스턴스를 찾아서 gamemgr 변수에 할당합니다.
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
            // 점수를 증가시킵니다.
            gamemgr.Score += 30;

            // 양을 제거합니다.
            Destroy(gameObject);
        }
    }


}
