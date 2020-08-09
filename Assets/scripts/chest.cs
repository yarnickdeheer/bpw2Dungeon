using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    private GameObject key;                     // this is the key sprite in the ui
    private bool inter;                         // this is the bool to check say you can interact with the object
    public GameObject stair;                    // stair object
    public GameObject tt;                       // this is the text indicator object
    void Awake()
    {
        key = GameObject.Find("key");           // set key object
        key.SetActive(false);                   // set key object inactive
        tt = GameObject.Find("interact");       // set text object
        tt.SetActive(false);                    // set text object inactive
    }
    
    void Update()
    {
        stair = GameObject.FindGameObjectWithTag("stair");  //set stair object
        stair.GetComponent<End>().tt = tt;                  //set stairs text component to this tt component

        if (inter == true && Input.GetKeyDown(KeyCode.E))       //when inter bool is true and player presses E set bool keyobtained true and set key object active
        {
            stair.GetComponent<End>().keyobtained = true;
            key.SetActive(true);
        }   
    }
    private void OnTriggerEnter(Collider other)                 // when object with player tag is in trigger collider set inter true and tt object active
    {
        if (other.gameObject.tag == "player")
        {
            stair.GetComponent<End>().tt.SetActive(true);
            inter = true;
          
        }
    }
    private void OnTriggerExit(Collider other)                      // when object with player tag exits trigger collider set inter false and tt object inactive
    {
        if (other.gameObject.tag == "player")
        {
            stair.GetComponent<End>().tt.SetActive(false);
            inter = false;

        }
    }
}
