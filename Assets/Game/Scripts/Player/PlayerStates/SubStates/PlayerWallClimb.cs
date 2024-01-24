using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimb : PlayerTouchingWallState
{
    public PlayerWallClimb(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string _animBoolName) : base(player, stateMachine, playerData, _animBoolName)
    {
    }
}
