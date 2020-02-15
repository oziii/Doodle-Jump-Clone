using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yokedici : MonoBehaviour
{
    [Header("Float")]
    public float minY = .2f;
    public float maxY = 1.5f;
    public float plusY = 5.4271f;
    float minX = -1.40f;
    float maxX = 3.40f;

    [Header("Int")]
    int scoreTemp;

    [Header("GameObject")]
    public GameObject platformPrefab;
    public GameObject delik;
    public Transform target;
    public Text score;

    private void Start()
    {

    }
    //Her altta kalan platform yok edildiğinde yukarıda başka bir platform oluşuyor
    //Score textini her platform yok ettiğinde güncelliyor
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 spawnPosition = new Vector3();
        if (collision.gameObject.tag == "platform")
        {
            spawnPosition.y += /*Random.Range(minY, maxY) +*/ plusY + target.position.y;
            spawnPosition.x = Random.Range(minX,maxX);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            scoreTemp++;
            score.text = "" + scoreTemp * 10;

            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "delik")
        {
            spawnPosition.y += /*Random.Range(minY, maxY) +*/ plusY + target.position.y;
            spawnPosition.x = Random.Range(minX, maxX);
            Instantiate(delik, spawnPosition, Quaternion.identity);

            Destroy(collision.gameObject);
        }
    }
}
