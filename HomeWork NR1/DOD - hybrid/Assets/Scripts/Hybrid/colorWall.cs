using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;

// Component to save modified color of the wall for moving sprites
public class colorWall : MonoBehaviour {
    public Color trueColor;
}


class ColorSystem : ComponentSystem
{
    //Variables
    private static float colorMultiplier = 3.0f;       // how much multiplication need to be done

    // struct as a tample for a group of moving sprites
    struct MovingSprites
    {
        public Movement movement;
        public Transform transform;
        public SpriteRenderer spriteRenderer;
    }

    // struct as a tample for a group of wall sprites
    struct WallStripes
    {
        public colorWall colorWall;
        public Transform transform;
        public SpriteRenderer spriteRenderer;
    }

    
    protected override void OnStartRunning()
    { 
        // Get refined colors based on your color
        foreach (WallStripes e in GetEntities<WallStripes>())
        {
            e.colorWall.trueColor = RefineColor(e.spriteRenderer.color);
        }
    }

    protected override void OnUpdate()
    {
        // Paint moving sprites
        foreach (MovingSprites wonderer in GetEntities<MovingSprites>())
        {
            wonderer.spriteRenderer.color = DetirmeneColor(wonderer.transform.position.x);
        }
    }

    
    // method to refine color
    Color RefineColor(Color oldColor)
    {
        //Algorithm for color recalculation
        float newRed = 1.0f - (colorMultiplier * (1.0f - oldColor.r));
        float newGreen = 1.0f - (colorMultiplier * (1.0f - oldColor.g));
        float newBlue = 1.0f - (colorMultiplier * (1.0f - oldColor.b));

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

    //calculate above which wall sprite is other sprites
    Color DetirmeneColor(float coordinates)
    {
        foreach (WallStripes e in GetEntities<WallStripes>())
        {
            // because wall sprites are 1.5f width if you are less then 0.75 way you are above it
            if (Mathf.Abs(coordinates - e.transform.position.x) < 0.75f)
            {
                return e.colorWall.trueColor;
            }
        }
        return new Color(0, 0, 0);
    }
}