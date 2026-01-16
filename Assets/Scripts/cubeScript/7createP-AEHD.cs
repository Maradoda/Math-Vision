//三角錐P-AEHDを描写

using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MovingPyramidLoop : MonoBehaviour
{
    private Mesh mesh;
    private GridPositionMapper mapper;

    void Start()
    {
        this.enabled = false; // 最初は無効
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

        // 頂点を取得
        Vector3 A = mapper.GetPosition("7_A");
        Vector3 D = mapper.GetPosition("7_D");
        Vector3 E = mapper.GetPosition("7_E");
        Vector3 H = mapper.GetPosition("7_H");
        Vector3 P = mapper.GetPosition("7_P");

        Vector3[] vertices = new Vector3[] { A, D, E, H, P };

        mesh.Clear();
        mesh.vertices = vertices;

        // 三角錐 P–A–D–E–H の面
        mesh.triangles = new int[]
        {
            // 🔹表にしたい3面（ADE, AEP, AHP）
            0, 4, 2, // A–E–P（AEP面）
            2, 4, 3, // E–P–H（AHP面）
            0, 1, 4, // A–P–D（ADP面）

            // 🔹その他の側面・底面
            1, 3, 4, // D–H–P（裏）
            2, 1, 0, // 底面 A–D–E
            2, 3, 1  // 底面 E–H–D
        };

        mesh.RecalculateNormals();
    }
}
