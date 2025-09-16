//三角形PFHを描写

using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class createPQF : MonoBehaviour
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

        // 各頂点を取得（"7_P", "7_Q", "7_F"）
        Vector3 P_v = mapper.GetPosition("7_P");
        Vector3 F_v = mapper.GetPosition("7_F");
        Vector3 H_v = mapper.GetPosition("7_H");

        // 頂点配列を構成
        Vector3[] vertices = new Vector3[] { P_v, F_v, H_v };

        mesh.Clear();
        mesh.vertices = vertices;

        // 四角錐 P-ADEH を描画
        mesh.triangles = new int[]
        {
            0, 1, 2, // P-F-H
        };

        mesh.RecalculateNormals();
    }
}
