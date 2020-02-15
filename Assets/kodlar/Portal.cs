using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject portal;
    public GameObject hero;

    float x;

    //Portala değen hero diğer portalın x değerini alıp heronun y değerine göre ışınlanıyor
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "hero")
        {
            if(portal.transform.position.x > 0)
            {
                x = 3.68f;
            }
            else
            {
                x = -1.56f;
            }
            hero.transform.position = new Vector2(x, hero.transform.position.y);
        }
    }
}
