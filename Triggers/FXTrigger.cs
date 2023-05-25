using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FXTrigger : MonoBehaviour
{
    public float FXDuration = 5f;
    public GameObject[] FXsToActivate;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<BoxCollider>().enabled = false;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.WindSFX);
            StartCoroutine(FXRoutine());
        }
    }

    private IEnumerator FXRoutine()
    {
        for (int i = 0; i < FXsToActivate.Length; i++)
        {
            FXsToActivate[i].SetActive(true);
        }

        yield return new WaitForSeconds(FXDuration);

        for (int i = 0; i < FXsToActivate.Length; i++)
        {
            FXsToActivate[i].SetActive(false);
        }
    }
}
