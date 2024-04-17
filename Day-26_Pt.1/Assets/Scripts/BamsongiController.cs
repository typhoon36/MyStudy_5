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
        Destroy(this.gameObject, 15.0f);
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
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<ParticleSystem>().Play();

        Destroy(this.gameObject, 4.0f);

        if(coll.gameObject.tag == "Enemy")
        {
            //--- ����� ���� �Ⱥ��̰� ����...
            GetComponent<SphereCollider>().enabled = false;

            MeshRenderer[] a_ChildList =
                    gameObject.GetComponentsInChildren<MeshRenderer>();
            for(int i = 0; i < a_ChildList.Length; i++)
            {
                a_ChildList[i].enabled = false;
            }
            //--- ����� ���� �Ⱥ��̰� ����...

            //--- �����ֱ�
            //GameObject a_Obj = GameObject.Find("GameMgr");
            //if(a_Obj != null)
            //{
            //    GameMgr a_GMgr = a_Obj.GetComponent<GameMgr>();
            //    if (a_GMgr != null)
            //        a_GMgr.AddScore();  
            //}

            GameMgr.Inst.AddScore();
            //--- �����ֱ�

            Destroy(coll.gameObject);   //�浹�� �� ĳ���� ��� ����
        }//if(coll.gameObject.tag == "Enemy")

    }//void OnCollisionEnter(Collision coll)
}
