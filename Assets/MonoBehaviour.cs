using UnityEngine;

public class Sample : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        // 頂点座標を表示する
        foreach (Vector3 vertex in vertices)
        {
            // ローカル座標を表示
            Debug.Log("ローカル座標: " + vertex);

            // ワールド座標に変換して表示
            Vector3 worldPos = transform.TransformPoint(vertex);
            Debug.Log("ワールド座標: " + worldPos);
        }
    }
} 