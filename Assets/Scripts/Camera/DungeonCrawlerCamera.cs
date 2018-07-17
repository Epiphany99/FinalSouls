using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawlerCamera : MonoBehaviour
{
    public Transform Target;
    public float Damping;

    private Vector3 m_offset;

	// Use this for initialization
	void Start ()
    {
        m_offset = transform.position - Target.position;
	}

    private void LateUpdate()
    {
        Vector3 desiredPosition = Target.position + m_offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * Damping);
        transform.LookAt(Target);
    }
}
