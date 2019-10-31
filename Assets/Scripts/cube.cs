using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -4.0f)
        {
            transform.position = new Vector2(0,4.0f);
        }
        if(rb.velocity.y < -5.0f)
        {
            rb.velocity = new Vector2(0, -2.0f);
        }
    }
}
