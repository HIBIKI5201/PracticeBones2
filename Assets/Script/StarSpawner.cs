using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StarSpawner : MonoBehaviour
{
    [Header("タイルマップ")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject Star;

    [Header("スポーン設定")]
    [SerializeField] private float _interval;

    [Header("スターのスポーン座標")]
    [SerializeField] private List<Vector2> _StarPosition;

    private List<Vector2> _shuffledPositions;
    public static int StarCount;
    void Start()
    {
        ShufflePositions();

        InvokeRepeating("SpawnStar", 0, _interval);
    }

    private void ShufflePositions()
    {
        _shuffledPositions = new List<Vector2>( _StarPosition);
    }

    public void SpawnStar()
    {
        if(StarCount <= _StarPosition.Count)
        {
            if (0 >= _shuffledPositions.Count)
            {
                ShufflePositions();
            }

            int randomIndex = Random.Range(0, _shuffledPositions.Count);

            // タイル座標を定義
            Vector3Int tilePosition = new Vector3Int((int)_shuffledPositions[randomIndex].x, (int)_shuffledPositions[randomIndex].y, 0);

            // タイル座標をワールド座標に変換
            Vector3 worldPosition = tilemap.CellToWorld(tilePosition);

            // オブジェクトを生成
            Instantiate(Star, worldPosition + new Vector3(0.5f, 0.5f), Quaternion.Euler(0, 0, 90));

            _shuffledPositions.RemoveAt(randomIndex);
            StarCount++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
