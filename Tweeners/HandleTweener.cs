using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandleTweener : MonoBehaviour
{

    private float scale = 0.4f;
    private void Start() {
        transform.DOPunchScale(new Vector3(scale,scale, scale), 0.03f, 2);
    }

    private void OnDisable() {
        transform.localScale = Vector3.one;
    }
}
