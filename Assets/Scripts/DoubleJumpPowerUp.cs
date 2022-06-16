using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DoubleJumpPowerUp : MonoBehaviour
{
    public GameObject particleEffect;
    public float powerupTime = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PickUp(other));
        }
    }


    IEnumerator PickUp(Collider player)
    {
        Instantiate(particleEffect, transform.position, transform.rotation);        
        PlayerController playerControllerScript = player.GetComponent<PlayerController>();
        playerControllerScript.doubleJumpEnabled = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(powerupTime);
        playerControllerScript.doubleJumpEnabled = false;
        Destroy(gameObject);
    }
}
