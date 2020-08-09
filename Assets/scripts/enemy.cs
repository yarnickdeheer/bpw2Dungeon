using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform Spos;  // enemy transform    
    GameObject Player;      // player object
   public GameObject Projectile;    // bullet object
    public Sprite spr;              // first sprite
    public Sprite spr2;             // second sprite
    private bool found = false;          // bool to see if we found the player       
    private bool canShoot = true;    // bool to see if we can shoot

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("player");    //set the player object

    }

    void Update()
    {
        Vector3 current = this.gameObject.transform.position; // this is a vector 3 for the curreent position
        float dist = Vector3.Distance(Player.transform.position, transform.position);   // float to mesure the distance between the enemy and the player
   
        if (dist < 3 && dist > 2)// when the distance is below 3
        {
            found = true;   // we found the player
            this.gameObject.GetComponent<Rigidbody>().velocity = (Player.transform.position - this.gameObject.transform.position) * 1;  //add velocity to the enemy in the direction of the player

        }else if(dist > 4.5f)//if ist higher than 4.5
        {
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;    // freeze position
            found = false;  // we lost the player
            
        }
        if (found == false) // when we lost the player
        {
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;  // unfreeze all
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ; // freeze rotations and Z position
        }
        if (this.gameObject.transform.position.x < Player.transform.position.x) // when the player is on the left side flip the enemy to face him
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX= true;
        }
        else// when the player is not on the left side flip the enemy to face him
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
            
    }
    
    
    private IEnumerator wait()  
    {
        yield return new WaitForSeconds(1f);        // wait 1 second
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spr2;   // set sprite
        yield return new WaitForSeconds(.5f);  // wait .5 seconds
        canShoot = true;    // set can shoot to true
    }

        private void OnTriggerStay(Collider other)
        {
        if (other.gameObject.tag == "player")   // when the player is within the collider
        {
            if(canShoot == true)    // when canshoot is true 
            {
                canShoot = false;   // can shoot is false
                this.gameObject.GetComponent<SpriteRenderer>().sprite = spr;    // set sprite
                Vector3 start = new Vector3(Spos.position.x, Spos.position.y, Spos.position.z);   // make a vector 3 to host the start position of the bullet
                GameObject instance = Instantiate(Projectile, start, Quaternion.identity) as GameObject;    // instantiate the bullet on startposition
                StartCoroutine(wait()); // start coroutine
            }
         
        }
    }
}
