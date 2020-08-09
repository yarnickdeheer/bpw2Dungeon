using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Sprite[] sp;                     // array of sprites for the states of the object
    public bool keyobtained = false;        // bool  for when you obtain the key    
    public bool openP = false;              // bool when you can interact with the stairs
    public bool next = false;               // bool when you can use the stairs
    public GameObject tt;                   // text object
    public GameObject player;               // player object


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }
    void Update()
    {
        if (openP == true && Input.GetKeyDown(KeyCode.E))   //when you can interact woth the object and press E set the sprite and start coroutine 
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sp[0];
            StartCoroutine(wait());
        }
    }
    private IEnumerator wait() 
    {
        yield return new WaitForSeconds(1f);                            // wait for 1 second
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sp[1];  // set sprite
        next = true;                                                    // set next to true
        tt.SetActive(false);                                            // set text object inactive

    }
        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player" && keyobtained ==true && next == false)            // when the player is within the collider and key is obtained and next is still false
        {
            tt.SetActive(true); // set text object active
            openP = true;       // set openP true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player" && next ==true)        // when the player stays within the collider start the scene
        {
            PlayerPrefs.SetInt("score", player.GetComponent<PlayerControl>().score);
            SceneManager.LoadScene("game");
        }

    }
    private void OnTriggerExit(Collider other)          // when the player exits the collider set text object false and openP false
    {
        if (other.gameObject.tag == "player" )
        {
            tt.SetActive(false);
            openP = false;

        }
    }
}
