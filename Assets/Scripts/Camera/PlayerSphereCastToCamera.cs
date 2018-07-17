using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSphereCastToCamera : MonoBehaviour
{

    private float m_distanceToObject = 0.0f;
    public float DistanceToObject{ get { return m_distanceToObject; } private set{ m_distanceToObject = value; } }

    private CharacterController playerCharacterController;
    private RaycastHit hit;

    private void Start()
    {
        playerCharacterController = transform.parent.GetComponent<CharacterController>();
        if(playerCharacterController == null)
        {
            Debug.LogError("No CharacterController attached");
        }
        hit = new RaycastHit
        {
            distance = float.MaxValue
        };
    }
    // Update is called once per frame
    void Update ()
    {
        Physics.SphereCast(transform.position, playerCharacterController.radius, transform.TransformDirection(Vector3.forward), out hit);
        DistanceToObject = hit.distance;
        hit.distance = float.MaxValue;
	}
}
