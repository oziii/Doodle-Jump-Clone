using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodler : MonoBehaviour
{
    [Header("Float")]
    public float h;
    public float maxHiz;
    public float hiz;
    public float minY = .2f;
    public float maxY = 1.5f;
    float minX = -1.40f;
    float maxX = 3.40f;

    [Header("Int")]
    public int platformCount;
    public int numberOfPlatforms;

    [Header("Bool")]
    public bool android;
    public bool sag;
    public bool sol;

    [Header("GameObject")]
    public GameObject platformPreFabs;
    Rigidbody2D agirlik;
    Vector3 spawnPosition = new Vector3();


    void Start()
    {
        agirlik = GetComponent<Rigidbody2D>();
        platformCount = 0;
        //oyun başında belli bir aşama rastegele platform oluşturuyoruz
        for (; platformCount < numberOfPlatforms; platformCount++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(minX, maxX);
            Instantiate(platformPreFabs, spawnPosition, Quaternion.identity);
        }
    }

    
    private void Update()
    {


    }
    private void FixedUpdate()
    {
        agirlik.AddForce(Vector2.right * h * hiz);
        if (android) {
            if (sol)
            {
                h = -0.01f;
                transform.localScale = new Vector2(-1, 1);//sola gitmesi için karaterin x eksende dönmesi
                agirlik.velocity = new Vector2(-maxHiz, agirlik.velocity.y);//karaktere hız uygulaması için
            }
            else if (sag)
            {
                h = 0.01f;
                transform.localScale = new Vector2(1, 1);//saga gitmesi için karaterin x eksende dönmesi
                agirlik.velocity = new Vector2(maxHiz, agirlik.velocity.y);//karaktere hız uygulaması için

            }
            else if (!sol && !sag)
            {
                h = 0;
                agirlik.velocity = new Vector2(0, agirlik.velocity.y);
            }
        }
        else
        {
            h = Input.GetAxis("Horizontal");
            if (h < 0.1f)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else if (h > 0.1f)
            {
                transform.localScale = new Vector2(1, 1);
            }
            if (agirlik.velocity.x > maxHiz)
            {
                agirlik.velocity = new Vector2(maxHiz, agirlik.velocity.y);
            }
            else if (agirlik.velocity.x < -maxHiz)
            {
                agirlik.velocity = new Vector2(-maxHiz, agirlik.velocity.y);
            }
            else if (h == 0)//heronun kaymaması için tuşu bıraktığında hızı sıfırlıyoruz.
            {
                agirlik.velocity = new Vector2(0, agirlik.velocity.y);
            }
        }
    }
    //Hero düştüğünde kamera altındaki gameobjecte değdiğinde tekrar başlatıyor.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "dusme")
        {
            retry();
        }
        else if(collision.gameObject.tag == "delik")
        {
            retry();
        }
    }

    public void retry()
    {
        Application.LoadLevel("1");
    }

}
