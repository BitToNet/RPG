using UnityEngine;
public class SprintJumpState:JumpingState
{

    public SprintJumpState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
    }

    public override void AnimatorTrigger()
    {
        character.animator.SetTrigger("sprintJump");
    }

    public override void NextState()
    {
        stateMachine.ChangeState(character.sprinting);
    }
}