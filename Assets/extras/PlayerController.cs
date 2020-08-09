using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Animator anim;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isGrounded;
    public float jumpForce;
    public float speed;
    public int scene;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        } 
    }

    void FixedUpdate()
    {
        //isGrounded = Physics2D.OverlapPoint(groundCheck.position, whatIsGround);
        float x = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(x * speed, rb.velocity.y, 0f); 
        rb.velocity = move;
        if (Input.GetAxis("Horizontal") !=0){
            anim.SetBool("walk",true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {  
        if(collision.gameObject.layer == 8)
        {
            isGrounded = true; 
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "endlevel")
        {
            SceneManager.LoadScene(scene);
        }
    }
}
