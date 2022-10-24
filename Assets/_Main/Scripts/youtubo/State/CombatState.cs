using UnityEngine;
public class CombatState : State
{
    bool sheathWeapon;
    float playerSpeed;
    bool attack;
 
    public CombatState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
    }
 
    public override void Enter()
    {
        base.Enter();
 
        sheathWeapon = false;
        attack = false;
 
        velocity = character.playerVelocity;
        playerSpeed = character.playerSpeed;
    }
 
    public override void HandleInput()
    {
        base.HandleInput();
 
        if (drawWeaponAction.triggered && !character.isCombatStateChanging)
        {
            sheathWeapon = true;
        }
 
        if (attackAction.triggered)
        {
            attack = true;
        }
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
 
        character.animator.SetFloat("speed", input.magnitude*playerSpeed, character.speedDampTime, Time.deltaTime);
 
        if (sheathWeapon)
        {
            character.animator.SetTrigger("sheathWeapon");
            stateMachine.ChangeState(character.standing);
        }
 
        if (attack)
        {
            character.animator.SetTrigger("attack");
            stateMachine.ChangeState(character.attacking);
        }
    }
}