using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public AbstractBullet[] Attacks;

    private ObjectPooler m_objPool;
    private Transform m_bulletSpawn;
    private float standardBulletTimer = 0.0f;

    // Use this for initialization
    void Start ()
    {
        m_objPool = GameObject.Find(Constants.GameObjectNames.ObjectPooler).GetComponent<ObjectPooler>();
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
                m_objPool.PoolObject(ObjectPooler.PooledElement.PlayerBulletStandard, m_bulletSpawn.position);
                standardBulletTimer = 0.0f;
            }
        }
    }
}
