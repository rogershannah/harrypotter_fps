using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject cratePieces;
    public float breakForce = 100f;
    public float spreadAmount = 5f; 
   private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("ReductoProjectile"))
        {
            Transform currentCrate = gameObject.transform;

            GameObject pieces = Instantiate(cratePieces, currentCrate.position, currentCrate.rotation);


            Rigidbody[] rbPieces = pieces.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rb in rbPieces)
            {
                rb.AddExplosionForce(breakForce, currentCrate.position, spreadAmount);
            }

            Destroy(gameObject);

        }
    }
}
