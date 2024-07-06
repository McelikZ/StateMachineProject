using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : CoreComponent
{
    // Singleton instance
    public static CollisionControl Instance;

    // Hareket bileþeni
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    #region Check Transforms

    // Zemin kontrol noktasý
    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }
    // Duvar kontrol noktasý
    public Transform WallCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private set => wallCheck = value;
    }
    // Yatay kýyý kontrol noktasý
    public Transform LedgeCheckHorizontal
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }
    // Dikey kýyý kontrol noktasý
    public Transform LedgeCheckVertical
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
        private set => ledgeCheckVertical = value;
    }
    // Tavan kontrol noktasý
    public Transform CeilingCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
        private set => ceilingCheck = value;
    }

    // Zemin kontrol yarýçapý
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    // Duvar kontrol mesafesi
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    // Zemin katman maskesi
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    // Serialize edilmiþ alanlar
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;
    [SerializeField] private Transform ceilingCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        Instance = this; // Singleton instance oluþtur
    }

    // Tavan var mý kontrol eder
    public bool Ceiling
    {
        get => Physics.OverlapSphere(CeilingCheck.position, groundCheckRadius, whatIsGround).Length > 0;
    }

    // Zemin var mý kontrol eder
    public bool Ground
    {
        get => Physics.OverlapSphere(GroundCheck.position, groundCheckRadius, whatIsGround).Length > 0;
    }

    // Önde duvar var mý kontrol eder
    public bool WallFront
    {
        get => Physics.Raycast(WallCheck.position, Vector2.right * 1, wallCheckDistance, whatIsGround);
    }

    // Yatay kýyý var mý kontrol eder
    public bool LedgeHorizontal
    {
        get => Physics.Raycast(LedgeCheckHorizontal.position, Vector2.right * 1, wallCheckDistance, whatIsGround);
    }

    // Dikey kýyý var mý kontrol eder
    public bool LedgeVertical
    {
        get => Physics.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
    }

    // Arkada duvar var mý kontrol eder
    public bool WallBack
    {
        get => Physics.Raycast(WallCheck.position, Vector2.right * -1, wallCheckDistance, whatIsGround);
    }

    // Gizmo'larý çizer
    private void OnDrawGizmos()
    {
        // Zemin kontrol noktasýný çizer
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);

        // Tavan kontrol noktasýný çizer
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(CeilingCheck.position, groundCheckRadius);

        // Duvar kontrol noktasýný çizer
        Gizmos.color = Color.green;
        Gizmos.DrawLine(WallCheck.position + Vector3.right * wallCheckDistance, WallCheck.position - Vector3.right * wallCheckDistance);

        // Yatay kýyý kontrol noktasýný çizer
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(LedgeCheckHorizontal.position + Vector3.right * wallCheckDistance, LedgeCheckHorizontal.position - Vector3.right * wallCheckDistance);

        // Dikey kýyý kontrol noktasýný çizer
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(LedgeCheckVertical.position, LedgeCheckVertical.position + Vector3.down * wallCheckDistance);
    }

    // Mantýk güncellemesi
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
