using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholItem : MonoBehaviour
{
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Disapear", lifeTime);
    }

    void Disapear()
    {
        Destroy(gameObject, 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;

            GetComponent<Animator>().Play("attack");
        }
    }
}
