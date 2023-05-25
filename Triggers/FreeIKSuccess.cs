using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeIKSuccess : MonoBehaviour
{
    PlayerReferences playerReferences;

    private void Awake()
    {
        playerReferences = FindObjectOfType<PlayerReferences>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerReferences>()) 
        {
            int randomText = Random.Range(0, playerReferences.successTexts.Length);
            GameObject winEffect = Instantiate(playerReferences.successTexts[randomText], new Vector3(playerReferences.transform.position.x, playerReferences.transform.position.y + 8, playerReferences.transform.position.z), Quaternion.identity, playerReferences.transform);
            winEffect.transform.LookAt(Camera.main.transform.position);
            Destroy(winEffect, 5f);
        }
    }
}
