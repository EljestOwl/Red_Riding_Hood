using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int _amountOfJumpesLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string _animBoolName) : base(player, stateMachine, playerData, _animBoolName)
    {
        _amountOfJumpesLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerData.jumpVelocity);

        isAbillityDone = true;

        DecreseAmountOfJumpesLeft();

        player.InAirState.SettIsJumping();

        player.rb2D.sharedMaterial.friction = 0f;
    }

    public bool CanJump()
    {
        if (_amountOfJumpesLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpesLeft()
    {
        _amountOfJumpesLeft = playerData.amountOfJumps;
    }

    public void DecreseAmountOfJumpesLeft()
    {
        _amountOfJumpesLeft--;
    }

}
