using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpell : MonoBehaviour
{
    public GameObject[] spells;
    public GameObject spellPanel;
    
    int selectedtSpell = 0;
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
        int prevSpell = selectedtSpell;
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(selectedtSpell >= spells.Length - 1)
            {
                selectedtSpell = 0;
            } 
            else
            {
                selectedtSpell++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedtSpell <= 0)
            {
                selectedtSpell = spells.Length - 1;
            }
            else
            {
                selectedtSpell--;
            }
        }

        currentProjectile = spells[selectedtSpell];
        ShootProjectile.currentProjectilePrefab = currentProjectile;

        if(prevSpell != selectedtSpell)
        {
            UpdateSpellUI();
        }
    }

    void UpdateSpellUI()
    {
        int i = 0;
        foreach(Button spellIcon in btns)
        {
            if(i == selectedtSpell)
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
