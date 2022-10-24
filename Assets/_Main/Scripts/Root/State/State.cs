using UnityEngine;
using UnityEngine.InputSystem;

public class State
{
    public Character character;
    public StateMachine stateMachine;

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;

    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;
    public InputAction crouchAction;
    public InputAction sprintAction;
    public InputAction drawWeaponAction;
    public InputAction attackAction;

    // 玩家实际要运行的方向
    Vector3 palyerMovement = Vector3.zero;
    private Transform _cameraTransform;

    public State(Character _character, StateMachine _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
        _cameraTransform = _character.cameraTransform;

        moveAction = character.playerInput.actions["Move"];
        lookAction = character.playerInput.actions["Look"];
        jumpAction = character.playerInput.actions["Jump"];
        crouchAction = character.playerInput.actions["Crouch"];
        sprintAction = character.playerInput.actions["Sprint"];
        drawWeaponAction = character.playerInput.actions["DrawWeapon"];
        attackAction = character.playerInput.actions["Attack"];
    }

    public virtual void Enter()
    {
        //StateUI.instance.SetStateText(this.ToString());
        Debug.Log("Enter State: " + this.ToString());

        input = Vector2.zero;
    }

    public virtual void HandleInput()
    {
        input = moveAction.ReadValue<Vector2>();
        Vector3 cameraForward = new Vector3(_cameraTransform.forward.x, 0, _cameraTransform.forward.z).normalized;
        palyerMovement = cameraForward * input.y + _cameraTransform.right * input.x;
    }

    public virtual void LogicUpdate()
    {
        if (palyerMovement.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation,
                Quaternion.LookRotation(palyerMovement), character.rotationDampTime);
        }
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void AnimatorMove()
    {
        // 将运动交给角色控制器，而不是root motion
        // SimpleMove会自动计算重力
        // 地面上的重力SimpleMove自动计算，空中的由跳跃、跌落自己计算
        character.controller.SimpleMove(character.animator.velocity);
        //取跳跃前三帧的平均速度
        character.averageVel = character.AverageVel(character.animator.velocity);
    }

    public virtual void Exit()
    {
    }
}