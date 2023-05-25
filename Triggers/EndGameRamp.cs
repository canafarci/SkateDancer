using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameRamp : MonoBehaviour
{
    public GameObject ramp;
    public Transform position;
    public GameObject EndGameCam;
    public GameObject endGameCanvas;
    TimeManager timeManager;

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }

    private void Update()
    {
        /* if (movingToPos)
        {
            ramp.transform.position = Vector3.MoveTowards(ramp.transform.position, position.transform.position, 250f * Time.deltaTime);
        } */

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Time.timeScale = 1f;
            timeManager.SlowMo(0.01f);

            endGameCanvas.SetActive(true);
            
            if (EndGameCam != null)
            {
                Animator _animator = FindObjectOfType<PlayerReferences>().animator;
                //_animator.Play("Skating_action");
                _animator.SetTrigger("EndGameSquat");
            }
        }
    }
}
