using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public float speed = 0.15f;
    public float span = 3;
    private float count;

    public bool isLeft;

    private SpriteRenderer spRenderer;    
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        count = span;
        spRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {

        var dir = isLeft ? -1 : 1;
        rb2D.velocity = new Vector2(dir * speed, 0);

        if (count <= 0)
        {
            isLeft = !isLeft;
            spRenderer.flipX = isLeft;
            count = span;
        }
        count -= Time.deltaTime;
    }
}
