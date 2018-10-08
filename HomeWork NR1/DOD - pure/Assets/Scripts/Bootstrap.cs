using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Collections;

public class Bootstrap : MonoBehaviour {

    #region Variables

    public float thisSpeed;         //how fast are sprites moving
    public Mesh mySpritesMesh;      //mesh needed to render sprites
    public Material material;       //sprites material
    public float direction;         //in which direction are sprites moving (left, right)
    public int numbOfSprites = 1;   //number of sprites to spawn
    public Material[] materials;    //materials for spectrum wall

    private static float colorMultiplier = 1.5f;                    // multiplier to make colors more vivid
    private static Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);   //scale multipliers for moving sprites
    private static Vector3 wallScale = new Vector3(1.5f, 10f, 0.1f);//scale multipliers for spectrum wall sprites

    private static float leftSideOfScreen = 0.125f;                 // value for left side of the screen
    private static float rightSideOfScreen = 20.35f;                // value for right side of the screen
    private static float topSideOfScreen = 4.74f;                   // value for top side of the screen
    private static float bottomSideOfScreen = -4.74f;               // value for bottom side of the screen


    private static EntityArchetype movingSpritesArchtype;

    public static Entity[] spriteEntity;
    private static Color[] colors;


    #endregion

    private void Start()
    {
        // create entity manager
        EntityManager entityManager = World.Active.GetOrCreateManager<EntityManager>();

        movingSpritesArchtype = entityManager.CreateArchetype(typeof(Movement), typeof(Position), typeof(Scale), typeof(MeshInstanceRenderer));

        spriteEntity = new Entity[numbOfSprites];
        colors = new Color[14];

        for (int i = 0; i < 14; i++)
            colors[i] = RefineColor(materials[i].color);


        #region movingSprites

        // spawn wanted number of sprites
        for (int i = 0; i < numbOfSprites; i++)
        {
            // randomize in which direction they are going
            if (Random.Range(-1f, 1f) <= 0)
            {
                direction = -1f;
            }
            else
                direction = 1f;

            // create entity
            spriteEntity[i] = entityManager.CreateEntity(movingSpritesArchtype);

            // set its position somewhere in the screen
            entityManager.SetComponentData(spriteEntity[i], new Position
            {
                Value = new Vector3(Random.Range(leftSideOfScreen, rightSideOfScreen), Random.Range(bottomSideOfScreen, topSideOfScreen))
            });

            // set its speed and give it direction
            entityManager.SetComponentData(spriteEntity[i], new Movement
            {
                speed = thisSpeed,
                direction = direction
            });

            // give it mesh and material so it can be randered
            entityManager.SetSharedComponentData(spriteEntity[i], new MeshInstanceRenderer
            {
                mesh = mySpritesMesh,
                material = material
            });

            // set scale
            entityManager.SetComponentData(spriteEntity[i], new Scale {
                Value = scale
            });

        }
        #endregion

        #region Spectrum wall

        // offset for spawning spectrum wall in one line
        float xOffset = 0.5f;
        
        // it consists of 14 colors
        for (int i = 0; i < 14; i++)
        {
            // spawn one wall
            Entity SpectrumWall = entityManager.CreateEntity(
                ComponentType.Create<WallColor>(),
                ComponentType.Create<Position>(),
                ComponentType.Create<Scale>(),
                ComponentType.Create<MeshInstanceRenderer>());

            // TODO: for now this is useless, fix it
           

            // set its location
            entityManager.SetComponentData(SpectrumWall, new Position
            {
                Value = new Vector3(xOffset,0)
            });

            // give it mech and material
            entityManager.SetSharedComponentData(SpectrumWall, new MeshInstanceRenderer
            {
                mesh = mySpritesMesh,
                material = materials[i]
            });

            entityManager.SetComponentData(SpectrumWall, new WallColor
            {
                myColor = colors[i]  //new Color(0.0f, 1.0f, 1.0f)
            });

            // set its scale
            entityManager.SetComponentData(SpectrumWall, new Scale
            {
                Value = wallScale
            });

            // incriment offset
            xOffset += 1.5f;
        }
        #endregion
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
