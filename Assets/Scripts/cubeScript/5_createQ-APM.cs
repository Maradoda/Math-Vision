// 三角錐 A-P-M-Q を描写（令和5年対応）
// 三角錐 A-P-M-Q を描写（令和5年対応）
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MovingPyramidLoop5 : MonoBehaviour
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

        Debug.Log("MovingPyramidLoop5 mapper ID = " + mapper.GetInstanceID());
    }

    void Update()
    {
        if (mapper == null) return;

        // --- 頂点取得（5年版）---
        Vector3 A = mapper.GetPosition("5_A");
        Vector3 P = mapper.GetPosition("5_P");
        Vector3 M = mapper.GetPosition("5_M");
        Vector3 Q = mapper.GetPosition("5_Q");

        // ★ Aの座標を毎フレーム表示

        Vector3[] vertices = new Vector3[] { A, P, M, Q };

        mesh.Clear();
        mesh.vertices = vertices;

        // --- 三角錐 A-P-M-Q の面 ---
        mesh.triangles = new int[]
        {
            // 🔹側面
            0, 2, 1, // A-M-P
            0, 1, 3, // A-P-Q
            0, 3, 2, // A-Q-M

            // 🔹底面（P-M-Q）
            1, 2, 3
        };

        mesh.RecalculateNormals();
    }
}
