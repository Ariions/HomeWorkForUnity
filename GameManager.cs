using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //variables
    public GameObject wondererPrefab;       // prejab to spawn
    public int howMany;                     // how many to spawn

    private void Start()
    {
        // if value is not valid spawn 100
        if (howMany <= 0) howMany = 100;    
        
        // spwan them alll
        spawnWonderers(howMany);      
    }

    // funtion for spawning them
    void spawnWonderers(int number)
    {
        // spawn them all in range of screen and with no perent
        for (int i = 0; i < number; i++)
        {
            Instantiate(wondererPrefab, new Vector3(Random.Range(0.125f, 20.35f), Random.Range(-4.74f, 4.74f)) , Quaternion.Euler(new Vector3(0, 0, 0)), null);
        }
    }
}
