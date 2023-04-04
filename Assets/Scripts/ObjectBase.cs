using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBase : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer => m_spriteRenderer;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    public bool ShouldDestroy { get; set; }
    [SerializeField] private bool m_shouldDestroy;

    public float Size => transform.localScale.x;
    public virtual void Move(Vector3 direction, float moveSpeed)
    {
        transform.position += direction * moveSpeed;
    }

    public virtual void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    
    public virtual void SetPositionOf(ObjectBase objBase, Vector2 position)
    {
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

}
