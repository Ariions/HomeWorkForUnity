using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class Movement : MonoBehaviour {
    public int index;
    public float speed;
    public Vector3 direction;
}

class MovementSystem : ComponentSystem
{
    private static float collideDistance = 0.212f;                  // my width half

    struct WallStripes
    {
        public colorWall colorWall;
        public Transform transform;
    }


    struct MovingSprites
    {
        public Movement movement;
        public Transform transform;
    }

    protected override void OnStartRunning()
    {
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
            // move me
            e.transform.Translate(e.movement.direction);

            // update direction if there was alot of collitions before (for dealing with stuck sprites)
            if (e.movement.direction.x != e.movement.speed && e.movement.direction.x != -e.movement.speed)
            {
                if (e.movement.direction.x >= 0) e.movement.direction.x = e.movement.speed;
                else e.movement.direction.x = -e.movement.speed;
            }

            if (e.transform.position.x < GameManager.leftSideOfScreen && e.movement.direction.x < 0) e.movement.direction *= -1f;
            if (e.transform.position.x > GameManager.rightSideOfScreen && e.movement.direction.x > 0) e.movement.direction *= -1f;
            //Debug.Log(e.movement.index);
            Collide(e);
        }
    }
    
    void Collide(MovingSprites entity)
    {

        // for each valid cadidate i check cordinates if we are not colliding
        foreach (MovingSprites e in GetEntities<MovingSprites>())
        {
            if (e.movement.index > entity.movement.index) {
                if ((e.transform.position.x - entity.transform.position.x <= collideDistance && e.transform.position.x - entity.transform.position.x >= -collideDistance) 
                 && (e.transform.position.y - entity.transform.position.y <= collideDistance && e.transform.position.y - entity.transform.position.y >= -collideDistance))
                {
                    entity.movement.direction *= -1f;   // reverse my direction
                    e.movement.direction *= -1f;   // reverse my direction
                    break;  // if i collided then just procede
                }
            }

          
        }
    }
    



}
