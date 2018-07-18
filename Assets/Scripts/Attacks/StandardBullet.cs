using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : AbstractBullet
{
    private Vector3 m_direction;
    private float m_lifeTimer = 0.0f;

    private void OnEnable()
    {
        switch(AimType)
        {
            case Target.Forward:
                m_direction = Owner.transform.forward;
                break;

            case Target.MousePosition:
                CalculateMousePosDirection();
                break;
        }

        m_lifeTimer = 0.0f;
    }


    // Update is called once per frame
    void Update ()
    {
        CalculateTrajectory();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(IgnoreTrigger.ToString()))
        {
            return;
        }

        if (Utilities.IsInLayerMask(other.gameObject.layer, HurtableLayerMask))
        {
            // ToDo: Not cool
            if(other.gameObject.name == "Player")
            {
                PlayerHealthController.Instance.ReceiveDamage(BulletDamage);
            }
            else
            {
                other.gameObject.GetComponent<EnemyHealthController>().ReceiveDamage(BulletDamage);
            }
        }

        gameObject.SetActive(false);
    }

    protected override void CalculateTrajectory()
    {
        transform.Translate(BulletSpeed * m_direction * Time.deltaTime);

        m_lifeTimer += Time.deltaTime;
        if (m_lifeTimer > LifeTime)
        {
            gameObject.SetActive(false);
            m_lifeTimer = 0.0f;
        }
    }

    protected override void CalculateMousePosDirection()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 1000.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        m_direction = (mousePos - transform.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, m_direction, out hit))
        {
            m_direction = (hit.point - transform.position).normalized;
        }
    }

}
