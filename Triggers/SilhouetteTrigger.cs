using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteTrigger : MonoBehaviour
{

    public DraggableLimb.Limb limbType;
    private Collider colliderr;
    private IKTarget iKTarget;

    private void Awake() {
        colliderr = GetComponent<Collider>();
    }
    private void LateUpdate() {
        if (iKTarget == null)
            return;
        
        transform.GetChild(0).transform.LookAt(Camera.main.transform);
        if (!transform.GetChild(0).gameObject.activeSelf)
            this.enabled = false;
    }

    public void OnTriggerActive() 
    {
        iKTarget = FindObjectOfType<IKTarget>();

        Vector3 _ikTargetLocation = iKTarget.transform.position;
        Vector3 _closestPoint = colliderr.ClosestPoint(_ikTargetLocation);
        transform.GetChild(0).transform.position = _closestPoint;        
        //transform.GetChild(0).transform.LookAt(Camera.main.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DraggableLimb") 
                    && other.GetComponent<DraggableLimb>().limb == limbType)
        {
            StartCoroutine(DelayedTrigger(other));
        }
    }

    IEnumerator DelayedTrigger(Collider other)
    {
        other.gameObject.GetComponent<IKTarget>().ChangeGizmoColor();

        yield return new WaitForSecondsRealtime(0.2f);

        GetComponent<CapsuleCollider>().enabled = false;
        transform.GetComponentInParent<SequentialIKTrigger>().OnDragSuccessful();
        //playerMovement.OnSilhouetteMatched(animationName);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DraggableLimb") 
                        && other.GetComponent<DraggableLimb>().limb == limbType)
        {
            //playerMovement.silhouetteCounter--;
            //print(playerMovement.silhouetteCounter);
        }
    } 
}
