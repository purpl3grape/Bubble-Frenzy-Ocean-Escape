using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType
{
    Asteroid,
    Enemy,
}

public class GenericSpawner : ObjectBase
{
    public ObjectBase PrefabObj => m_prefabObj;
    [SerializeField] private ObjectBase m_prefabObj;
    public SpawnType SpawnType => m_spawnType;
    [SerializeField] private SpawnType m_spawnType;
    public float SpawnRate => m_spawnRate;
    [Min(0.01f), Range(0.01f, 10f)] [SerializeField] private float m_spawnRate = 1f;
    private float m_lastSpawnTime;
    public Transform Tr => m_tr;
    private Transform m_tr;

    private void Awake()
    {
        m_tr = transform;
        m_lastSpawnTime = Time.time;
    }
    private void Update()
    {
        if (GameManager.Instance.GameOver) return;
        if (Time.time - m_lastSpawnTime > m_spawnRate)
        {
            m_lastSpawnTime = Time.time;
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        var randomX = Random.Range(-Screen.width + m_prefabObj.Size / 2, Screen.width - m_prefabObj.Size / 2);
        var spawnPos = new Vector2(randomX, Screen.height/2);
        GameObject obj = (GameObject)Instantiate(m_prefabObj.gameObject, Tr.parent, instantiateInWorldSpace: false);
        SetPositionOf(m_spawnType.Equals(SpawnType.Asteroid) ? obj.GetComponent<Shark>() : obj.GetComponent<Bubble>(), spawnPos);
    }

    public override void SetPositionOf(ObjectBase objectBase, Vector2 position)
    {
        objectBase.SetPosition(position);
    }
}