// 四角錐 P-A-B-E-D を描写（令和6年対応）
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MovingPyramidLoop6 : MonoBehaviour
{
    private Mesh mesh;
    private GridPositionMapper mapper;

    void Start()
    {
        this.enabled = false; // 最初は無効化
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mapper = FindObjectOfType<GridPositionMapper>();
        if (mapper == null)
        {
            Debug.LogError("GridPositionMapper が見つかりません！");
        }
    }

    void Update()
    {
        if (mapper == null) return;

        // --- 頂点取得（6年版）---
        Vector3 A = mapper.GetPosition("6_A");
        Vector3 B = mapper.GetPosition("6_B");
        Vector3 E = mapper.GetPosition("6_E");
        Vector3 D = mapper.GetPosition("6_D");
        Vector3 P = mapper.GetPosition("6_P");

        Vector3[] vertices = new Vector3[] { A, B, E, D, P };

        mesh.Clear();
        mesh.vertices = vertices;

        // --- 四角錐 P-A-B-E-D の面 ---
        mesh.triangles = new int[]
        {
            // 🔹側面（表向き）
            0, 4, 1, // A-P-B
            1, 4, 2, // B-P-E
            2, 4, 3, // E-P-D
            3, 4, 0, // D-P-A

            // 🔹底面（A-B-E-D）
            0, 1, 2,
            0, 2, 3
        };

        mesh.RecalculateNormals();
    }
}
