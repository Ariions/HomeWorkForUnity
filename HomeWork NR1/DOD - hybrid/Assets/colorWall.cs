﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;

public class colorWall : MonoBehaviour {

    public Color wallColor;
}

class ColorSystem : ComponentSystem
{

    private static float colorMultiplier = 3.0f;       // how much multiplication need to be done

    struct MovingSprites
    {
        public Movement movement;
        public Transform transform;
        public SpriteRenderer spriteRenderer;
    }

    struct WallStripes
    {
        public colorWall colorWall;
        public Transform transform;
        public SpriteRenderer spriteRenderer;
    }


    protected override void OnStartRunning()
    {
        foreach (WallStripes e in GetEntities<WallStripes>())
        {
            e.colorWall.wallColor = RefineColor(e.spriteRenderer.color);
        }
    }

    protected override void OnUpdate()
    {
        foreach (WallStripes wall in GetEntities<WallStripes>())
        {
            foreach (MovingSprites wonderer in GetEntities<MovingSprites>())
            {
                if (wall.transform.position.x - wonderer.transform.position.x < 0.75f && wall.transform.position.x - wonderer.transform.position.x > -0.75f)
                {
                    wonderer.spriteRenderer.color = wall.colorWall.wallColor;
                }
            }
        }

        }

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

}