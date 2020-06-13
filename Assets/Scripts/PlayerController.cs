using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed = 5;
    public float jumpForce = 350;

    public LayerMask groundLayer;
    
    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer spRenderer;

    private int friendScore;
    private bool isPlaying;

    public GameObject resultScreen;
    public Text friendScoreScreen;
    public Text judge;
    
    private bool isGround;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spRenderer = GetComponent<SpriteRenderer>();
        friendScore = 0;
        resultScreen.SetActive(false);
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            var vx = rb2D.velocity.x;
            var vy = rb2D.velocity.y;
            animator.SetFloat("Speed", vx);
            float x = Input.GetAxis("Horizontal");
            rb2D.AddForce(Vector2.right * x * speed);
            spRenderer.flipX = x < 0;


            if (Mathf.Abs(vx) > 5)
            {
                rb2D.velocity = new Vector2(Mathf.Sign(vx) * 5, vy);
            }

            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGround)
            {
                animator.SetBool("isJump", true);
                rb2D.AddForce(Vector2.up * jumpForce);
            }

            if (isGround)
            {
                animator.SetBool("isJump", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }
    }

    private void FixedUpdate()
    {
        isGround = false;

        Vector2 groundPos =
            new Vector2(
                transform.position.x,
                transform.position.y
            );

        Vector2 groundArea = new Vector2(0.5f, 0.5f);

        Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red);

        isGround =
            Physics2D.OverlapArea(
                groundPos + groundArea,
                groundPos - groundArea,
                groundLayer
            );

        // Debug.Log(isGround);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Friend"))
        {
            var friend = other.gameObject.GetComponent<SayYo>();
            friendScore += friend.score;
            friend.Yo();
        }

        if (other.CompareTag("Finish"))
        {
            rb2D.velocity = new Vector2(0f, 0f);
            isPlaying = false;
            resultScreen.SetActive(true);
            CallResult();
        }
    }

    private void CallResult()
    {
        friendScoreScreen.text = "友達の人数：" + friendScore.ToString();
        String judgeText;
        if (friendScore >= 6)
        {
            judgeText = "友達多いね、もしかしてパリピ？";
        }
        else if (friendScore >= 1)
        {
            judgeText = "友達は大事にしないとね";
        }
        else
        {
            judgeText = "お前が真のぼっちだ！研究者適性あり！";
        }
        judge.text = judgeText;
    }
}
