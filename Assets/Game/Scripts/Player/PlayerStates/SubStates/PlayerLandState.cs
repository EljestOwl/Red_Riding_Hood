using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
	public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string _animBoolName) : base(player, stateMachine, playerData, _animBoolName)
	{
	}
	public override void Enter()
	{
		base.Enter();

		if (player.InAirState.fallHight > playerData.maxFallDistance)
		{
			stateMachine.ChangeState(player.DeathFallState);
		}

	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (!isExetingState)
		{
			if (xInput != 0)
			{
				stateMachine.ChangeState(player.MoveState);
			}
			else if (isAnimationFinished)
			{
				stateMachine.ChangeState(player.IdleState);
			}
		}

	}
}
