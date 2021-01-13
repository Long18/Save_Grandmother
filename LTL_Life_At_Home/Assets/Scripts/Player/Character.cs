using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float playerSpeed, jumpForce, throwForce;
    public Collider2D player, platform;
    public int itemStat = 4;
    int moveInput, itemInput;
    bool isGround, isOnPlatform, nearGrandma, isRunning, isJumping;
    Transform weaponPos, groundCheckPos;

    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        weaponPos = transform.Find("weaponPos");
        groundCheckPos = transform.Find("GroundCheckPos");
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(8, 12, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.gameStage == GameStage.Over || GameController.instance.gameStage == GameStage.Ready)
        {
            itemStat = 4;
            for (int i = 0; i < 3; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }

        isGround = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, 10);
        isOnPlatform = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, 11);

    }

    void FixedUpdate()
    {
        CharacterMovement();
    }

    void CharacterMovement()
    {
        rb.velocity = new Vector2(moveInput * playerSpeed, rb.velocity.y);
    }

    public void OnLeftButtonDown()
    {
        moveInput = -1;
        itemInput = -1;
        //transform.eulerAngles = new Vector3(0, -180, 0);
        transform.localRotation = Quaternion.Euler(0, -180, 0);
        isRunning = true;
        anim.SetBool("isRunning", true);
        SoundManager.instance.PlayLoopClip("Run");
    }

    public void OnRightRightDown()
    {
        moveInput = 1;
        itemInput = 1;
        //transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        isRunning = true;
        anim.SetBool("isRunning", true);
        SoundManager.instance.PlayLoopClip("Run");
    }

    public void OnMoveButtonUp()
    {
        isRunning = false;
        moveInput = 0;
        anim.SetBool("isRunning", false);
        SoundManager.instance.StopLoopClip("Run");
    }

    public void OnJumpButtonDown()
    {
        if (isGround || isOnPlatform)
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeOff");
            anim.SetBool("isJumping", true);
            SoundManager.instance.PlayOneShotClip("Jump");
        }
    }
    public void OnDownButtonDown()
    {
        if (isOnPlatform == true)
        {
            isJumping = true;
            Physics2D.IgnoreCollision(player, platform, true);
            anim.SetTrigger("down");
        }
    }

    public void OnActionButtonDown()
    {
        ActionItem(itemStat);
    }

    void ActionItem(int ind)
    {
        if (ind <= 2 && !nearGrandma && GameController.instance.itemCount[ind] > 0)
        {
            GameObject obj = Instantiate(items[ind], weaponPos.position, Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(itemInput, 1) * throwForce);
            if (itemInput == -1)
            {
                obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                obj.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            GameController.instance.itemCount[ind] -= 1;
            anim.SetTrigger("thro");
            if (GameController.instance.itemCount[ind] <= 0)
            {
                transform.GetChild(itemStat).gameObject.SetActive(false);
            }
            obj.transform.SetParent(GameController.instance.Pool);
        }
        else if (nearGrandma && GameController.instance.foodCount > 0)
        {
            GameController.instance.foodCount -= 1;
            GameController.instance.hungerPoint += GameController.instance.healthValue;
        }
    }

    public void OnSwitchDown()
    {
        transform.GetChild(itemStat).gameObject.SetActive(false);
        itemStat++;
        if (itemStat > 2) { itemStat = 0; }
        if (GameController.instance.itemCount[itemStat] >0) { transform.GetChild(itemStat).gameObject.SetActive(true); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            Physics2D.IgnoreCollision(player, platform, false);
            anim.SetBool("isJumping", false);
            SoundManager.instance.PlayOneShotClip("Collision");
        }
        else if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            anim.SetBool("isJumping", false);
            SoundManager.instance.PlayOneShotClip("Collision");
        }

        if (collision.gameObject.tag == "Supply")
        {
            SoundManager.instance.PlayOneShotClip("GetItem");
            Destroy(collision.gameObject);
            if (GameController.instance.itemCount[0] == 3 && GameController.instance.itemCount[1] == 3 && GameController.instance.itemCount[2] == 3)
            {
                GameController.instance.foodCount += 1;
            }
            else
            {
                int gift = Random.Range(0, 4);
                if (gift <= 2)
                {
                    if (itemStat > 2 || GameController.instance.itemCount[itemStat] <= 0) { itemStat = gift; transform.GetChild(itemStat).gameObject.SetActive(true); }
                    GameController.instance.itemCount[gift] += 1;
                }
                else
                {
                    GameController.instance.foodCount += 1;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Supply")
        {
            SoundManager.instance.PlayOneShotClip("GetItem");
            Destroy(other.gameObject);
            if (GameController.instance.itemCount[0] == 3 && GameController.instance.itemCount[1] == 3 && GameController.instance.itemCount[2] == 3)
            {
                GameController.instance.foodCount += 1;
            }
            else
            {
                int gift = Random.Range(0, 4);
                if (gift <= 2)
                {
                    if (itemStat > 2 || GameController.instance.itemCount[itemStat] <= 0) { itemStat = gift; transform.GetChild(itemStat).gameObject.SetActive(true); }
                    GameController.instance.itemCount[gift] += 1;
                }
                else
                {
                    GameController.instance.foodCount += 1;
                }
            }
        }
        else if (other.gameObject.tag == "Grandma")
        {
            nearGrandma = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grandma")
        {
            nearGrandma = false;
        }
    }
}
