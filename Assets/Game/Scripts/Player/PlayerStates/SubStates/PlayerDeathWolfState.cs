using UnityEngine;

public class PlayerDeathWolfState : PlayerState
{
	public PlayerDeathWolfState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string _animBoolName) : base(player, stateMachine, playerData, _animBoolName)
	{
	}

	public override void Enter()
	{
		base.Enter();
	}

}
