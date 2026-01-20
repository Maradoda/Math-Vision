using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class createPQF6 : MonoBehaviour
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

        // R6用の座標
        Vector3 P_v = mapper.GetPosition("6_P");
        Vector3 Q_v = mapper.GetPosition("6_Q");
        Vector3 F_v = mapper.GetPosition("6_F");

        // 頂点配列
        Vector3[] vertices = new Vector3[]
        {
            P_v,
            Q_v + new Vector3(0, 0.01f, 0), // 少し持ち上げて重なり防止
            F_v
        };

        mesh.Clear();
        mesh.vertices = vertices;

        mesh.triangles = new int[]
        {
            0, 1, 2,
            1, 0, 2
        };

        mesh.RecalculateNormals();
    }
}
