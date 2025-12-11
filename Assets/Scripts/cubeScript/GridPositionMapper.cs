//適応した年度の点の変数を作り、座標を取得する。
//使い方
//取得したい点（メッシュボールなど）にプログラムをアタッチメントするだけ。

using UnityEngine;
using System.Collections.Generic;

public class GridPositionMapper : MonoBehaviour
{
    private string yearPrefix;  // 例：「7_」

    private Dictionary<string, GameObject> objectMap = new Dictionary<string, GameObject>();
    private Dictionary<string, Vector3> positionMap = new Dictionary<string, Vector3>();

    void Start()
    {
        // GameManager から年度を取得
        var data = GameManager.Instance?.currentYearData;

        if (data == null)
        {
            Debug.LogError("GameManager に currentYearData が設定されていません");
            return;
        }

        // yearIdentifier = "R7" → "7" を取り出す
        string number = data.yearIdentifier.Replace("R", "");
        yearPrefix = number + "_";     // 例： "7_"

        Debug.Log("年度プリフィックス：" + yearPrefix);

        // 探索して登録する名前リスト（必要なら A〜H を増やす）
        string[] names = new string[]
        {
            "A","B","C","D",
            "E","F","G","H",
            "P"
        };

        RegisterObjects(names);
    }

    /// <summary>
    /// 必要なオブジェクトを年度に合わせて登録する
    /// </summary>
    private void RegisterObjects(string[] names)
    {
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (string letter in names)
        {
            string targetName = yearPrefix + letter;   // 例： "7_A"
            GameObject found = null;

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == targetName)
                {
                    found = obj;
                    objectMap[targetName] = obj;
                    positionMap[targetName] = obj.transform.position;

                    Debug.Log($"{targetName} を登録: 初期位置 {obj.transform.position}");
                    break;
                }
            }

            if (found == null)
            {
                Debug.LogWarning($"{targetName} が見つかりませんでした");
            }
        }
    }

    void Update()
    {
        foreach (var pair in objectMap)
        {
            if (pair.Value != null)
                positionMap[pair.Key] = pair.Value.transform.position;
        }
    }

    /// <summary>
    /// 例：「7_A」など完全な名前で座標取得
    /// </summary>
    public Vector3 GetPosition(string fullName)
    {
        if (positionMap.ContainsKey(fullName))
            return positionMap[fullName];

        Debug.LogWarning($"{fullName} の座標は登録されていません");
        return Vector3.zero;
    }

    /// <summary>
    /// "A" を渡すと "7_A" に自動変換して座標取得
    /// </summary>
    public Vector3 GetPositionAuto(string letter)
    {
        string key = yearPrefix + letter;

        if (positionMap.ContainsKey(key))
            return positionMap[key];

        Debug.LogWarning($"{key} の座標は登録されていません");
        return Vector3.zero;
    }
}
