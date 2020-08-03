using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject reductoPrefab;

    public float projectileSpeed = 100f;
    public AudioClip spellSFX;
    public Image reticleImage;

    public Color reticleDementorColor;
    public Color reticleReductoColor;

    Color originalReticleColor;
    GameObject currentProjectilePrefab;

    void Start()
    {
        originalReticleColor = reticleImage.color;
        currentProjectilePrefab = projectilePrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuBehavior.isGamePaused)
        {
            if (Input.GetButtonDown("Fire1")) //left mouse/ctrl/fire button on any controller
            {
                GameObject projectile = Instantiate(currentProjectilePrefab, transform.position + transform.forward, transform.rotation) as GameObject;

                Rigidbody rb = projectile.GetComponent<Rigidbody>();

                rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

                projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

                AudioSource.PlayClipAtPoint(spellSFX, transform.position);
            }

            reticleImage.enabled = false;
            ReticleEffect();
        } 
        else
        {
            reticleImage.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        ReticleEffect();
    }

    void ReticleEffect()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) //may want to define public float/int range to limit speel reach
        {
            if(hit.collider.CompareTag("Dementor"))
            {
                currentProjectilePrefab = projectilePrefab;
                reticleImage.color = Color.Lerp(reticleImage.color, reticleDementorColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, .7f, 1), Time.deltaTime * 2);
            }
            else if(hit.collider.CompareTag("Reducto"))
            {
                currentProjectilePrefab = reductoPrefab;
                reticleImage.color = Color.Lerp(reticleImage.color, reticleReductoColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, .7f, 1), Time.deltaTime * 2);
            }
            else
            {
                currentProjectilePrefab = projectilePrefab;
                reticleImage.color = Color.Lerp(reticleImage.color, originalReticleColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
            }
        }
        else
        {
            reticleImage.color = Color.Lerp(reticleImage.color, originalReticleColor, Time.deltaTime * 2);
            reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
        }
    }
}
