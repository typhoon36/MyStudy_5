using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Mgr : MonoBehaviour
{
    public static GameObject m_BulletPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        m_BulletPrefab = Resources.Load("BulletPrefab") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
