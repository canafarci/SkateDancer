using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlash : MonoBehaviour
{
    public bool isActive;

    CanvasGroup canvasGroup;

    private float speed;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        SetFlashSpeed();
    }

    private void Update()
    {
        if (!isActive) return;

        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0f, speed * Time.deltaTime);

        if (canvasGroup.alpha < 0.04f)
        {
            SetFlashSpeed();
            canvasGroup.alpha = 1f;
        }

    }

    void SetFlashSpeed()
    {
        speed = Random.Range(0.5f, 4f);
    }
}
