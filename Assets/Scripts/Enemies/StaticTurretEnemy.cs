using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTurretEnemy : MonoBehaviour
{
    public AbstractBullet Attack;
    public float Damage = 20.0f;
    public float AttackCoolDown = 1.0f;
    public float AttackRadius = 10.0f;
    public float RotationDamping = 20.0f;
    public enum Action
    {
        Searching,
        Aim,
        Attack
    }
    public Action ActionState;

    public bool LockY = false;
    public bool IsDebugEnabled = false;

    private Transform m_bulletSpawn;
    private Transform m_playerTransform;
    private float m_attackRadiusSqr;
    private float m_timer = 0.0f;

	// Use this for initialization
	void Start ()
    {
        m_bulletSpawn = transform.Find(Constants.GameObjectNames.BulletSpawn);
        m_playerTransform = FindObjectOfType<PlayerController>().transform;
        m_attackRadiusSqr = AttackRadius * AttackRadius;
        ActionState = Action.Searching;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(IsDebugEnabled)
        {
            DebugExtension.DebugCircle(transform.position, AttackRadius);
        }

        switch (ActionState)
        {
            case Action.Searching:
                var sqrDistance = (gameObject.transform.position - m_playerTransform.position).sqrMagnitude;
                if (sqrDistance < m_attackRadiusSqr)
                {

                    ActionState = Action.Aim;
                }
                break;

            case Action.Aim:
                m_timer += Time.deltaTime;
                var lookPos = m_playerTransform.position - transform.position;
                if(LockY)
                {
                    lookPos.y = 0;
                }
                var targetRotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationDamping);
                if(m_timer >= AttackCoolDown)
                {
                    m_timer = 0.0f;
                    ActionState = Action.Attack;
                }

                break;

            case Action.Attack:
                ObjectPooler.Instance.PoolObject(ObjectPooler.PooledElement.EnemyBulletStandard, m_bulletSpawn.position);
                ActionState = Action.Searching;

                break;
        }

	}
}
