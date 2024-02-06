using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
	[Header("Grounded State (Superstate)")]
	public float frictionOnGround = 1f;

	[Header("Move State")]
	public float movementVelocity = 10f;

	[Header("Jump State")]
	public float jumpVelocity = 19f;
	public int amountOfJumps = 1;

	[Header("In Air State (Superstate)")]
	public float maxFallVelocity = 5f;
	public float maxFallDistance = 20f;
	public float coyoteTime = 0.3f;
	public float variableJumpHeightMultiplier = 0.5f;

	[Header("Wall Slide State")]
	public float wallSlideVelocity = -2f;

	[Header("Check Veriables")]
	public float groundCheckRadius = 0.19f;
	public float wallCheckDistance = 0.4f;
	public LayerMask whatIsGround;

}
