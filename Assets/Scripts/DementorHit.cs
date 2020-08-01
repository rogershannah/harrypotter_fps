using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementorHit : MonoBehaviour
{
    public GameObject dementorExpelled;
    public GameObject greenLootPrefab;
    public GameObject pinkLootPrefab;
    public GameObject yellowLootPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Projectile") || other.CompareTag("Protego"))
        {
            DestroyDementor();
        }
    }

    void DestroyDementor()
    {
        Instantiate(dementorExpelled, transform.position, transform.rotation);

        gameObject.SetActive(false);

        GameObject lootPrefab = lootDrop();

        Instantiate(lootPrefab, transform.position, transform.rotation);

        Destroy(gameObject, 0.5f);
    }

    private GameObject lootDrop()
    {
        int n = Random.Range(1, 4);
        if(n == 1)
        {
            return greenLootPrefab;
        } else if (n == 2)
        {
            return pinkLootPrefab;
        } else
        {
            return yellowLootPrefab;
        }
    }
}
