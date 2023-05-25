/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeIKCheckPlayer : MonoBehaviour
{
    FreeIKTrigger freeIKTrigger;

    private void Awake()
    {
        freeIKTrigger = GetComponentInParent<FreeIKTrigger>();
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBody")) 
        {
            StartCoroutine(StartSpeedUp());
            print("xD");
        }
    }

    IEnumerator StartSpeedUp() 
    {
        yield return new WaitForSecondsRealtime(1f);
        StopCoroutine(freeIKTrigger.FreeIKRoutine);
        StartCoroutine(freeIKTrigger.FreeIKSpeedUp());
    }
} */
