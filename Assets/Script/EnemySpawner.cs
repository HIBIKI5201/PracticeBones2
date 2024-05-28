using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("�G��Prefab")]
    [SerializeField] private GameObject EnemyPrefab;

    [Header("�X�|�[���p�����[�^�[")]
    [Tooltip("�G���X�|�[������Ԋu�B�l���������قǑ����X�|�[������B")]
    [SerializeField] private float _interval;
    [SerializeField] private float _degreeInterval;
    private float Timer;
    private float DegreeInterval = 1;
    [Tooltip("�G���X�|�[������͈͂̒��S�_�B")]
    [SerializeField] private Vector2 spawnFieldCentorPoint;
    [Tooltip("�G���X�|�[������O���͈̔́B�����Əc�����w�肷��B")]
    [SerializeField] private Vector2 spawnField;

    [Tooltip("�G��������������")]
    public SpawnArea spawnArea;
    public enum SpawnArea
    {
        up,
        down,
        right,
        left
    }

    private Vector2 spawnPoint;

    [Header("�G�̃X�e�[�^�X")]
    [Tooltip("�G�̈ړ��X�s�[�h")]
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
