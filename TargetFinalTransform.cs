using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinalTransform : MonoBehaviour
{
    GameObject player;
    IKTarget _ikTarget;
    Transform childTransform;

    Vector3 initialDifference;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        childTransform = transform.GetChild(0);
    }

    private void OnEnable()
    {
        _ikTarget = FindObjectOfType<IKTarget>();
        _ikTarget.targetTransform = transform.GetChild(0).transform;

        initialDifference = transform.position - player.transform.position;

        /* if (childTransform.position.y < 0.7) 
        {
            childTransform.position = new Vector3(childTransform.position.x,0.7f,childTransform.position.z);
        } */
        //print(Vector3.Distance(transform.position, _ikTarget.transform.position));
    }


    /* private void Update()
    {
        transform.LookAt(Camera.main.transform);

        transform.rotation = player.transform.rotation;

        transform.position += initialDifference;
    } */
}
