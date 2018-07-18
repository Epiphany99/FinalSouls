using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public AbstractBullet[] Attacks;

    private Transform m_bulletSpawn;
    private float standardBulletTimer = 0.0f;

    // Use this for initialization
    void Start ()
    {
        m_bulletSpawn = transform.Find(Constants.GameObjectNames.BulletSpawn);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // ======================
        //        TIMERS
        // ====================== 
        standardBulletTimer += Time.deltaTime;

        // ======================
        //        ATTACK
        // ======================
        if (Input.GetMouseButtonDown(0))
        {
            if(standardBulletTimer > Attacks[0].BulletCoolDown)
            {
                ObjectPooler.Instance.PoolObject(ObjectPooler.PooledElement.PlayerBulletStandard, gameObject, m_bulletSpawn.position);
                standardBulletTimer = 0.0f;
            }
        }
    }
}
