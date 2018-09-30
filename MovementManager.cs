using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    //variables
    private Transform myPosition;                           // for my transform reference
    private Vector3 direction = new Vector3 (0.01f, 0.0f);  // direction in which to go
    private float collideDistance = 0.212f;                 // my width half
    public int numOfCollisions = 0;                         // speed multiplier for stuck sprites

    // Use this for initialization
    void Start () {

        // get my transform
        myPosition = gameObject.transform;

        // randomize movement start direction
        if(Random.Range(-1f, 1f) <= 0)
        {
            direction *= -1f; 
        }
	}
	
	// Update is called once per frame
	void Update () {

        // move me
        myPosition.Translate(direction);

        // update direction if there was alot of collitions before (for dealing with stuck sprites)
        if (direction.x != 0.01f && direction.x != -0.01f) {
            if (direction.x >= 0) direction.x = 0.01f;
            else direction.x = -0.01f;
        }

        // reverse dirrection if reached the end of the screen
        if (myPosition.position.x < 0.125f && direction.x < 0) direction *= -1f;
        if(myPosition.position.x > 20.35f && direction.x > 0) direction *= -1f;

        // check for collitions
        Collide();


    }


    void Collide()
    {
        // has collided ?
        bool collided = false;

        // lists for all gameobjects
        List<GameObject> allGos = new List<GameObject>(FindObjectsOfType<GameObject>());
        List<GameObject> otherWonderers = new List<GameObject>();

        // checking if these game object are Movement Manager's and if it is not me and from all that creating new list
        foreach(GameObject go in allGos)
        {
            if (go != gameObject && go.GetComponent<MovementManager>())
            {
                otherWonderers.Add(go);
            }
        }

        // for each valid cadidate i check cordinates if we are not colliding
        foreach(GameObject go in otherWonderers)
        {
            if ((go.transform.position.x - myPosition.position.x <= collideDistance && go.transform.position.x - myPosition.position.x >= -collideDistance) && (go.transform.position.y - myPosition.position.y <= collideDistance && go.transform.position.y - myPosition.position.y >= -collideDistance))
            {
                collided = true;    // we collided
                numOfCollisions++;  // + 1 to that many times in a row
                direction *= -1f;   // reverse my direction
                if (numOfCollisions > 25f) direction *= numOfCollisions;    // if we collided enouth times just "teleport" me out of here
                break;  // if i collided then just procede
            }
        }

        // if i havent collided reset "collitions in a row" variable
        if(!collided) numOfCollisions = 0;

    }


}
