using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{

     
    void Start()
    {

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // get mouseposition
        mousePosition.z = 0;    
        this.gameObject.GetComponent<Rigidbody>().velocity = (mousePosition - this.gameObject.transform.position).normalized * 10; // add velocity to the bullet in the direction of the mouseposition
        StartCoroutine(wait());     //start coroutine wait
        
    }
    private IEnumerator wait()// after 5 second without hitting an enemy destroy object
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "wall")
        {
            Destroy(this.gameObject);
        }
    }
}

