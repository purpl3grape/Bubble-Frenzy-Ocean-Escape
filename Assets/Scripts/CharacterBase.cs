using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : ObjectBase
{
    public float MoveSpeed => m_moveSpeed;
    [SerializeField] private float m_moveSpeed = 10;
    public int Health => m_health;
    [SerializeField] private int m_health = 1;
    public bool IsAlive => m_isAlive;
    [SerializeField] private bool m_isAlive;
    public Vector2 ImpactPos => m_impactPos;
    [SerializeField] private Vector2 m_impactPos;
    public GameObject Bullet => m_bullet;
    [SerializeField] GameObject m_bullet;
    public float FireRate => m_fireRate;
    [SerializeField] float m_fireRate = 0.15f;
    public int Score;

    public bool IsJump => m_isJump;
    private bool m_isJump;

    public virtual void ApplyDamage()
    {
        m_health -= 1;
        GameManager.Instance.SetHealth(m_health);
        SetIsAlive(m_health);
    }

    public virtual void SetIsAlive(float health)
    {
        SpriteRenderer.enabled = health > 0;
        GameManager.Instance.SetGameOver(!(health > 0));
    }

    public override void Move(Vector3 direction, float moveSpeed)
    {
        if (direction.x > 0)
        {
            if (!(transform.position.x + Size/2 < Screen.width))
            {
                direction = new Vector2(0, direction.y);
            }
        }
        else
        {
            if (!(transform.position.x - Size/2 > -Screen.width))
            {
                direction = new Vector2(0, direction.y);
            }
        }
        if (direction.y > 0)
        {
            if (!(transform.position.y + Size/ 2 < Screen.height / 2))
            {
                direction = new Vector2(direction.x, 0);
            }
        }
        else
        {
            if (!(transform.position.y - Size / 2 > -Screen.height / 2))
            {
                direction = new Vector2(direction.x, 0);
            }
        }
        base.Move(direction, moveSpeed);
    }

    public virtual void Shoot()
    {
        var spawnPos = transform.position;
        GameObject obj = (GameObject)Instantiate(m_bullet.gameObject, transform, instantiateInWorldSpace: false);
        SetPosition(spawnPos);
        obj.transform.SetParent(null, worldPositionStays: true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Shark>())
        {
            if (FlickerCoroutine != null)
            {
                StopCoroutine(FlickerCoroutine);
                FlickerCoroutine = null;
                SpriteRenderer.color = Color.white;
            }
            FlickerCoroutine = Flicker();
            StartCoroutine(FlickerCoroutine);
            ApplyDamage();
        }
    }

    IEnumerator FlickerCoroutine;
    IEnumerator Flicker()
    {
        SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        SpriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        SpriteRenderer.color = Color.white;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Shark>())
        {
            SpriteRenderer.color = Color.white;
        }
    }
    
}