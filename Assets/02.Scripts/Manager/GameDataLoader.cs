using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using UnityEngine;
using UnityEngine.Networking;

public class GameDataLoader : Singleton<GameDataLoader>
{
    [Header("CSV URL")]
    [SerializeField] private string _enemyCsvUrl;

    public EnemyData[] EnemyDatas { get; private set; } = Array.Empty<EnemyData>();
    public bool IsLoaded { get; private set; } = false;

    private IEnumerator Start()
    {
        string enemyCsv = null;

        yield return StartCoroutine(DownloadCsv(_enemyCsvUrl, text => enemyCsv = text));
        EnemyDatas = ParseEnemies(enemyCsv);

        IsLoaded = true;
    }

    private IEnumerator DownloadCsv(string url, Action<string> onSuccess)
    {
        using UnityWebRequest request = UnityWebRequest.Get(url);
        request.timeout = 10;
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            throw new System.InvalidOperationException(request.error);
        }

        string text = request.downloadHandler.text;
        onSuccess(text);
    }

    private EnemyData[] ParseEnemies(string text)
    {
        List<EnemyData> result = new();
        using StringReader reader = new(text.TrimStart('\uFEFF'));
        using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

        if (!csv.Read())
        {
            Debug.LogError("CSV에 헤더가 없습니다.");
            return Array.Empty<EnemyData>();
        }

        csv.ReadHeader();

        while (csv.Read())
        {
            string key = csv.GetField<string>("Prefab").Trim();
            GameObject prefab = Resources.Load<GameObject>($"Enemy/{key}");

            float maxHealth = csv.GetField<float>("MaxHealth");
            float moveSpeed = csv.GetField<float>("MoveSpeed");
            float attackPower = csv.GetField<float>("AttackPower");
            float score = csv.GetField<float>("Score");
            float spawnWeight = csv.GetField<float>("SpawnWeight");
            result.Add(new EnemyData(prefab, maxHealth, moveSpeed, attackPower, score, spawnWeight));
        }

        return result.ToArray();
    }
}