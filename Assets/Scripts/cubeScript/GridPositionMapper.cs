//メッシュボール(7_A...7_H)の座標取得プログラム
//各頂点の座標取得

using UnityEngine;

public class GridPositionMapper : MonoBehaviour
{
    private string[,] nameGrid = new string[2, 4]
    {
        { "7_A", "7_B", "7_C", "7_D" },
        { "7_E", "7_F", "7_G", "7_H" }
    };

    private Vector3[,] positionGrid = new Vector3[2, 4];
    private GameObject[,] objectGrid = new GameObject[2, 4];

    private string extraName = "7_P";
    private GameObject objectP;
    private Vector3 positionP;

    void Start()
    {
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        for (int i = 0; i < nameGrid.GetLength(0); i++)
        {
            for (int j = 0; j < nameGrid.GetLength(1); j++)
            {
                string targetName = nameGrid[i, j];

                foreach (GameObject obj in allObjects)
                {
                    if (obj.name == targetName)
                    {
                        objectGrid[i, j] = obj;
                        Debug.Log($"[{i},{j}] {targetName} を登録: 初期位置 {obj.transform.position}");
                        break;
                    }
                }

                if (objectGrid[i, j] == null)
                {
                    Debug.LogWarning($"[{i},{j}] 名前「{targetName}」のオブジェクトが見つかりませんでした");
                }
            }
        }

        // 7_Pの登録
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == extraName)
            {
                objectP = obj;
                Debug.Log($"7_P を登録: 初期位置 {obj.transform.position}");
                break;
            }
        }

        if (objectP == null)
        {
            Debug.LogWarning("名前「7_P」のオブジェクトが見つかりませんでした");
        }
    }

    void Update()
    {
        for (int i = 0; i < objectGrid.GetLength(0); i++)
        {
            for (int j = 0; j < objectGrid.GetLength(1); j++)
            {
                GameObject obj = objectGrid[i, j];

                if (obj != null)
                {
                    positionGrid[i, j] = obj.transform.position;

                    if (Time.frameCount % 30 == 0)
                        Debug.Log($"[{i},{j}] {obj.name} の現在位置: {positionGrid[i, j]}");
                }
            }
        }

        if (objectP != null)
        {
            positionP = objectP.transform.position;

            if (Time.frameCount % 30 == 0)
                Debug.Log($"7_P の現在位置: {positionP}");
        }
    }

    public Vector3 GetPosition(string name)
    {
        for (int i = 0; i < nameGrid.GetLength(0); i++)
        {
            for (int j = 0; j < nameGrid.GetLength(1); j++)
            {
                if (nameGrid[i, j] == name)
                {
                    return positionGrid[i, j];
                }
            }
        }

        if (name == extraName)
        {
            return positionP;
        }

        Debug.LogWarning($"名前 {name} の座標が見つかりませんでした。Vector3.zero を返します。");
        return Vector3.zero;
    }
}
