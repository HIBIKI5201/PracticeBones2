using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("敵のPrefab")]
    [SerializeField] private GameObject EnemyPrefab;

    [Header("スポーンパラメーター")]
    [Tooltip("敵がスポーンする間隔。値が小さいほど早くスポーンする。")]
    [SerializeField] private float _interval;
    [SerializeField] private float _degreeInterval;
    private float Timer;
    private float DegreeInterval = 1;
    [Tooltip("敵がスポーンする範囲の中心点。")]
    [SerializeField] private Vector2 spawnFieldCentorPoint;
    [Tooltip("敵がスポーンする外周の範囲。横幅と縦幅を指定する。")]
    [SerializeField] private Vector2 spawnField;

    [Tooltip("敵が生成される方位")]
    public SpawnArea spawnArea;
    public enum SpawnArea
    {
        up,
        down,
        right,
        left
    }

    private Vector2 spawnPoint;

    [Header("敵のステータス")]
    [Tooltip("敵の移動スピード")]
    [SerializeField] private float _enemySpeed;

    void Start()
    {

    }

    private void EnemySpawn()
    {
        System.Array values = System.Enum.GetValues(typeof(SpawnArea));
        int randomIndex = Random.Range(0, values.Length);
        spawnArea = (SpawnArea)values.GetValue(randomIndex);

        if (spawnArea == SpawnArea.up || spawnArea == SpawnArea.down)
        {
            spawnPoint.x = Random.Range(spawnFieldCentorPoint.x - spawnField.x / 2, spawnFieldCentorPoint.x + spawnField.x / 2);
        }
        else if (spawnArea == SpawnArea.right || spawnArea == SpawnArea.left)
        {
            spawnPoint.y = Random.Range(spawnFieldCentorPoint.y - spawnField.y / 2, spawnFieldCentorPoint.y + spawnField.y / 2);
        }

        if (spawnArea == SpawnArea.up)
        {
            spawnPoint.y = spawnField.y / 2;
        }
        else if (spawnArea == SpawnArea.down)
        {
            spawnPoint.y = spawnField.y / 2 * -1;
        }
        else if (spawnArea == SpawnArea.right)
        {
            spawnPoint.x = spawnField.x / 2;
        }
        else if (spawnArea == SpawnArea.left)
        {
            spawnPoint.x = spawnField.x / 2 * -1;
        }

        GameObject enemy = Instantiate(EnemyPrefab, spawnPoint, Quaternion.Euler(Vector3.zero));
        enemy.GetComponent<NavMeshAgent>().speed = _enemySpeed;
    }

    void Update()
    {
        Timer += Time.deltaTime;
        if (_interval / (DegreeInterval) < Timer)
        {
            EnemySpawn();
            Timer = 0;
            DegreeInterval += _degreeInterval;
        }
    }
}
