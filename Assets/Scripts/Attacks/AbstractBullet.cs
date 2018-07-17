using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBullet : MonoBehaviour
{
    public GameObject Owner;
    public uint BulletDamage = 10;
    public float BulletSpeed = 5.0f;
    public float LifeTime = 3.0f;
    public float BulletCoolDown = 1.0f;

    public enum TriggerTag
    {
        Player,
        Enemy,
        StaticObjects
    }
    public TriggerTag IgnoreTrigger;

    public enum Target
    {
        PlayerForward,
        MousePosition
    }
    public Target AimType;

    public LayerMask HurtableLayerMask;

    // ===================================
    //    MUST IMPLEMENT THESE METHODS
    // ===================================
    protected abstract void CalculateTrajectory();
    protected abstract void CalculateMousePosDirection();
}
