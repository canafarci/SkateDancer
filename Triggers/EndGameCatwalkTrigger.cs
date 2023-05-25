using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class EndGameCatwalkTrigger : MonoBehaviour
{
    PlayerReferences playerReferences;

    private void Awake()
    {
        playerReferences = FindObjectOfType<PlayerReferences>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            playerReferences.SpeedUpFX.SetActive(false);
            playerReferences.animator.SetTrigger("Catwalk");
            playerReferences.GetComponent<PathFollower>().speed = 5f;
            playerReferences.EndGameCinematicCam.SetActive(true);
        }
    }
}
