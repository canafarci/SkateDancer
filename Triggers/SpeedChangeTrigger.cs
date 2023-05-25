using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpeedChangeTrigger : MonoBehaviour
{
    public float Speed;
    PathFollower pathFollower;
    BoxCollider boxCollider;

    private void Awake()
    {
        pathFollower = FindObjectOfType<PathFollower>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            boxCollider.enabled = false;
            StartCoroutine(SpeedChange());
        }
    }

    IEnumerator SpeedChange()
    {
        if (pathFollower.speed < Speed)
        {
            while(pathFollower.speed < Speed)
            {
                yield return new WaitForSecondsRealtime(0.01f);
                pathFollower.speed += 0.1f;
            }
            gameObject.SetActive(false);
        }
        else
        {
            while(pathFollower.speed > Speed)
            {
                yield return new WaitForSecondsRealtime(0.01f);
                pathFollower.speed -= 0.1f;
            }
        }
        
    }
}
