using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtegoEntered : MonoBehaviour
{
    public AudioClip protegoSFX;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Dementor"))
        {
            AudioSource.PlayClipAtPoint(protegoSFX, transform.position);
        }
    }
}
