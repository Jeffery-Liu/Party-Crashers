using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallPoolManager : MonoBehaviour {

    public GameObject m_BallPrefab;           // Ball prefab.
    public int        m_Size;                 // Ball Pool size.

    private List<GameObject> m_BallPool;      // Object pool for balls.

    // Use this for initialization
    void Start ()
    {
        SetBallPool();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void SetBallPool()
    {
        // Create object pool for balls
        m_BallPool = new List<GameObject>();

        // Instantiate balls and put them inside the object pool
        for (int i = 0; i < m_Size; ++i)
        {
            GameObject ball = Instantiate<GameObject>(m_BallPrefab);
            ball.SetActive(false);
            ball.GetComponent<BallManager>().SetBallPoolManager(this);
            m_BallPool.Add(ball);
        }
    }

    public GameObject GetBallFromPool()
    {
        // If my ball pool is not empty
        if (m_BallPool.Count > 0)
        {
            GameObject ball = m_BallPool[0];
            m_BallPool.RemoveAt(0);
            return ball;
        }
        return null;
    }

    public void PutBallBackIntoPool(GameObject ball)
    {
        m_BallPool.Add(ball);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.SetActive(false);
    }
}
