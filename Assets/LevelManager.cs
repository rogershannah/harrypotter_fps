using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int playerSpellDamageAmount = 10;

    public static GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
