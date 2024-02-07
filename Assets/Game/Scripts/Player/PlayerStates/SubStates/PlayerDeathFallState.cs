using UnityEngine;

public class PlayerDeathFallState : PlayerState
{
	public PlayerDeathFallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string _animBoolName) : base(player, stateMachine, playerData, _animBoolName)
	{
	}

	public override void Enter()
	{
		base.Enter();
	}

}
