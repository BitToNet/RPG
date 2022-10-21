using UnityEngine;
 
public class JumpingState:State
{
    
    //滞空相关
    // 角色垂直方向速度
    public float _verticalVelocity;
    public bool _grounded;
    
    public JumpingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
    }
 
    public override void Enter()
    {
        base.Enter();
 
        _grounded = false;
        AnimatorTrigger();
        _verticalVelocity = 0;
        Jump();
    }

    public virtual void AnimatorTrigger()
    {
        character.animator.SetTrigger("jump");
    }
    
    public virtual void NextState()
    {
        stateMachine.ChangeState(character.landing);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
 
        if (_grounded)
        {
            NextState();
        }
    }
 
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // 下降时加速度更大，虽然不科学，但是游戏感觉更好
        if (_verticalVelocity <= 0)
        {
            _verticalVelocity += character.gravityValue * character.gravityMultiplier * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += character.gravityValue * Time.deltaTime;
        }
        
        _grounded = character.controller.isGrounded;
    }
 
    void Jump()
    {
        _verticalVelocity += Mathf.Sqrt(character.jumpHeight * -2.0f * character.gravityValue);
    }

    public override void AnimatorMove()
    {
        // 重力自己计算
        character.averageVel.y = _verticalVelocity;
        var playerDeltaPosition = character.averageVel * Time.deltaTime;
        character.controller.Move(playerDeltaPosition);
    }
}