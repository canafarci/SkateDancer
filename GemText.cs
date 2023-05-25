using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemText : MonoBehaviour
{
    private TextMeshProUGUI text;
    [HideInInspector] public int GemCount = 0;

    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnGemCollected()
    {
        GemCount++;
        text.text = GemCount.ToString();
    }
}
