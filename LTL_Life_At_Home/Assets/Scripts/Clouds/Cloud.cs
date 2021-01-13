using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public int type;
    public float speed;
    bool isMovingRight;
    // Start is called before the first frame update
    void Start()
    {
        if (type == 0)
        {
            speed = 0.03f;
        }
        else if (type == 1)
        {
            speed = 0.015f;
        }
        else if (type == 2)
        {
            speed = 0.01f;
        }
        else if (type == 3)
        {
            speed = 0.005f;
        }
        else
        {
            speed = 0.002f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight)
        {
            transform.Translate(Vector2.right * speed);
        }
        else
        {
            transform.Translate(Vector2.left * speed);
        }

        if (transform.position.x >= 18.5f)
        {
            isMovingRight = false;
        }
        else if (transform.position.x <= -18.5f)
        {
            isMovingRight = true;
        }
    }
}
