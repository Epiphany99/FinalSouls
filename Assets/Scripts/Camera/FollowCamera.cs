using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Target;
    public float Damping = 1.0f;

    private Vector3 m_offset;

	// Use this for initialization
	void Start ()
    {
        m_offset = Target.position - transform.position;
	}
	
	private void LateUpdate()
    {
        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = Target.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * Damping);

        Quaternion rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        transform.position = Target.position - (rotation * m_offset);

        transform.LookAt(Target);
    }
}
