using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    public GameObject PlayerBulletStandard;
    public uint PlayerBulletStandardAmount = 5;

    public GameObject EnemyBulletStandard;
    public uint EnemyBulletStandardAmount = 5;

    public enum PooledElement
    {
        PlayerBulletStandard,
        EnemyBulletStandard
    };

    private GameObject[] m_playerBulletStandardPool;
    private uint m_playerBulletStandardCounter = 0;

    private GameObject[] m_enemyBulletStandardPool;
    private uint m_enemyBulletStandardCounter = 0;

    // Use this for initialization
    void Start ()
    {
        m_playerBulletStandardPool = new GameObject[PlayerBulletStandardAmount];
        for(uint i = 0; i < PlayerBulletStandardAmount; i++)
        {
            m_playerBulletStandardPool[i] = Instantiate(PlayerBulletStandard);
            m_playerBulletStandardPool[i].SetActive(false);
        }

        m_enemyBulletStandardPool = new GameObject[EnemyBulletStandardAmount];
        for (uint i = 0; i < EnemyBulletStandardAmount; i++)
        {
            m_enemyBulletStandardPool[i] = Instantiate(EnemyBulletStandard);
            m_enemyBulletStandardPool[i].SetActive(false);
        }
    }
	 
    public GameObject GetPooledObject(PooledElement toPool, Vector3 startPos)
    {
        GameObject obj = null;

        switch(toPool)
        {
            case PooledElement.PlayerBulletStandard:
                obj = m_playerBulletStandardPool[m_playerBulletStandardCounter];
                if(++m_playerBulletStandardCounter >= PlayerBulletStandardAmount)
                {
                    m_playerBulletStandardCounter = 0;
                }
                break;

            case PooledElement.EnemyBulletStandard:
                obj = m_enemyBulletStandardPool[m_enemyBulletStandardCounter];
                if (++m_enemyBulletStandardCounter >= EnemyBulletStandardAmount)
                {
                    m_enemyBulletStandardCounter = 0;
                }
                break;
        }

        obj.transform.position = startPos;
        // ToDo: de- and activate the gameobject so trigger the OnEnable-method at bullet script (not so cool)
        obj.SetActive(false);
        obj.SetActive(true);

        return obj;
    }

    public void PoolObject(PooledElement toPool, GameObject owner, Vector3 startPos)
    {
        GameObject obj = null;

        switch (toPool)
        {
            case PooledElement.PlayerBulletStandard:
                obj = m_playerBulletStandardPool[m_playerBulletStandardCounter];
                obj.GetComponent<StandardBullet>().Owner = owner;
                if (++m_playerBulletStandardCounter >= PlayerBulletStandardAmount)
                {
                    m_playerBulletStandardCounter = 0;
                }
                break;

            case PooledElement.EnemyBulletStandard:
                obj = m_enemyBulletStandardPool[m_enemyBulletStandardCounter];
                obj.GetComponent<StandardBullet>().Owner = owner;
                if (++m_enemyBulletStandardCounter >= EnemyBulletStandardAmount)
                {
                    m_enemyBulletStandardCounter = 0;
                }
                break;
        }

        obj.transform.position = startPos;
        // ToDo: de- and activate the gameobject so trigger the OnEnable-method at bullet script (not so cool)
        obj.SetActive(false);
        obj.SetActive(true);

    }
}
