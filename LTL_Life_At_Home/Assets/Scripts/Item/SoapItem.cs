using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapItem : MonoBehaviour
{
    bool explo = false;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 12);
    }
    // Update is called once per frame
    void Update()
    {
        if (explo)
        {
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<Animator>().Play("explosion");
            explo = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
        else if ( other.tag == "Enemy")
        {
            GetComponent<Animator>().Play("explosion");
            explo = true;
        }
    }
}
