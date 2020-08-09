using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    private int health = 10;            // enemy health value
    public GameObject bar;              // enemy health bar
    private float step;                 // steps of damage
    public GameObject parrent;          // enemy object
    private GameObject player;          // player object

    void Start()
    {
        step = bar.transform.localScale.x / health * 2;         // set step value 
        player = GameObject.FindGameObjectWithTag("player");    // set player object
    }
    
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.tag == "playerBullet")     //check if object collides with playerbullet objects
        {
            health = health - 2;    // set new health after damage
            Destroy(other.gameObject);  // destroy bullet object

            bar.transform.localScale = new Vector3(bar.transform.localScale.x - step, bar.transform.localScale.y, bar.transform.localScale.z);  // rescale  healthbar
            if (health <= 0)
            {
                player.GetComponent<PlayerControl>().score = player.GetComponent<PlayerControl>().score + 100;  // add points to score
                player.GetComponent<PlayerControl>().text.text = player.GetComponent<PlayerControl>().score.ToString(); //reset score counter
                Destroy(parrent.gameObject); // destroy this object
            }
        }
    }
}
