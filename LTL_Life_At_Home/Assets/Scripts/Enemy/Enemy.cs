using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy enemy;

    GameObject playerObj;
    Rigidbody2D rb;

    bool isOnPlat, isGround, canMove;

    public int startHP, startSpeed, countVirus;
    float hp, speed;

    // Start is called before the first frame update
    void Start()
    {
        hp = startHP;
        speed = startSpeed;
        canMove = true;
        GetComponent<SpriteRenderer>().color = new Color32((byte)Random.Range(100, 255), (byte)Random.Range(100, 255), (byte)Random.Range(100, 255), (byte)255);
        playerObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        transform.SetParent(GameController.instance.Pool);
    }
    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(8, 13, true);
        if (canMove)
        {
            if (isGround)
            {
                if (playerObj.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (playerObj.transform.position.x < transform.position.x)
                {
                    rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
            }
            else if (isOnPlat)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (hp <= 0)
        {
            speed = 0;
            GetComponent<Animator>().Play("dead");
            Destroy(gameObject, 0.75f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            isOnPlat = false;
        }
        else if (collision.gameObject.tag == "Platform")
        {
            isGround = false;
            isOnPlat = true;
        }
        else if (collision.gameObject.tag == "Mask")
        {
            canMove = false;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mask")
        {
            canMove = true;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "AlcoholGas")
        {
            hp -= 10;
        }
        else if (other.tag == "Grandma")
        {
            GameController.instance.hungerPoint -= 10;
            dead();
        }
        else if (other.tag == "Soap")
        {
            hp -= 3;
            speed /= 2;
        }
        else if (other.tag == "Mask")
        {
            canMove = false;
        }
    }
    private void dead()
    {
        GameController.instance.virusCount += 1;
        SoundManager.instance.PlayOneShotClip("EnemyAttack");
        speed = 0;
        GetComponent<Animator>().Play("dead");
        Destroy(gameObject, 0.75f);

    }

}
