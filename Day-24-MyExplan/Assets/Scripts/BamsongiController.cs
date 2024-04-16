using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsongiController : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        //Shoot(new Vector3(0, 200, 2000));
        Destroy(this.gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
    }

    void OnCollisionEnter(Collision coll)
    {
        // 연속 충돌 감지를 비활성화합니다.
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;

        // 이제 isKinematic 속성을 안전하게 true로 설정할 수 있습니다.
        GetComponent<Rigidbody>().isKinematic = true;

        GetComponent<ParticleSystem>().Play();

      
    }
}
