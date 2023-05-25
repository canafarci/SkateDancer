using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;
using System;

public class EndGameJumpTrigger : MonoBehaviour
{
    PlayerReferences playerReferences;
    PathFollower pathFollower;

    private void Awake()
    {
        playerReferences = FindObjectOfType<PlayerReferences>();
        pathFollower = FindObjectOfType<PathFollower>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerReferences.animator.SetTrigger("EndJump");
            print("calledJump");
            //Array.Clear(AudioManager.Instance.PlayableLevelMusic,0, AudioManager.Instance.PlayableLevelMusic.Length);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.WindSFX);
            
            StartCoroutine(SpeedChange());

            StartCoroutine(DelayedStopSmokeFX());
        }
    }

    IEnumerator DelayedStopSmokeFX()
    {
        yield return new WaitForSeconds(1f);
        foreach (var _fx in playerReferences.SmokeFX)
        {
            _fx.SetActive(false);
        }
    }

    IEnumerator SpeedChange()
    {
        while (pathFollower.speed < 40f)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            pathFollower.speed += 0.2f;
        }
        gameObject.SetActive(false);
    }
}
