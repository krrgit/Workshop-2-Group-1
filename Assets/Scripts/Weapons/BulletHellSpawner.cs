/*
 * How to Use:
 * First call Initialize()
 * Call ToggleEmit(bool) When you would like to start/stop emitting bullets
 * Call DoEmitOnce() When you would like to emit once
 * Call LoadSo(BulletHellSpawner bhs) when you would like to load a scriptable object
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSpawner : MonoBehaviour {
    [Header("Spawner")]
    [Range(1,25)]
    public int columns = 5;
    [Range(0,360)]
    public int spread = 360; // max angle between first and last column. 360 = max
    [Range(-180,180)]
    public float spinSpeed = 0;
    public float centerDist = 0;
    public float fireRate = 1;
    
    [Header("Bullet")]
    public float speed = 1;
    public float lifetime = 5;
    public float size = 1;
    public Color color;
    public Sprite sprite;
    public Material mtl;

    private List<ParticleSystem> columnList = new List<ParticleSystem>();
    
    private float angle; // angle between columns
    private float centerOffset; // angle offset so the center of spray is upwards
    private float spinTimer;
    private bool doEmit = false; // bool to check if emit is ran once
    
    private ParticleSystem system;
    
    public bool IsEmitting
    {
        get { return doEmit; }
    }

    public void EmitOnce()
    {
        DoEmit();
    }

    public void Initialize()
    {
        Summon();
    }

    public void LoadSO(BulletHellSpawner bhs)
    {
        columns = bhs.columns;
        spread = bhs.spread;
        spinSpeed = bhs.spinSpeed;
        centerDist = bhs.centerDist;
        fireRate = bhs.fireRate;
    
        speed = bhs.speed;
        lifetime = bhs.lifetime;
        size = bhs.size;
        color = bhs.color;
        sprite = bhs.sprite;
        mtl = bhs.mtl;
        
        UpdateColumns();
    }

    public void ToggleEmit(bool toggleOn) {
        if (toggleOn) {
            StartInvoke();
        } else {
            StopInvoke();
        }
    }
    
    void FixedUpdate()
    {
        SpinSpawner();
    }

    void SpinSpawner()
    {
        if (spinSpeed == 0) return;
        spinTimer += Time.fixedDeltaTime;
        transform.rotation *= Quaternion.Euler(0,0,spinTimer * spinSpeed);
    }

    // Function called by other classes to update the spawner
    public void UpdateColumns()
    {
        StopInvoke(); // stop emission before change particle systems
        Summon(); // update particle systems
        StartInvoke(); // start emission
    }

    void Summon() {
        // Calculate angle & offset differently if spread is 360 or not.
        if (spread == 0)
        {
            angle = 0;
            centerOffset = -90;
        }
        else if (spread == 360) {
            angle = (spread) * 1f / columns;
            centerOffset = 0;
        }
        else {
            angle = (spread) * 1f / (columns-1);
            centerOffset = -90 - spread / 2f;
        }
        
        // Create Columns
        int i;
        for (i=0; i < columns; ++i)
        {
            // A simple particle material with no texture.
            Material particleMaterial = mtl;

            // Create a green Particle System.
            system = GetColumn(i);
            var go = system.gameObject;
            go.transform.rotation = transform.rotation;
            go.transform.Rotate(angle*i + centerOffset, 90, 0); // Rotate so the system emits upwards.
            go.transform.parent = transform;
            go.transform.position = transform.position + (go.transform.forward * centerDist);

            // Set values
            go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            var mainModule = system.main;
            mainModule.startColor = Color.green;
            mainModule.startSize = size;
            mainModule.startSpeed = speed;
            mainModule.maxParticles = 10000;
            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
            
            // Make particles align with velocity
            mainModule.startRotation3D = true;
            var curve = new ParticleSystem.MinMaxCurve();
            curve.mode = ParticleSystemCurveMode.Constant;
            curve.constant = Mathf.PI/2f;
            mainModule.startRotationX = curve;
            mainModule.startRotationY = curve;
            
            var renderer = system.GetComponent<ParticleSystemRenderer>();
            renderer.alignment = ParticleSystemRenderSpace.Velocity;

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
            
            
            
            system.Play();
        }

        DisableUnusedColumns(i);
    }

    // Stop emission of particle systems.
    void StopInvoke() {
        if (doEmit) {
            CancelInvoke();
            doEmit = false;
        }
    }
    
    // Starts the emission of the particle systems.
    void StartInvoke() {
        InvokeRepeating("DoEmit", 0, fireRate);
        doEmit = true;
    }

    ParticleSystem GetColumn(int i) {
        if (i < columnList.Count) {
            columnList[i].gameObject.SetActive(true);
            columnList[i].transform.rotation = transform.rotation;
            return columnList[i];
        }
        
        var go = new GameObject("Particle System");
        var sys = go.AddComponent<ParticleSystem>();
        columnList.Add(sys);
        return sys;
    }

    void DisableUnusedColumns(int i) {
        if (i >= columnList.Count) { return; }

        for (; i < columnList.Count; ++i)
        {
            columnList[i].gameObject.SetActive(false);
        }
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
