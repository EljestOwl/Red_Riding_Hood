using UnityEngine;

public class PlayerDeathWolfState : PlayerState
{
	private string _causeOfDeathText = "You didn't run fast enough.";

	public PlayerDeathWolfState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string _animBoolName) : base(player, stateMachine, playerData, _animBoolName)
	{
	}

	public override void Enter()
	{
		base.Enter();

		player.rb2D.sharedMaterial.friction = 1;

		GameOver();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
		player.rb2D.velocity = Vector2.zero;
	}

	private void GameOver()
	{
		GameManagerScript.instance.GameOver(_causeOfDeathText);
	}

}
