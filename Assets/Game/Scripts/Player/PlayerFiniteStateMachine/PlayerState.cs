using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;
    protected bool isAnimationFinished;
    protected bool isExetingState;

    private string _animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string _animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this._animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.Animator.SetBool(_animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        // Debug.Log(_animBoolName);
        isExetingState = false;
    }

    public virtual void Exit()
    {
        player.Animator.SetBool(_animBoolName, false);
        isExetingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
