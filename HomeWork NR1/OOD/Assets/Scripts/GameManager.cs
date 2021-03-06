﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //variables
    public GameObject wondererPrefab;       // prejab to spawn
    public int howMany;                     // how many to spawn
    public float leftSideOfScreen = 0.125f;                 // coordinates of left side of the screen
    public float rightSideOfScreen = 20.35f;                // coordinates of right side of the screen
    public float topSideOfScreen = 4.74f;                // coordinates of right side of the screen
    public float bottomSideOfScreen = -4.74f;                // coordinates of right side of the screen

    private void Start()
    {
        // if value is not valid spawn 100
        if (howMany <= 0) howMany = 100;    
        
        // spwan them alll
        spawnWonderers(howMany);      
    }

    // funtion for spawning sprites
    void spawnWonderers(int number)
    {
        // spawn them all in range of screen and with no perent
        for (int i = 0; i < number; i++)
        {
            // spawn spirete somewhere inside the screen with 0 0 0 rotation and no perent
            Instantiate(wondererPrefab, new Vector3(Random.Range(leftSideOfScreen, rightSideOfScreen), Random.Range(bottomSideOfScreen, topSideOfScreen)) , Quaternion.Euler(new Vector3(0, 0, 0)), null);
        }
    }
}
