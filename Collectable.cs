using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] GameObject VFX;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            GameObject _VFX = Instantiate(VFX, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.DiamondSFX, 0.1f);

            FindObjectOfType<GemText>().OnGemCollected();
            
            Destroy(_VFX, 3f);
            Destroy(this.gameObject);
        }
    }
}
