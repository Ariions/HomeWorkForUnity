using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

// Components for moving sprites
public class Movement : MonoBehaviour {
    public int index;               //Unique index so i can indentify them
    public float speed;             //moving sprites speed
    public Vector3 direction;       //where are you going (left, right)
    public int CollisionsInARow;    //how many timed have you collided in a row of frames
}

class MovementSystem : ComponentSystem
{
    // varaibles
    private static float collideDistance = 0.212f;     // mving sprites width in half

    // struct as a tample for a group of wall sprites
    struct WallStripes
    {
        public colorWall colorWall;
        public Transform transform;
    }

    // struct as a tample for a group of moving sprites
    struct MovingSprites
    {
        public Movement movement;
        public Transform transform;
    }

    protected override void OnStartRunning()
    {
        // randomize starting movement direction
        foreach (MovingSprites e in GetEntities<MovingSprites>())
        {
            if (Random.Range(-1f, 1f) <= 0)
            {
                e.movement.direction *= -1f;
            }
        }
    }

    protected override void OnUpdate()
    {
        foreach (MovingSprites e in GetEntities<MovingSprites>())
        {
            // if it has been stuck for more then 25 frames start increasing translate so eventualy they "pop out" 
            if(e.movement.CollisionsInARow > 25)
                e.transform.Translate(e.movement.direction * e.movement.CollisionsInARow);
            else
                e.transform.Translate(e.movement.direction);

            // update direction if there was alot of collitions before (for dealing with stuck sprites)
            if (e.movement.direction.x != e.movement.speed && e.movement.direction.x != -e.movement.speed)
            {
                if (e.movement.direction.x >= 0) e.movement.direction.x = e.movement.speed;
                else e.movement.direction.x = -e.movement.speed;
            }

            // if you reached side of the sceen change direction
            if (e.transform.position.x < GameManager.leftSideOfScreen && e.movement.direction.x < 0) e.movement.direction *= -1f;
            if (e.transform.position.x > GameManager.rightSideOfScreen && e.movement.direction.x > 0) e.movement.direction *= -1f;
            
            // check if you need to change direction because of collition
            if (Collide(e))
            {
                e.movement.CollisionsInARow++;
            }
            else
                e.movement.CollisionsInARow = 1;
        }
    }
    
    bool Collide(MovingSprites entity)
    {
        foreach (MovingSprites e in GetEntities<MovingSprites>())
        {   // so i dont check same sprite again i do not check sprites that already been check once
            if (e.movement.index > entity.movement.index) {
                if ((e.transform.position.x - entity.transform.position.x <= collideDistance && e.transform.position.x - entity.transform.position.x >= -collideDistance) 
                 && (e.transform.position.y - entity.transform.position.y <= collideDistance && e.transform.position.y - entity.transform.position.y >= -collideDistance))
                {
                    entity.movement.direction *= -1f;   // reverse my direction
                    e.movement.direction *= -1f;        // reverse my direction
                    return true;                        // I have collided
                }
            }
        }
        return false;
    }
}
