using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : ObjectBase
{
    public float MoveSpeed => m_moveSpeed;
    [SerializeField] private float m_moveSpeed = .8f;
    private float k_moveSpeedMin = 4f;
    private float k_moveSpeedMax = 12f;

    private void Awake()
    {
        m_moveSpeed = Random.Range(k_moveSpeedMin, k_moveSpeedMax);
    }

    private void FixedUpdate()
    {
        if (!(transform.position.y - Size / 2 > -Screen.height))
        {
            if (!ShouldDestroy)
            {
                ShouldDestroy = true;
                Destroy();
            }
        }
        else
        {
            Move(Vector2.down, m_moveSpeed);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Bullet>())
        {
            collision.GetComponent<Bullet>().Destroy();
        }
    }
}
