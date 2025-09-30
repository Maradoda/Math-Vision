//テストプログラム
//取得した座標の上にキューブを配置

using UnityEngine;

public class CubeSpawnerByGrid : MonoBehaviour
{
    private string[,] nameGrid = new string[2, 4]
    {
        { "A", "B", "C", "D" },
        { "E", "F", "G", "H" }
    };

    private GameObject targetA;
    private GameObject cube;

    void Start()
    {
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        // "A" の GameObject を探す
        for (int i = 0; i < nameGrid.GetLength(0); i++)
        {
            for (int j = 0; j < nameGrid.GetLength(1); j++)
            {
                if (nameGrid[i, j] == "A")
                {
                    foreach (GameObject obj in allObjects)
                    {
                        if (obj.name == "A")
                        {
                            targetA = obj;
                            break;
                        }
                    }
                }
            }
        }

        if (targetA != null)
        {
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // 小さめに
        }
        else
        {
            Debug.LogWarning("名前 'A' のオブジェクトが見つかりませんでした");
        }
    }

    void Update()
    {
        if (targetA != null && cube != null)
        {
            cube.transform.position = targetA.transform.position + new Vector3(0, 1, 0); // Aの上に浮かせる
        }
    }
}
