using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limbs : MonoBehaviour
{
    public Collider cld;
    public Rigidbody rgbd;

    private void Awake()
    {
        cld = GetComponent<Collider>();
        rgbd = GetComponent<Rigidbody>();
    }
}
