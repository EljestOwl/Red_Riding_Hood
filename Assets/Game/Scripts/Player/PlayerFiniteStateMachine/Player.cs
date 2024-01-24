using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region State Variables
    public PlayerStateMachine StateMachine {  get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrab WallGrabState { get; private set; }
    public PlayerWallClimb WallClimbState { get; private set; }


    [SerializeField] private PlayerData _PlayerData;
    #endregion

    #region Components
    public Animator Animator {  get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D rb2D { get; private set; }
    #endregion

    #region Check Transforms

    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Transform _wallCheckPoint;

    #endregion

    #region Other Variables
    public int FacingDiraction {  get; private set; }
    public Vector2 curretVelocity { get; private set; }

    private Vector2 _workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, _PlayerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, _PlayerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, _PlayerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, _PlayerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, _PlayerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, _PlayerData, "wallSlide");
        WallGrabState = new PlayerWallGrab(this, StateMachine, _PlayerData, "wallGrab");
        WallClimbState = new PlayerWallClimb(this, StateMachine, _PlayerData, "wallClimb");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        rb2D = GetComponent<Rigidbody2D>();

        FacingDiraction = 1;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        curretVelocity = rb2D.velocity;
        StateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.currentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, curretVelocity.y);
        rb2D.velocity = _workspace;
        curretVelocity = _workspace;
    }

    public void SetVelocityY(float velocity)
    {
        _workspace.Set(curretVelocity.x, velocity);
        rb2D.velocity = _workspace;
        curretVelocity = _workspace;
    }
    #endregion

    #region Check Functions
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheckPoint.position, _PlayerData.groundCheckRadius, _PlayerData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_wallCheckPoint.position,Vector2.right*FacingDiraction, _PlayerData.wallCheckDistance, _PlayerData.whatIsGround);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDiraction)
        {
            Flip();
        }
    }
    #endregion

    #region Other Funktions
    private void AnimationTrigger() => StateMachine.currentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.currentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDiraction *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        // Draw GroundCheck:
        Gizmos.DrawWireSphere(_groundCheckPoint.position, _PlayerData.groundCheckRadius);

        // Draw WallCheck:
        Gizmos.DrawLine(_wallCheckPoint.position, new Vector3(_wallCheckPoint.position.x + (_PlayerData.wallCheckDistance * FacingDiraction), _wallCheckPoint.position.y, 0));
    }
    #endregion

}
