using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : CoreComponent
{
    // Singleton instance
    public static CollisionControl Instance;

    // Hareket bile�eni
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    #region Check Transforms

    // Zemin kontrol noktas�
    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }
    // Duvar kontrol noktas�
    public Transform WallCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private set => wallCheck = value;
    }
    // Yatay k�y� kontrol noktas�
    public Transform LedgeCheckHorizontal
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }
    // Dikey k�y� kontrol noktas�
    public Transform LedgeCheckVertical
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
        private set => ledgeCheckVertical = value;
    }
    // Tavan kontrol noktas�
    public Transform CeilingCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
        private set => ceilingCheck = value;
    }

    // Zemin kontrol yar��ap�
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    // Duvar kontrol mesafesi
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    // Zemin katman maskesi
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    // Serialize edilmi� alanlar
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
        Instance = this; // Singleton instance olu�tur
    }

    // Tavan var m� kontrol eder
    public bool Ceiling
    {
        get => Physics.OverlapSphere(CeilingCheck.position, groundCheckRadius, whatIsGround).Length > 0;
    }

    // Zemin var m� kontrol eder
    public bool Ground
    {
        get => Physics.OverlapSphere(GroundCheck.position, groundCheckRadius, whatIsGround).Length > 0;
    }

    // �nde duvar var m� kontrol eder
    public bool WallFront
    {
        get => Physics.Raycast(WallCheck.position, Vector2.right * 1, wallCheckDistance, whatIsGround);
    }

    // Yatay k�y� var m� kontrol eder
    public bool LedgeHorizontal
    {
        get => Physics.Raycast(LedgeCheckHorizontal.position, Vector2.right * 1, wallCheckDistance, whatIsGround);
    }

    // Dikey k�y� var m� kontrol eder
    public bool LedgeVertical
    {
        get => Physics.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
    }

    // Arkada duvar var m� kontrol eder
    public bool WallBack
    {
        get => Physics.Raycast(WallCheck.position, Vector2.right * -1, wallCheckDistance, whatIsGround);
    }

    // Gizmo'lar� �izer
    private void OnDrawGizmos()
    {
        // Zemin kontrol noktas�n� �izer
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);

        // Tavan kontrol noktas�n� �izer
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(CeilingCheck.position, groundCheckRadius);

        // Duvar kontrol noktas�n� �izer
        Gizmos.color = Color.green;
        Gizmos.DrawLine(WallCheck.position + Vector3.right * wallCheckDistance, WallCheck.position - Vector3.right * wallCheckDistance);

        // Yatay k�y� kontrol noktas�n� �izer
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(LedgeCheckHorizontal.position + Vector3.right * wallCheckDistance, LedgeCheckHorizontal.position - Vector3.right * wallCheckDistance);

        // Dikey k�y� kontrol noktas�n� �izer
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(LedgeCheckVertical.position, LedgeCheckVertical.position + Vector3.down * wallCheckDistance);
    }

    // Mant�k g�ncellemesi
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
