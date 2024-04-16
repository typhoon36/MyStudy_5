using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCtrl : MonoBehaviour
{
    private GameMgr gamemgr; // GameMgr 스크립트를 참조하기 위한 변수

    void Start()
    {
        // GameMgr 스크립트의 인스턴스를 찾아서 gamemgr 변수에 할당합니다.
        gamemgr = GameObject.FindObjectOfType<GameMgr>();
    }

    void OnCollisionEnter(Collision coll)
    {
        // 고양이가 밤송이에 맞았을 때
        if (coll.gameObject.CompareTag("Bamsongi"))
        {
            // 점수를 증가시킵니다.
            gamemgr.IncreaseScore(10);

            // 고양이를 삭제합니다.
            Destroy(gameObject);
        }
    }
}
