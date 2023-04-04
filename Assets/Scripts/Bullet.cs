using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ObjectBase
{
    public float MoveSpeed => m_moveSpeed;
    [SerializeField] private float m_moveSpeed = 2f;
    private float k_moveSpeedMin = 1f;
    private float k_moveSpeedMax = 3f;

    public void Init(Vector2 spawnPos)
    {
        m_moveSpeed = Random.Range(k_moveSpeedMin, k_moveSpeedMax);
        transform.position = spawnPos;
        transform.parent = null;
    }

    private void FixedUpdate()
    {
        if (!(transform.position.x + Size / 2 < Screen.width))
        {
            if (!ShouldDestroy)
            {
                ShouldDestroy = true;
                Destroy();
            }
        }
        else
        {
            Move(Vector2.right, m_moveSpeed);
        }
    }

}
