using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool _isGrounded;
    private int _xInput;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _coyoteTime;
    private bool _isJumping;
    private bool _isTouchingWall;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string _animBoolName) : base(player, stateMachine, playerData, _animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = player.CheckIfGrounded();
        _isTouchingWall = player.CheckIfTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();

        player.rb2D.sharedMaterial.friction = 0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        _xInput = player.InputHandler.NormInputX;
        _jumpInput = player.InputHandler.jumpInput;
        _jumpInputStop = player.InputHandler.jumpInputStop;

        CheckJumpMultiplier();

        if (_isGrounded && player.curretVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if(_jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (_isTouchingWall && _xInput == player.FacingDiraction && player.curretVelocity.y <= 0f)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            player.CheckIfShouldFlip(_xInput);
            player.SetVelocityX(playerData.movementVelocity * _xInput);
            player.Animator.SetFloat("yVelocity", player.curretVelocity.y);
            player.Animator.SetFloat("xVelocity", Mathf.Abs(player.curretVelocity.x));
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                player.SetVelocityY(player.curretVelocity.y * playerData.variableJumpHeightMultiplier);
                _isJumping = false;
            }
            else if (player.curretVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            _coyoteTime = false;
            player.JumpState.DecreseAmountOfJumpesLeft();
        }
    }

    public void StartCoyoteTime()
    {
        _coyoteTime = true;
    }

    public void SettIsJumping()
    {
        _isJumping = true;
    }
}
