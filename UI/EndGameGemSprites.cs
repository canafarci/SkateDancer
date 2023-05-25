using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameGemSprites : MonoBehaviour
{
    Transform endGemPos;
    GemText gemText;

    private void Awake()
    {
        endGemPos = FindObjectOfType<EndGameGemPos>().transform;
        gemText = FindObjectOfType<GemText>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endGemPos.position, Time.deltaTime * 600);
        if (transform.position == endGemPos.position)
        {
            gemText.OnGemCollected();
            Destroy(gameObject);
        }
    }
}
