using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

public class RealtimeIKRaycaster : MonoBehaviour
{
    public LayerMask StartLayerMask, DragLayerMask;
    public FullBodyBipedIK ik;
    private bool draggingLimb = false;
    private IKEffector draggedLimb;
    private GameObject boxColliderObject;
    private Transform dragTransform, oldDragTransform;

    PlayerIKManager playerIKManager;

    private void Awake() {
        playerIKManager = FindObjectOfType<PlayerIKManager>();
    }

    private void Update() 
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(_ray, out _hit, Mathf.Infinity, StartLayerMask))
        {
            //set bools and get which limb is dragged
            draggingLimb = true;

            //print(_hit.collider.transform.gameObject.name);

            DraggableLimb.Limb _draggedLimbEnum = _hit.collider.transform.GetComponent<DraggableLimb>().limb;
            dragTransform = _hit.collider.transform;
            oldDragTransform = playerIKManager.GetOldTransforms(_draggedLimbEnum);
            draggedLimb = GetDraggedLimb(_draggedLimbEnum);

            //create dragging box collider
            boxColliderObject = new GameObject();
            boxColliderObject.transform.position = _hit.collider.transform.position;
            boxColliderObject.transform.localScale = new Vector3(10f, 10f, 0.05f);

            boxColliderObject.AddComponent<BoxCollider>();
            boxColliderObject.GetComponent<BoxCollider>().isTrigger = true;
            boxColliderObject.layer = LayerMask.NameToLayer("DragLayer");
            boxColliderObject.transform.LookAt(Camera.main.transform);
        }

        if (draggingLimb && Physics.Raycast(_ray, out _hit, Mathf.Infinity, DragLayerMask))
        {
            //boxColliderObject.transform.LookAt(Camera.main.transform);
            //print(draggedLimb);

            if (dragTransform != null) 
            {
                dragTransform.position = _hit.point;
                oldDragTransform.position = _hit.point;;
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            draggingLimb = false;
            draggedLimb = null;
            Destroy(boxColliderObject);
        }
    }

    private IKEffector GetDraggedLimb(DraggableLimb.Limb __limb)
    {
        switch (__limb)
        {
            case(DraggableLimb.Limb.LeftHand):
                return ik.solver.leftHandEffector;
            case(DraggableLimb.Limb.RightHand):
                return ik.solver.rightHandEffector;
            case(DraggableLimb.Limb.LeftFoot):
                return ik.solver.leftFootEffector;
            case(DraggableLimb.Limb.RightFoot):
                return ik.solver.rightFootEffector;
            case(DraggableLimb.Limb.Body):
                return ik.solver.bodyEffector;
            default:
                return ik.solver.leftHandEffector;
        }
        
    } 
}
