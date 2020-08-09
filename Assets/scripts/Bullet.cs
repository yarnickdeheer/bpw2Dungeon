using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bullet : MonoBehaviour
{
    GameObject Player; // player object

   
    void Start()
    { 
        Player = GameObject.FindGameObjectWithTag("player"); // set player object
        // add velocity to the bullet in the direction of the player position
        this.gameObject.GetComponent<Rigidbody>().velocity = (Player.transform.position - this.gameObject.transform.position).normalized * 10;
        StartCoroutine(wait());                 //start coroutine wait 


    }
    private IEnumerator wait()
    {
        // delay of 5 second when the bullet has not hit it wil be destroyed
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            Destroy(this.gameObject);
        }
    }

}
 