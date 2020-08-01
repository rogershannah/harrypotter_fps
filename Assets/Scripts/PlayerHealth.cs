using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip deadSFX;
    public static bool isDead = false;
    public Slider healthSlider;

    int currentHealth;
    void Start()
    {
        if (healthSlider == null)
        {
                healthSlider = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Slider>();
        }
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            PlayerDies();
        }

        Debug.Log("Current health: " + currentHealth);
    }

    public void TakeHealth(int healthAmount)
    {
        if (currentHealth < 100)
        {
            currentHealth += healthAmount;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, 100);
        }

        Debug.Log("Current health with loot: " + currentHealth);
    }

    void PlayerDies()
    {
        isDead = true;
        Debug.Log("Player is dead...");
        AudioSource.PlayClipAtPoint(deadSFX, transform.position);
        transform.Rotate(-90, 0, 0, Space.Self);
    }
}
