using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskItem : MonoBehaviour
{
    public float startLifeTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("PlayNormal", startLifeTime / 2);
        Invoke("PlayBreak", startLifeTime);
    }

    void PlayNormal()
    {
        GetComponent<Animator>().Play("normal");
    }

    void PlayBreak()
    {
        GetComponent<Animator>().Play("break");
        Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Collider2D>().isTrigger = true;
        }
    }
}
