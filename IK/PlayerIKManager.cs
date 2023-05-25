using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class PlayerIKManager : MonoBehaviour
{
    [SerializeField] FullBodyBipedIK fullBodyBipedIK;

    [Header("Body Positions For IK")]
    [SerializeField] Transform body, bodyOld;
    [SerializeField] Transform leftHand, leftHandOld;
    [SerializeField] Transform rightHand, rightHandOld;
    [SerializeField] Transform leftFoot, leftFootOld;
    [SerializeField] Transform rightFoot, rightFootOld;
    [SerializeField] Transform newTargetToFollow, EmptyTransform;
    public List<Transform> limbTransforms = new List<Transform>();
    private Coroutine boneWeightsRoutine = null;

    private void Start()
    {
        bodyOld = Instantiate(EmptyTransform, body.position, Quaternion.identity, transform);
        leftHandOld = Instantiate(EmptyTransform, leftHand.position, Quaternion.identity, transform);
        rightHandOld = Instantiate(EmptyTransform, rightHand.position, Quaternion.identity, transform);
        leftFootOld = Instantiate(EmptyTransform, leftFoot.position, Quaternion.identity, transform);
        rightFootOld = Instantiate(EmptyTransform, rightFoot.position, Quaternion.identity, transform);
    }

    public void SetFirstTargets(bool __leftFoot = true, bool __rightFoot = true, bool __body = true,
                                 bool __rightHand = true, bool __leftHand = true)
    {
        if (__body)
            fullBodyBipedIK.solver.bodyEffector.target = body;
        if (__leftHand)
            fullBodyBipedIK.solver.leftHandEffector.target = leftHand;
        if (__rightHand)
            fullBodyBipedIK.solver.rightHandEffector.target = rightHand;
        if (__leftFoot)
            fullBodyBipedIK.solver.leftFootEffector.target = leftFoot;
        if (__rightFoot)
            fullBodyBipedIK.solver.rightFootEffector.target = rightFoot;
    }

    public void HandleNewTargets(bool __leftFoot = true, bool __rightFoot = true, bool __body = true,
                                    bool __rightHand = true, bool __leftHand = true, bool __firstTrigger = false)
    {
        if (__firstTrigger)
        {
            Transform newBodyPos = Instantiate(EmptyTransform, body.position, Quaternion.identity, transform);
            newBodyPos.position = body.position;
            fullBodyBipedIK.solver.bodyEffector.target = newBodyPos.transform;
            limbTransforms.Add(newBodyPos);

            Transform newLeftFootPos = Instantiate(EmptyTransform, leftFoot.position, Quaternion.identity, transform);
            newLeftFootPos.position = leftFoot.position;
            fullBodyBipedIK.solver.leftFootEffector.target = newLeftFootPos.transform;
            limbTransforms.Add(newLeftFootPos);

            Transform newRightFootPos = Instantiate(EmptyTransform, rightFoot.position, Quaternion.identity, transform);
            newRightFootPos.position = rightFoot.position;
            fullBodyBipedIK.solver.rightFootEffector.target = newRightFootPos.transform;
            limbTransforms.Add(newRightFootPos);

            Transform newLeftHandPos = Instantiate(EmptyTransform, leftHand.position, Quaternion.identity, transform);
            newLeftHandPos.position = leftHand.position;
            fullBodyBipedIK.solver.leftHandEffector.target = newLeftHandPos.transform;
            limbTransforms.Add(newLeftHandPos);

            Transform newRightHandPos = Instantiate(EmptyTransform, rightHand.position, Quaternion.identity, transform);
            newRightHandPos.position = rightHand.position;
            fullBodyBipedIK.solver.rightHandEffector.target = newRightHandPos.transform;
            limbTransforms.Add(newRightHandPos);

            return;
        }
        if (__body)
        {
            Transform newBodyPos = Instantiate(newTargetToFollow, body.position, Quaternion.identity, transform);
            newBodyPos.GetComponent<DraggableLimb>().limb = DraggableLimb.Limb.Body;
            fullBodyBipedIK.solver.bodyEffector.target = newBodyPos.transform;
            limbTransforms.Add(newBodyPos);
        }
        else
        {
            fullBodyBipedIK.solver.bodyEffector.target = bodyOld.transform;
        }
        //------- -------------
        if (__leftFoot)
        {
            Transform newLeftFootPos = Instantiate(newTargetToFollow, leftFoot.position, Quaternion.identity, transform);
            newLeftFootPos.GetComponent<DraggableLimb>().limb = DraggableLimb.Limb.LeftFoot;
            fullBodyBipedIK.solver.leftFootEffector.target = newLeftFootPos.transform;
            limbTransforms.Add(newLeftFootPos);
        }
        else
        {
            fullBodyBipedIK.solver.leftFootEffector.target = leftFootOld.transform;
        }
        //-------
        if (__rightFoot)
        {
            Transform newRightFootPos = Instantiate(newTargetToFollow, rightFoot.position, Quaternion.identity, transform);
            newRightFootPos.GetComponent<DraggableLimb>().limb = DraggableLimb.Limb.RightFoot;
            fullBodyBipedIK.solver.rightFootEffector.target = newRightFootPos.transform;
            limbTransforms.Add(newRightFootPos);
        }
        else
        {
            fullBodyBipedIK.solver.rightFootEffector.target = rightFootOld.transform;
        }
        //-------
        if (__leftHand)
        {
            Transform newLeftHandPos = Instantiate(newTargetToFollow, leftHand.position, Quaternion.identity, transform);
            newLeftHandPos.GetComponent<DraggableLimb>().limb = DraggableLimb.Limb.LeftHand;
            fullBodyBipedIK.solver.leftHandEffector.target = newLeftHandPos.transform;
            limbTransforms.Add(newLeftHandPos);
        }
        else
        {
            fullBodyBipedIK.solver.leftHandEffector.target = leftHandOld.transform;
        }
        //-------
        if (__rightHand)
        {
            Transform newRightHandPos = Instantiate(newTargetToFollow, rightHand.position, Quaternion.identity, transform);
            newRightHandPos.GetComponent<DraggableLimb>().limb = DraggableLimb.Limb.RightHand;
            fullBodyBipedIK.solver.rightHandEffector.target = newRightHandPos.transform;
            limbTransforms.Add(newRightHandPos);
        }
        else
        {
            fullBodyBipedIK.solver.rightHandEffector.target = rightHandOld.transform;
        }
    }

    public void EmptyTransformList()
    {
        foreach (Transform _tr in limbTransforms)
        {
            Destroy(_tr.gameObject);
        }

        limbTransforms = new List<Transform>();
    }

    public void HandleIKEnabled(bool __leftFoot = true, bool __rightFoot = true, bool __body = true,
                                    bool __rightHand = true, bool __leftHand = true, bool __firstTrigger = false)
    {
        fullBodyBipedIK.enabled = true;
        /* SetFirstTargets(__leftFoot, __rightFoot, __body, __rightHand, __leftHand); */
        HandleNewTargets(__leftFoot, __rightFoot, __body, __rightHand, __leftHand, __firstTrigger);
    }

    public void HandleIKDisabled()
    {
        fullBodyBipedIK.enabled = false;
        EmptyTransformList();
    }

    public void SetBoneWeights()
    {
        fullBodyBipedIK.solver.leftHandEffector.positionWeight = 1f;
        fullBodyBipedIK.solver.rightHandEffector.positionWeight = 1f;
        fullBodyBipedIK.solver.bodyEffector.positionWeight = 1f;
        fullBodyBipedIK.solver.leftFootEffector.positionWeight = 1f;
        fullBodyBipedIK.solver.rightFootEffector.positionWeight = 1f;
    }

    public IEnumerator LowerBoneWeightsRoutine(float __speed)
    {
        float _speed = __speed * 0.001f;
        while (fullBodyBipedIK.solver.leftHandEffector.positionWeight > 0.01f)
        {
            fullBodyBipedIK.solver.leftHandEffector.positionWeight -= 0.01f;
            fullBodyBipedIK.solver.rightHandEffector.positionWeight -= 0.01f;
            fullBodyBipedIK.solver.bodyEffector.positionWeight -= 0.01f;
            fullBodyBipedIK.solver.leftFootEffector.positionWeight -= 0.01f;
            fullBodyBipedIK.solver.rightFootEffector.positionWeight -= 0.01f;
            yield return new WaitForSecondsRealtime(_speed);
        }

    }
    public Transform GetOldTransforms(DraggableLimb.Limb __limb)
    {
        switch (__limb)
        {
            case (DraggableLimb.Limb.Body):
                return bodyOld;
            case (DraggableLimb.Limb.LeftFoot):
                return leftFootOld;
            case (DraggableLimb.Limb.RightFoot):
                return rightFootOld;
            case (DraggableLimb.Limb.LeftHand):
                return leftHandOld;
            case (DraggableLimb.Limb.RightHand):
                return rightHandOld;

            default:
                return null;
        }
    }

    public void ResetOldTransforms()
    {
        bodyOld.transform.position = body.transform.position;
        leftFootOld.transform.position = leftFoot.transform.position;
        rightFootOld.transform.position = rightFoot.transform.position;
        leftHandOld.transform.position = leftHand.transform.position;
        rightHandOld.transform.position = rightHand.transform.position;
    }
}
