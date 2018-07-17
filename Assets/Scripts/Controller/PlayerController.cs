using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 3.0f;
    public float RotationSpeed = 4.0f;
    public float GroundDistance = 0.2f;
    public float JumpHeight = 2.0f;
    public float DashDistance = 5.0f;
    public float GravityModifier = 1.0f;
    public float SidewardDamping = 0.75f;
    public float BackwardDamping = 0.75f;
    public float DashCoolDown = 3.0f;

    public LayerMask Ground;
    public Vector3 Drag;

    public bool IsDebugEnabled = true;

    private CharacterController m_controller;
    private Transform m_groundChecker;
    private Vector3 m_velocity;
    private float m_dashTimer = 0.0f;
    private bool m_isGrounded = true;

    // Use this for initialization
    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        m_groundChecker = transform.Find(Constants.GameObjectNames.GroundCheck);

    }

    // Update is called once per frame
    void Update ()
    {
        // ======================
        //         TIMER
        // ======================
        m_dashTimer += Time.deltaTime;

        // ======================
        //      GROUND CHECK
        // ======================
        m_isGrounded = Physics.CheckSphere(m_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        // visualize ground check sphere
        if(IsDebugEnabled)
        {
            DebugExtension.DebugWireSphere(m_groundChecker.position, GroundDistance);
        }

        if(m_isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = 0.0f;
        }

        // ======================
        //        MOVEMENT
        // ======================
        Vector3 move = Vector3.zero;
        ProcessMovement(out move);

        // ======================
        //         JUMP
        // ======================
        if(Input.GetButtonDown(Constants.Inputs.Jump) && m_isGrounded)
        {
            m_velocity.y = Mathf.Sqrt(JumpHeight * -2.0f * Physics.gravity.y);
        }

        // ======================
        //         DASH
        // ======================
        if(Input.GetButtonDown(Constants.Inputs.Dash))
        {
            if(m_dashTimer > DashCoolDown)
            {
                m_velocity += Vector3.Scale(move,
                    DashDistance * new Vector3((Mathf.Log(1.0f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime),
                    0,
                    (Mathf.Log(1.0f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));

                m_dashTimer = 0.0f;
            }
        }

        // ======================
        //        GRAVITY
        // ======================
        m_velocity.y += Physics.gravity.y * GravityModifier * Time.deltaTime;

        // ======================
        //         DRAG
        // ======================
        m_velocity.x /= 1 + Drag.x * Time.deltaTime;
        m_velocity.y /= 1 + Drag.y * Time.deltaTime;
        m_velocity.z /= 1 + Drag.z * Time.deltaTime;

        // ======================
        //   APPLY ALL MOVEMENT
        // ======================
        m_controller.Move(m_velocity * Time.deltaTime);

    }

    public void RotatePlayer(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }

    private void ProcessMovement(out Vector3 move)
    {
        bool forward = Input.GetKey(KeyCode.W);
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);
        bool backward = Input.GetKey(KeyCode.S);
        float moveScale = 1.0f;

        move = new Vector3(Input.GetAxis(Constants.Inputs.Horizontal), 0, Input.GetAxis(Constants.Inputs.Vertical));
        move = transform.TransformDirection(move);

        // we need to scale the movement down to cos(45) when the player runs diagonally
        if (forward && left || forward && right || backward && left || backward && right)
        {
            moveScale = 0.707107f;
        }
        else if (left || right)
        {
            moveScale = SidewardDamping;
        }
        else if (backward)
        {
            moveScale = BackwardDamping;
        }

        m_controller.Move(move * Time.deltaTime * MoveSpeed * moveScale);
    }
}
