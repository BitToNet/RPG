using UnityEngine;
 
public class CrouchingState : State
{
    float playerSpeed;
    // 头上有遮挡
    bool belowCeiling;
    // 站起来了
    bool crouchHeld;
 
    public CrouchingState(Character _character, StateMachine _stateMachine):base(_character, _stateMachine)
    {
    }
 
    public override void Enter()
    {
        base.Enter();
 
        character.animator.SetTrigger("crouch");  
        belowCeiling = false;
        crouchHeld = false;
        // gravityVelocity.y = 0;
 
        playerSpeed = character.crouchSpeed;
        character.controller.height = character.crouchColliderHeight;
        character.controller.center = new Vector3(0f, character.crouchColliderHeight / 2f, 0f);
        
    }
 
    public override void Exit()
    {
        base.Exit();
        character.controller.height = character.normalColliderHeight;
        character.controller.center = new Vector3(0f, character.normalColliderHeight / 2f, 0f);
        character.playerVelocity = new Vector3(input.x, 0, input.y);
        character.animator.SetTrigger("move");
    }
 
    public override void HandleInput()
    {
        base.HandleInput();
        if ((crouchAction.triggered || jumpAction.triggered) && !belowCeiling)
        {
            crouchHeld = true;
        }
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        character.animator.SetFloat("speed", input.magnitude*playerSpeed, character.speedDampTime, Time.deltaTime);
 
        if (crouchHeld)
        {
            stateMachine.ChangeState(character.standing);
        }
    }
 
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        belowCeiling = CheckCollisionOverlap(character.transform.position + Vector3.up * character.normalColliderHeight);
    }
 
    public bool CheckCollisionOverlap(Vector3 targetPositon)
    {
        int layerMask = 1 << 10;
        layerMask = ~layerMask;
        RaycastHit hit;
 
        Vector3 direction = targetPositon - character.transform.position;
        if (Physics.Raycast(character.transform.position, direction, out hit, character.normalColliderHeight, layerMask))
        {
            Debug.DrawRay(character.transform.position, direction * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(character.transform.position, direction * character.normalColliderHeight, Color.white);
            return false;
        }       
    }
}