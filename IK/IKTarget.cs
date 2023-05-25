using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IKTarget : MonoBehaviour
{
    public Color32 successColor;
    public Gradient gradient;
    public SpriteRenderer GizmoImage;
    [HideInInspector] public Transform targetTransform;
    [HideInInspector] public float maxDistance;
    bool maxDistanceSet = false;

    private void Start()
    {
        //print(maxDistance);
    }

    public void ChangeGizmoColor()
    {
        GizmoImage.color = successColor;
    }

    private void LateUpdate()
    {
        if (targetTransform == null)
            return;
        //float value = Mathf.Lerp(0f, 1f, t);
        //t += Time.deltaTime / duration;
        if (!maxDistanceSet)
        {
            maxDistance = Vector3.Distance(transform.GetChild(0).position, targetTransform.position);
            maxDistanceSet = true;
        }

        float _distance = Vector3.Distance(transform.GetChild(0).position, targetTransform.position);

        float _distanceRatio = 1f - (_distance / maxDistance);

        //print(_distanceRatio);

        Color color = gradient.Evaluate(_distanceRatio);
        GizmoImage.color = color;
    }
}
