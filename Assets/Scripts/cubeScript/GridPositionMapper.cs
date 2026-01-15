using UnityEngine;
using System.Collections.Generic;

public class GridPositionMapper : MonoBehaviour
{
    private string yearPrefix;  // 例：「7_」

    private Dictionary<string, GameObject> objectMap = new Dictionary<string, GameObject>();
    private Dictionary<string, Vector3> positionMap = new Dictionary<string, Vector3>();

    void Start()
    {
        var data = GameManager.Instance?.currentYearData;

        if (data == null)
        {
            Debug.LogError("GameManager に currentYearData が設定されていません");
            return;
        }

        string number = data.yearIdentifier.Replace("R", "");
        yearPrefix = number + "_";

        Debug.Log("年度プリフィックス：" + yearPrefix);

        string[] names = new string[]
        {
            "A","B","C","D",
            "E","F","G","H",
            "P","M","Q"
        };

        RegisterObjects(names);
        
        Debug.Log("GridPositionMapper instance ID = " + GetInstanceID());
    }

    private void RegisterObjects(string[] names)
    {
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (string letter in names)
        {
            string targetName = yearPrefix + letter;
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
            if (pair.Value == null) continue;

            Vector3 currentPos = pair.Value.transform.position;
            Vector3 oldPos = positionMap[pair.Key];

            // ★ 座標が変わったときだけログを出す
            if (currentPos != oldPos)
            {
                Debug.Log($"{pair.Key} 座標更新: {oldPos} → {currentPos}");
            }

            positionMap[pair.Key] = currentPos;
        }
    }

    public Vector3 GetPosition(string fullName)
    {
        if (positionMap.ContainsKey(fullName))
            return positionMap[fullName];

        Debug.LogWarning($"{fullName} の座標は登録されていません");
        return Vector3.zero;
    }

    public Vector3 GetPositionAuto(string letter)
    {
        string key = yearPrefix + letter;

        if (positionMap.ContainsKey(key))
            return positionMap[key];

        Debug.LogWarning($"{key} の座標は登録されていません");
        return Vector3.zero;
    }

}
