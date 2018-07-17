using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject PlayerBulletStandard;
    public uint PlayerBulletStandardAmount = 5;
    public enum PooledElement
    {
        PlayerBulletStandard
    };

    private GameObject[] m_playerBulletStandardPool;
    private uint m_playerBulletStandardCounter = 0;

	// Use this for initialization
	void Start ()
    {
        m_playerBulletStandardPool = new GameObject[PlayerBulletStandardAmount];
        for(uint i = 0; i < PlayerBulletStandardAmount; i++)
        {
            m_playerBulletStandardPool[i] = Instantiate(PlayerBulletStandard);
            m_playerBulletStandardPool[i].SetActive(false);
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
        }

        obj.transform.position = startPos;
        // ToDo: de- and activate the gameobject so trigger the OnEnable-method at bullet script (not so cool)
        obj.SetActive(false);
        obj.SetActive(true);

        return obj;
    }

    public void PoolObject(PooledElement toPool, Vector3 startPos)
    {
        GameObject obj = null;

        switch (toPool)
        {
            case PooledElement.PlayerBulletStandard:
                obj = m_playerBulletStandardPool[m_playerBulletStandardCounter];
                if (++m_playerBulletStandardCounter >= PlayerBulletStandardAmount)
                {
                    m_playerBulletStandardCounter = 0;
                }
                break;
        }

        obj.transform.position = startPos;
        // ToDo: de- and activate the gameobject so trigger the OnEnable-method at bullet script (not so cool)
        obj.SetActive(false);
        obj.SetActive(true);

    }
}
