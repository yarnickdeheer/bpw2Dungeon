using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float speed = 100;               // speed value
    public Rigidbody rb;                    // player rigidbody        
    public bool moving = false;             // bool to check if the player is moving
    public int score;                       // score value
    public Text text;                       // text object for score
    public RectTransform healthbar;         // player healthbar object
    private int hp = 500;                   // hp value

    public void Start()
    {
        score = PlayerPrefs.GetInt("score"); 
        healthbar = GameObject.FindGameObjectWithTag("hp").GetComponent<RectTransform>(); //set healthbar object
        healthbar.sizeDelta = new Vector2(hp, healthbar.sizeDelta.y);                     //set healthbar size Y to hp value

        text = GameObject.Find("score").GetComponent<Text>();       // set texst object
        text.text = score.ToString();                               // set text to score value
    }

    public void Update()
    {
        
        float h = Input.GetAxis("Horizontal");      // set float h to input horizontal
        float v = Input.GetAxis("Vertical");        // set float v to input vertical
        Vector3 tempVect = new Vector3(h, v, 0);     // set tempvect    
        tempVect = tempVect.normalized * speed * Time.deltaTime;   
        rb.MovePosition(rb.transform.position + tempVect);  // move the player 

        if (Input.GetAxis("Horizontal") != 0 && moving == false || Input.GetAxis("Vertical") != 0 && moving ==false)    // when the horizontal or vertical input is not 0 set moving is true
        {
            moving = true;
        }
        else if (Input.GetAxis("Horizontal") == 0 && moving == true && Input.GetAxis("Vertical") == 0)  // when the imput is 0 set moving to false
        {
            moving = false;
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;    // freeze the x and y position
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;                                                      // unfreeze all position and rotations
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;     //freeze rotations and Z position
        }
        if (Input.GetAxis("Horizontal") <0) // when the horizontal input is under 0 flip the sprite and set links in the playershoot script to true
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            this.gameObject.GetComponent<playerShoot>().links = true;
        }
        else if(Input.GetAxis("Horizontal") > 0)    // when the horizontal input is over 0 flip the sprite and set links in the playershoot script to false
        {
            this.gameObject.GetComponent<playerShoot>().links = false;
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullets")      // when the player collides with enemy bullets
        {

            hp = hp - 100;  // set hp value - damage
            healthbar.sizeDelta = new Vector2(hp, healthbar.sizeDelta.y);   // rescale healthbar
            Destroy(other.gameObject);  // destroy bullet object
         
            if (hp <= 0) // when hp is 0 start gameover scene
            {
                PlayerPrefs.SetInt("score", 0);
                SceneManager.LoadScene("death");
            }
        }
    }
    
}
 