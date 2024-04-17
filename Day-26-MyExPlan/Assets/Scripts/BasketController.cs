using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public AudioClip appleSE;
    public AudioClip bombSE;
    AudioSource aud;
    GameObject director;

    //파티클 시스템 관련
    public ParticleSystem particleEffect;

    Color appleColor = Color.white;
    Color bombColor = Color.black;
    //~파티클 시스템 관련

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        this.aud = GetComponent<AudioSource>();
        this.director = GameObject.Find("GameDirector");

        // 파티클 시스템 초기화
        particleEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 'Apple'과 'Bomb' 레이어를 무시하도록 레이어 마스크를 설정합니다.
            int layerMask = 1 << LayerMask.NameToLayer("Apple") | 1 << LayerMask.NameToLayer("Bomb");
            layerMask = ~layerMask;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
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
            this.aud.PlayOneShot(this.appleSE);
            this.director.GetComponent<GameDirector>().GetApple();

            //사과에 맞으면 파티클 시스템의 색상을 흰색으로 변경합니다.
            var main = particleEffect.main;
            main.startColor = new ParticleSystem.MinMaxGradient(appleColor);

            particleEffect.Play();
        }
        else if (other.gameObject.tag == "Bomb")
        {
            this.aud.PlayOneShot(this.bombSE);
            this.director.GetComponent<GameDirector>().GetBomb();

            // 폭탄에 맞으면 파티클 시스템의 색상을 검정색으로 변경합니다.
            var main = particleEffect.main;
            main.startColor = new ParticleSystem.MinMaxGradient(bombColor);

            particleEffect.Play();
        }

        Destroy(other.gameObject);
    }

}
