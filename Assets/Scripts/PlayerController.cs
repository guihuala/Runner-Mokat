using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rigid;
    private int jumpCount; // 追踪跳跃次数
    public int maxJumpCount = 2; // 最大跳跃次数
    private GameManager gameManager;

    public AudioClip die;
    public AudioClip award;
    public AudioClip jump;

    private Sfx sfx;

    void Start()
    {
        jumpCount = 0;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        sfx = FindObjectOfType<Sfx>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && jumpCount < maxJumpCount)
        {
            anim.SetTrigger("jump");
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * 2000);
            jumpCount++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigid.AddForce(Vector2.down * 10);
        }
        if (Input.GetKeyDown(KeyCode.S) && jumpCount == 0)
        {
            anim.SetBool("down",true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("down", false);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            jumpCount = 0;
        }
        else if (collision.transform.tag == "Barrier")
        {
            sfx.PlaySfx(die);
            gameManager.GameOver();
            anim.speed = 0;
        }
        else if (collision.transform.tag == "Buff")
        {
            sfx.PlaySfx(jump);
            rigid.AddForce(new Vector3(0.4f, 2, 0) * 600);
        }
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Award")
        {
            sfx.PlaySfx(award);
            gameManager.AddScore(10);
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "Barrier")
        {
            sfx.PlaySfx(die);
            gameManager.GameOver();
            anim.speed = 0;
        }
    }
}


