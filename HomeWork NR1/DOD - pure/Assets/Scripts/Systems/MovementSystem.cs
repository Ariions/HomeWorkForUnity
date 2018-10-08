using System.Collections.Generic;
using System;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
using Unity.Rendering;
using Unity.Collections;

public class MovementSystem : JobComponentSystem
{
    //private static float collideDistance = 0.212f;               // my width half
    public static float leftSideOfScreen = 0.125f;                 // coordinates of left side of the screen
    public static float rightSideOfScreen = 20.35f;                // coordinates of right side of the screen
    Bootstrap bootstrap;

    // TODO: somehow reach other entities so i could check collition
    public struct SpriteMovementJob : IJobProcessComponentData<Movement, Position>
    {
        public void Execute(ref Movement movement, ref Position postion)
        {       
            postion.Value.x += movement.speed * movement.direction;

            if (postion.Value.x < leftSideOfScreen && movement.direction < 0) movement.direction *= -1f;
            if (postion.Value.x > rightSideOfScreen && movement.direction > 0) movement.direction *= -1f;
        }
    }

    /// <summary>
    /// I could not reach other entities form here
    /// </summary>

    // TOOD: each every moving entity from wall so i would check if it need to get repainted
    private struct SpriteColorJob : IJobProcessComponentData<WallColor, Position>
    {
        public void Execute(ref WallColor wallColor, ref Position position)
        {
            
        }
    }

    //  schedule a job
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new SpriteMovementJob { };

        return job.Schedule(this, inputDeps);
    }



}
