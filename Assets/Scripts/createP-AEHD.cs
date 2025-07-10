//三角錐P-AEHDを描写

using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MovingPyramidLoop : MonoBehaviour
{
    private Mesh mesh;
    private GridPositionMapper mapper;

    void Start()
    {
        this.enabled = false; // 自分自身を最初に無効化
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

        // 各頂点を取得（"7_A", "7_D", "7_E", "7_H", "7_P"）
        Vector3 A_v = mapper.GetPosition("7_A");
        Vector3 D_v = mapper.GetPosition("7_D");
        Vector3 E_v = mapper.GetPosition("7_E");
        Vector3 H_v = mapper.GetPosition("7_H");
        Vector3 P_v = mapper.GetPosition("7_P");

        // 頂点配列を構成
        Vector3[] vertices = new Vector3[] { A_v, D_v, E_v, H_v, P_v };

        mesh.Clear();
        mesh.vertices = vertices;

        // 四角錐 P-ADEH を描画
        mesh.triangles = new int[]
        {
            0, 1, 4, // A-D-P
            1, 3, 4, // D-H-P
            2, 3, 4, // E-H-P
            0, 2, 4, // H-A-P
            0, 1, 2, // 底面 A-D-E
            2, 3, 1  // 底面 E-H-D（補完）
        };

        mesh.RecalculateNormals();
    }
}
