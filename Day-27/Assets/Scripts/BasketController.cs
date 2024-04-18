using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public AudioClip appleSE;
    public AudioClip bombSE;
    AudioSource aud;
    GameObject director;
    GameDirector m_GDirect;

    LayerMask m_StageMask = -1;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        this.aud = GetComponent<AudioSource>();
        this.director = GameObject.Find("GameDirector");
        m_GDirect = director.GetComponent<GameDirector>();

        m_StageMask = 1 << LayerMask.NameToLayer("Stage");

    }

    // Update is called once per frame
    void Update()
    {
        if(m_GDirect != null && m_GDirect.time <= 0) //게임오버
        
            return;
        

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, m_StageMask.value))
            {
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);
                transform.position = new Vector3(x, 0, z);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Apple")
        {
            //Debug.Log("Tag=Apple");
            this.aud.PlayOneShot(this.appleSE);
            this.director.GetComponent<GameDirector>().GetApple();
            ParticleSystem.MainModule a_main = this.GetComponent<ParticleSystem>().main;
            a_main.startColor = Color.white;
                /*new Color(1.0f,1.0f,1.0f,1.0f); */

            //this.GetComponent<ParticleSystem>().main.startColor = Color.red; //변수로 받게 되어있으니 따로 할당해줘야함.
        }
        else
        {
            //Debug.Log("Tag=Bomb");
            this.aud.PlayOneShot(this.bombSE);
            this.director.GetComponent<GameDirector>().GetBomb();
            ParticleSystem.MainModule a_main = this.GetComponent<ParticleSystem>().main;
            a_main.startColor = Color.black; //new Color(0.0f,0.0f,0.0f,1.0f);
        }

        this.GetComponent<ParticleSystem>().Play();

        Destroy(other.gameObject);
    }
}
