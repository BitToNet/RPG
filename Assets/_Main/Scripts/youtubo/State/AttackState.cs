using UnityEngine;
public class AttackState : State
{
    float timePassed;
    float clipLength;
    float clipSpeed;
    bool attack;
    public AttackState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }
 
    public override void Enter()
    {
        base.Enter();
 
        attack = false;
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("attack");
        character.animator.SetFloat("speed", 0f);
    }
 
    public override void HandleInput()
    {
        base.HandleInput();
 
        if (attackAction.triggered)
        {
            attack = true;
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
 
        timePassed += Time.deltaTime;
        // 获取第二层正在运行的动画的第一个片段的长度
        clipLength = character.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
        // 获取播放速度
        clipSpeed = character.animator.GetCurrentAnimatorStateInfo(1).speed;
 
        if (timePassed >= clipLength / clipSpeed && attack)
        {
            //攻击2
            stateMachine.ChangeState(character.attacking);
        }
        if (timePassed >= clipLength / clipSpeed)
        {
            //攻击玩回到战斗状态
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("move");
        }
 
    }
    public override void Exit()
    {
        base.Exit();
        character.animator.applyRootMotion = false;
    }
}