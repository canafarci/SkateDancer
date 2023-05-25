using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedSetActiveTrigger : MonoBehaviour
{
    public GameObject ObjectToActivate;
    public float delay = 1f;

    public void DelayedSetActiveObject()
    {
        StartCoroutine(DelayedSetActiveObjectRoutine());
    }
    private IEnumerator DelayedSetActiveObjectRoutine()
    {
        yield return new WaitForSecondsRealtime(delay);
        ObjectToActivate.SetActive(true);
    }
}
