using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSpawner : MonoBehaviour
{
    [Header("Spawner")]
    public int columns = 5;
    [Range(0,360)]
    public int spread = 360; // max angle between first and last column. 360 = max
    public float centerDist = 0;
    public float spinSpeed = 0;
    
    [Header("Bullet")]
    public float speed = 1;
    public float lifetime = 5;
    public float firerate = 1;
    public float size = 1;
    public Color color;
    public Sprite sprite;
    public Material mtl;
    
    
    private float angle; // angle between columns
    private float spinTimer;
    
    
    public ParticleSystem system;

    void Awake() {
        Summon();
    }

    void FixedUpdate()
    {
        spinTimer += Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0,0,spinTimer * spinSpeed);
    }

    void Summon() {
        angle = spread * 1f / columns;
        float offset = -spread / 2f * (angle == 360 ? 0 : 1);
        for (int i = 0; i < columns; ++i)
        {
            // A simple particle material with no texture.
            Material particleMaterial = mtl;

            // Create a green Particle System.
            var go = new GameObject("Particle System");
            go.transform.Rotate(angle*i + offset, 90, 0); // Rotate so the system emits upwards.
            go.transform.parent = transform;
            go.transform.position = transform.position + (go.transform.forward * centerDist);
            system = go.AddComponent<ParticleSystem>();
            go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            var mainModule = system.main;
            mainModule.startColor = Color.green;
            mainModule.startSize = size;
            mainModule.startSpeed = speed;
            mainModule.maxParticles = 100000;
            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;

            var emission = system.emission;
            emission.enabled = false;

            var shape = system.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Sprite;
            shape.sprite = null;

            var text = system.textureSheetAnimation;
            text.enabled = true;
            text.mode = ParticleSystemAnimationMode.Sprites;
            text.AddSprite(sprite);

            var coll = system.collision;
            coll.enabled = true;
            coll.sendCollisionMessages = true;
            coll.type = ParticleSystemCollisionType.World;
            coll.mode = ParticleSystemCollisionMode.Collision2D;
            coll.bounce = 0;
            coll.lifetimeLoss = 1;
        }

        // Every 2 secs we will emit.
        InvokeRepeating("DoEmit", 0, firerate);
    }

    void DoEmit() {
        foreach (Transform child in transform) {
            system = child.GetComponent<ParticleSystem>();
            // Any parameters we assign in emitParams will override the current system's when we call Emit.
            // Here we will override the start color and size.
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startColor = color;
            emitParams.startSize = size;
            emitParams.startLifetime = lifetime;
            
            system.Emit(emitParams, 10);
        }
    }
}
