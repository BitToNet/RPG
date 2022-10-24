using UnityEngine;
 
public class StandingState: State
{  
    bool jump;   
    bool crouch;
    bool grounded;
    bool sprint;
    bool drawWeapon;

    public StandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
    }
 
    public override void Enter()
    {
        base.Enter();
 
        jump = false;
        crouch = false;
        sprint = false;
        drawWeapon = false;
    }
 
    public override void HandleInput()
    {
        base.HandleInput();
        
        if (jumpAction.triggered)
        {
            jump = true;
        }
        if (crouchAction.triggered)
        {
            crouch = true;
        }
        if (sprintAction.triggered)
        {
            sprint = true;
        }
 
        if (drawWeaponAction.triggered && !character.isCombatStateChanging)
        {
            drawWeapon = true;
        }
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
 
        character.animator.SetFloat("speed", input.magnitude * character.playerSpeed, character.speedDampTime, Time.deltaTime);
 
        if (sprint)
        {
            stateMachine.ChangeState(character.sprinting);
        }    
        if (jump)
        {
            stateMachine.ChangeState(character.jumping);
        }
        if (crouch)
        {
            stateMachine.ChangeState(character.crouching);
        }
        if (drawWeapon)
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("drawWeapon");
        }
    }
}