using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBulletSpawner : MonoBehaviour {
    [Header("Spawner")] 
    private WeaponSO w;

    private List<ParticleSystem> columnList = new List<ParticleSystem>();
    
    private float angle; // angle between columns
    private float centerOffset; // angle offset so the center of spray is upwards
    private float spinTimer;
    private bool doEmit = false; // bool to check if emit is ran once
    
    private ParticleSystem system;

    int emitCount;

    public int EmitAmount
    {
        get { return emitCount; }
    }
    
    
    public bool IsEmitting
    {
        get { return doEmit; }
    }

    public void EmitOnce()
    {
        DoEmit();
    }

    public void ToggleEmit(bool toggleOn) {
        if (toggleOn) {
            StartInvoke();
        } else {
            StopInvoke();
        }
    }
    
    
    // Sets all variables, then initializes spawner
    public void WeaponInit(WeaponSO w)
    {
        this.w = w;
        Summon();
    }

    void Summon() {
        // Calculate angle & offset differently if spread is 360 or not.
        if (w.spread == 0)
        {
            angle = 0;
            centerOffset = -90;
        }
        else if (w.spread == 360) {
            angle = (w.spread) * 1f / w.columns;
            centerOffset = 0;
        }
        else {
            angle = (w.spread) * 1f / (w.columns-1);
            centerOffset = -90 - w.spread / 2f;
        }
        
        // Create Columns
        int i;
        for (i=0; i < w.columns; ++i)
        {
            // Create a green Particle System.
            system = GetColumn(i);
            var go = system.gameObject;
            go.transform.rotation = transform.rotation;
            go.transform.Rotate(angle*i + centerOffset, 90, 0); // Rotate so the system emits upwards.
            go.transform.parent = transform;
            go.transform.position = transform.position + (go.transform.forward * w.centerDist);

            // Set values
            go.GetComponent<ParticleSystemRenderer>().material = w.mtl;
            var mainModule = system.main;
            mainModule.startColor = w.color;
            mainModule.startSize = w.size;
            mainModule.startSpeed = w.speed;
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
            renderer.sortingLayerName = "Bullets";

            var emission = system.emission;
            emission.enabled = false;

            var shape = system.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Sprite;
            shape.sprite = null;

            var text = system.textureSheetAnimation;
            text.enabled = true;
            text.mode = ParticleSystemAnimationMode.Sprites;
            text.AddSprite(w.sprite);

            var coll = system.collision;
            coll.enabled = true;
            coll.sendCollisionMessages = true;
            coll.type = ParticleSystemCollisionType.World;
            coll.mode = ParticleSystemCollisionMode.Collision2D;
            coll.bounce = 0;
            coll.lifetimeLoss = 1;

            // 191 specifically makes sure player does not collide with particles
            coll.collidesWith = 191;

            system.Play();
        }

        DisableUnusedColumns(i);
    }

    // Stop emission of particle systems.
    void StopInvoke() {
        if (doEmit) {
            CancelInvoke();
            doEmit = false;
            emitCount = 0;
        }
    }
    
    // Starts the emission of the particle systems.
    void StartInvoke() {
        InvokeRepeating("DoEmit", 0, w.fireRate);
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
            emitParams.startColor = w.color;
            emitParams.startSize = w.size;
            emitParams.startLifetime = w.lifetime;

            system.Emit(emitParams, 10);
        }
        ++emitCount;
    }
}
