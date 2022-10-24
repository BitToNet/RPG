using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [Header("Controls")] [Range(3.5f, 4.7f)]
    public float playerSpeed = 3.5f;

    [Range(1f, 2f)]
    public float crouchSpeed = 2.0f;
    // [Range(4.7f, 7f)] public float sprintSpeed = 7.0f;
    public float jumpHeight = 0.8f;
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;
    public float crouchColliderHeight = 1.35f;
    public bool 显示鼠标 = false;

    [Header("Animation Smoothing")] [Range(0, 1)]
    public float speedDampTime = 0.1f;

    [Range(0, 1)] public float velocityDampTime = 0.9f;
    [Range(0, 1)] public float rotationDampTime = 0.2f;
    [Range(0, 1)] public float airControl = 0.5f;

    public StateMachine movementSM;
    public StandingState standing;
    public JumpingState jumping;
    public CrouchingState crouching;
    public LandingState landing;
    public SprintState sprinting;
    public SprintJumpState sprintjumping;
    public CombatState combatting;
    public AttackState attacking;
    
    // 取跳跃前三帧的平均速度
    public Vector3 averageVel = Vector3.zero;
    static readonly int CACHE_SIZE = 3;
    Vector3[] velCache = new Vector3[CACHE_SIZE];
    int curentCacheIndex = 0;

    [HideInInspector] public float gravityValue = -9.81f;
    [HideInInspector] public float normalColliderHeight;
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public Transform cameraTransform;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Vector3 playerVelocity;
    // 状态转换中，此时再点切换无效
    [HideInInspector] public bool isCombatStateChanging = false;

    private Transform tr;


    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        tr = transform;

        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        crouching = new CrouchingState(this, movementSM);
        landing = new LandingState(this, movementSM);
        sprinting = new SprintState(this, movementSM);
        sprintjumping = new SprintJumpState(this, movementSM);
        combatting = new CombatState(this, movementSM);
        attacking = new AttackState(this, movementSM);

        movementSM.Initialize(standing);

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;

        if (!显示鼠标)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        //手柄输入
        movementSM.currentState.HandleInput();

        //逻辑更新
        movementSM.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        // 物理更新
        movementSM.currentState.PhysicsUpdate();
    }

    private void OnAnimatorMove()
    {
        movementSM.currentState.AnimatorMove();
    }

    // 取跳跃前三帧的平均速度
    public Vector3 AverageVel(Vector3 newVel)
    {
        velCache[curentCacheIndex] = newVel;
        curentCacheIndex++;
        curentCacheIndex %= CACHE_SIZE;
        Vector3 average = Vector3.zero;
        foreach (var vel in velCache)
        {
            average += vel;
        }

        return average / CACHE_SIZE;
    }
}