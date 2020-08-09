using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    public bool links = false;  //bool to see with side the player is facing
    public GameObject lp;       // left position object
    public GameObject rp;       // right position object
    public GameObject Bullet;   // bullet object

    void Update()
    {
        //when you click your left mousebutton create bullet
        if (Input.GetKeyDown(KeyCode.Mouse0))       
        {
            if (links == true)      // when links is true create the bullet on lp position
            {
                GameObject instance = Instantiate(Bullet, lp.transform.position, Quaternion.identity) as GameObject;
            }
            else                    // when links is false create the bullet on rp position
            {
                GameObject instance = Instantiate(Bullet, rp.transform.position, Quaternion.identity) as GameObject;
            }
        }
    
    }
}
