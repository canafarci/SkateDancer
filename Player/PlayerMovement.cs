using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 10f;

    private void Update()
    {
        transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ManeuverZone") )
        {
            other.GetComponent<IActionTrigger>().ActionTrigger();
        }

    }

    /* private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ManeuverZone") )
        {
            other.GetComponent<BoxCollider>().enabled = false;

            if (IKBulletTime != null)
                StopCoroutine(IKBulletTime);

            IKBulletTime = StartCoroutine(TimeManagerForFreeIKObstacle());
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("MatchSilhouette") )
        {
            other.GetComponent<BoxCollider>().enabled = false;

            if (IKBulletTime != null)
                StopCoroutine(IKBulletTime);

            IKBulletTime = StartCoroutine(TimeManagerForMatchSilhouette());
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("MatchGhost")  )
        {
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponent<GhostTrigger>().Ghost.SetActive(true);

            if (IKBulletTime != null)
                StopCoroutine(IKBulletTime);

            IKBulletTime = StartCoroutine(TimeManagerForMatchSilhouette());
        }
    } */



    /* public void OnSilhouetteMatched(string __animationToPlay)
    {
        AnimationToPlay = __animationToPlay;

        silhouetteCounter++;
        if (silhouetteCounter == 5)
        {
            

            animator.Play(AnimationToPlay);

            StopCoroutine(IKBulletTime);

            BulletTimeCam.SetActive(false);
            timeManager.SpeedUp();
            inBulletTime = false;
            silhouetteCounter = 0;

            playerIKManager.HandleIKDisabled();
        }
    } */



    /* IEnumerator TimeManagerForMatchSilhouette()
    {
        timeManager.SlowMo();
        animator.runtimeAnimatorController = SkaterAction;

        //SetFirstTargets();
        //realtimeIKRaycaster.StartIK();
        if (!inBulletTime)
        {
            inBulletTime = true;
            playerIKManager.HandleIKEnabled();
        }

        yield return new WaitForSecondsRealtime(10f);

        BulletTimeCam.SetActive(false);
        timeManager.SpeedUp();
        inBulletTime = false;
        silhouetteCounter = 0;

        playerIKManager.HandleIKDisabled();

        animator.runtimeAnimatorController = SkaterDefault;

    } */

    /* IEnumerator TimeManagerForFreeIKObstacle()
    {
        //berke

        timeManager.SlowMo();

        //SetFirstTargets();
        //realtimeIKRaycaster.StartIK();
        if (!inBulletTime)
        {
            BulletTimeCam.SetActive(true);
            inBulletTime = true;

            playerIKManager.HandleIKEnabled();
        }

        yield return new WaitForSecondsRealtime(3f);

        BulletTimeCam.SetActive(false);
        timeManager.SpeedUp();

        yield return new WaitForSecondsRealtime(1f);

        playerIKManager.HandleIKDisabled();
        inBulletTime = false;
        //realtimeIKRaycaster.StopIK();
    } */
}
