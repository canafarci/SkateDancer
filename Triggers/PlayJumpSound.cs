using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayJumpSound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int rand = Random.Range(0, AudioManager.Instance.JumpSFXs.Length);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.JumpSFXs[rand], 0.7f);
        }
    }
}
