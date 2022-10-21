using UnityEngine;

public class SprintState : State
{
    bool grounded;

    bool sprint;

    bool sprintJump;

    public SprintState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        sprint = false;
        sprintJump = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (sprintAction.triggered || input.sqrMagnitude == 0f)
        {
            sprint = false;
        }
        else
        {
            sprint = true;
        }

        if (jumpAction.triggered)
        {
            sprintJump = true;

        }

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (sprint)
        {
            character.animator.SetFloat("speed", (input.magnitude + 0.5f) * character.playerSpeed,
                character.speedDampTime, Time.deltaTime);
        }
        else
        {
            stateMachine.ChangeState(character.standing);
        }

        if (sprintJump)
        {
            stateMachine.ChangeState(character.sprintjumping);
        }
    }
}