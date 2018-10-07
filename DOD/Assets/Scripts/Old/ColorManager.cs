using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    //variables
    
    private SpriteRenderer myRenderer;          // spectrum wall's stripes sprite renderer 
    private Transform myPosition;               // spectrum wall's stripes Transform
    private float colorMultiplier = 3.0f;       // how much multiplication need to be done

    private Color myRefinedColor;                // required to save refined color 


    void Start () {
      
        // get my varaibles
        myRenderer = GetComponent<SpriteRenderer>();
        myPosition = gameObject.transform;

        //get refiend color from my color
        myRefinedColor = RefineColor(myRenderer.color);

	}
	

	void Update () {
        // change color of every sprite that is above me
        ChangeColor(GetAllSpritesToChangeColor(),myRefinedColor);
	}

    // Pallet's Colors are all pale, this function refines them for sprites
    Color RefineColor(Color oldColor)
    {
        //Algorithm for color recalculation
        float newRed   = 1.0f - (colorMultiplier * (1.0f - oldColor.r));    
        float newGreen = 1.0f - (colorMultiplier * (1.0f - oldColor.g));
        float newBlue  = 1.0f - (colorMultiplier * (1.0f - oldColor.b));

        //Normalize Colors values
        if (newRed > 1.0f) newRed = 1.0f;
        if (newRed < 0.0f) newRed = 0.0f;
        if (newGreen > 1.0f) newGreen = 1.0f;
        if (newGreen < 0.0f) newGreen = 0.0f;
        if (newBlue > 1.0f) newBlue = 1.0f;
        if (newBlue < 0.0f) newBlue = 0.0f;

        // Form new color variable
        Color newColor = new Color(newRed, newGreen, newBlue);
        
        // return new Color
        return newColor;
    }

    // get all gameObjects which are above you
    List<GameObject> GetAllSpritesToChangeColor() { 

        // list for AllGameObjects in scene
        List<GameObject> allGameobjects = new List<GameObject>(FindObjectsOfType<GameObject>());

        //list for sprites above this gameObject
        List<GameObject> spritesAboveMe = new List<GameObject>();

        //calculate if gameObject IS above
        foreach (GameObject go in allGameobjects)
        {
            if (go != gameObject && go.GetComponent<SpriteRenderer>())
            {
                if (myPosition.position.x - go.transform.position.x < 0.75f && myPosition.position.x - go.transform.position.x > -0.75f)
                {
                    spritesAboveMe.Add(go);
                }
            }
        }

        // return all gameObjects above me
        return spritesAboveMe;
    }

    // change color for each GameObject above me
    void ChangeColor(List<GameObject> sprites, Color color)
    {
        foreach(GameObject go in sprites)
        {
            SpriteRenderer spritesRenderer = go.GetComponent<SpriteRenderer>();
            spritesRenderer.color = color;
        }
    }

}
