using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Movement : CoreComponent
{
    internal Rigidbody RB { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        RB = GetComponentInParent<Rigidbody>();
    }
    internal void SetVelocity(Vector3 vector3)
    {
        RB.velocity = vector3;
    }
    internal void SetVelocitY(Vector2 vector2)
    {
        RB.velocity = vector2;
    }
}
