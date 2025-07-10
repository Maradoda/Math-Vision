using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PyramidWithVertexLogger : MonoBehaviour
{
    private Mesh mesh;
    private float t = 0f;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        // 四角錐の底面の4点（XY平面）
        Vector3 v1 = new Vector3(0, 0, 0);
        Vector3 v2 = new Vector3(4, 0, 0);
        Vector3 v3 = new Vector3(4, 4, 0);
        Vector3 v4 = new Vector3(0, 4, 0);

        // 頂点P：XY上を(0,0,4)→(6,6,4)まで移動
        Vector3 p = new Vector3(t, t, 4);

        // 頂点配列に格納
        Vector3[] vertices = new Vector3[] { v1, v2, v3, v4, p };
        mesh.Clear();
        mesh.vertices = vertices;

        // 三角形構成
        mesh.triangles = new int[]
        {
            0, 1, 4, // 側面1
            1, 2, 4,
            2, 3, 4,
            3, 0, 4,

            0, 1, 2, // 底面1
            2, 3, 0  // 底面2
        };

        mesh.RecalculateNormals();

        // 点Pの移動
        t += Time.deltaTime;
        if (t > 6f) t = 0f;
        
        // ワールド座標で頂点を表示
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(vertices[i]);
            Debug.Log($"頂点[{i}]のワールド座標: {worldPos.ToString("F2")}");
        }
    }
}