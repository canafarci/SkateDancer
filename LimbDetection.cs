using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbDetection : MonoBehaviour
{
    [SerializeField] Animator animator;
    Rigidbody rgbd;
    Collider cld;
    public Limbs[] ragdollLimbs;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
        cld = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Obstacle")) 
        {
            Time.timeScale = 1;
            foreach (Limbs limb in ragdollLimbs) 
            {
                limb.rgbd.isKinematic = false;
                limb.rgbd.useGravity = true;
                limb.cld.isTrigger = false;
            }
            cld.isTrigger = false;
            rgbd.isKinematic = false;
            rgbd.useGravity = true;

            animator.runtimeAnimatorController = null;
        }
    }

}
