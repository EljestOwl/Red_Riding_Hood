using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region State Variables
    public PlayerStateMachine StateMachine {  get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    [SerializeField] private PlayerData _PlayerData;
    #endregion

    #region Components
    public Animator Animator {  get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Other Variables
    public Vector2 curretVelocity { get; private set; }
    public int FacingDiraction {  get; private set; }

    private Vector2 _workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, _PlayerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, _PlayerData, "move");
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        FacingDiraction = 1;
    }

    private void Update()
    {
        curretVelocity = RB.velocity;
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
    }

    public void SetVelocityY(float velocity)
    {
        _workspace.Set(curretVelocity.x, velocity);
    }
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDiraction)
        {
            Flip();
        }
    }
    #endregion

    #region Other Funktions
    private void Flip()
    {
        FacingDiraction *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

}
