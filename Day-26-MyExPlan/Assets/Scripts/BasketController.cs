using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public AudioClip appleSE;
    public AudioClip bombSE;
    AudioSource aud;
    GameObject director;

    //��ƼŬ �ý��� ����
    public ParticleSystem particleEffect;

    Color appleColor = Color.white;
    Color bombColor = Color.black;
    //~��ƼŬ �ý��� ����

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        this.aud = GetComponent<AudioSource>();
        this.director = GameObject.Find("GameDirector");

        // ��ƼŬ �ý��� �ʱ�ȭ
        particleEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 'Apple'�� 'Bomb' ���̾ �����ϵ��� ���̾� ����ũ�� �����մϴ�.
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

            //����� ������ ��ƼŬ �ý����� ������ ������� �����մϴ�.
            var main = particleEffect.main;
            main.startColor = new ParticleSystem.MinMaxGradient(appleColor);

            particleEffect.Play();
        }
        else if (other.gameObject.tag == "Bomb")
        {
            this.aud.PlayOneShot(this.bombSE);
            this.director.GetComponent<GameDirector>().GetBomb();

            // ��ź�� ������ ��ƼŬ �ý����� ������ ���������� �����մϴ�.
            var main = particleEffect.main;
            main.startColor = new ParticleSystem.MinMaxGradient(bombColor);

            particleEffect.Play();
        }

        Destroy(other.gameObject);
    }

}
