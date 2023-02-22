using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpell : MonoBehaviour
{
    public GameObject[] spells;
    public GameObject spellPanel;
    
    int selectedSpell = 0;
    GameObject currentProjectile;
    Button[] btns;


    void Start()
    {
        btns = spellPanel.GetComponentsInChildren<Button>();
        UpdateSpellUI();
    }

    // Update is called once per frame
    void Update()
    {
        int prevSpell = selectedSpell;
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(selectedSpell >= spells.Length - 1)
            {
                selectedSpell = 0;
            } 
            else
            {
                selectedSpell++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedSpell <= 0)
            {
                selectedSpell = spells.Length - 1;
            }
            else
            {
                selectedSpell--;
            }
        }

        currentProjectile = spells[selectedSpell];
        ShootProjectile.currentProjectilePrefab = currentProjectile;

        if(prevSpell != selectedSpell)
        {
            UpdateSpellUI();
            PlayerPrefs.SetInt("selectedSpell", selectedSpell);
        }
    }

    void UpdateSpellUI()
    {
        int i = 0;
        foreach(Button spellIcon in btns)
        {
            if(i == selectedSpell)
            {
                spellIcon.transform.localScale *= 1.25f;
            }
            else
            {
                spellIcon.transform.localScale = new Vector3(1, 1, 1);
            }
            i++;
        }
    }
}
