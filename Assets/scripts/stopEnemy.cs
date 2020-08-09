using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopEnemy : MonoBehaviour
{
    public GameObject spook;
 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player" ) // when player is within the collider freeze X and Y position and rotation
        {
            spook.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
            spook.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")   // when player exits the collider freeze X and Y position and rotation unfreeze all and refreeze rotation and Zposition
        {
            spook.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
            spook.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            spook.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        }

    }
}
