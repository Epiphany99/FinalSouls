using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour
{
    public Transform Target;
    public Vector2 CameraTargetRotationBoundsX = new Vector2(-45.0f, 55.0f);
    public float RotationSpeed = 1.0f;

    public Vector3 CollisionNormal { get; private set; }
    public enum CameraState
    {
        Following,
        StartZooming,
        ZoomingIn,
        ZoomedIn,
        StartZoomOut
    }
    public CameraState CameraStates;

    private Camera m_camera;
    private Transform m_player;
    private Vector3 m_offset;
    private float m_distanceToCamera;
    private PlayerSphereCastToCamera raycastToTransform;

    // Use this for initialization
    void Start ()
    {  
        m_camera = GetComponent<Camera>();
        if(m_camera == null)
        {
            Debug.LogError("No Camera attached");
        }
        m_offset = Target.position - transform.position;
        m_distanceToCamera = m_offset.magnitude;
        m_player = Target.parent;
        raycastToTransform = m_player.transform.Find(Constants.GameObjectNames.CameraRaycast).GetComponent<PlayerSphereCastToCamera>();
        if(raycastToTransform == null)
        {
            Debug.LogError("No RaycastToTransform script attached");
        }
        CameraStates = CameraState.Following;
    }

    private void Update()
    {
        var distance = Mathf.Clamp(raycastToTransform.DistanceToObject, 0.0f, m_distanceToCamera);

        distance = Mathf.Clamp(distance, 0.0f, m_distanceToCamera);
        m_offset = m_offset.normalized * distance;
    }

    private void LateUpdate()
    {
        CalculateRotation();
    }

    private void CalculateRotation()
    {
        float horizontal = Input.GetAxis(Constants.Inputs.MouseX) * RotationSpeed;
        float vertical = Input.GetAxis(Constants.Inputs.MouseY) * RotationSpeed;

        m_player.Rotate(0.0f, horizontal, 0.0f);
        Target.Rotate(-vertical, 0.0f, 0.0f);

        float angle = Target.localEulerAngles.x;
        angle = (angle > 180) ? angle - 360 : angle;
        Target.localEulerAngles = new Vector3(Mathf.Clamp(angle, CameraTargetRotationBoundsX.x, CameraTargetRotationBoundsX.y), 0f, 0f);

        Quaternion rotation = Quaternion.Euler(Target.eulerAngles.x, Target.eulerAngles.y, 0);
        transform.position = Target.position - (rotation * m_offset);

        transform.LookAt(Target);
    }
}
