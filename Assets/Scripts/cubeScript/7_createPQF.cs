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

        // --- 1枚目の頂点 ---
        Vector3[] v1 = new Vector3[] { P_v, F_v, H_v };

        // --- 2枚目の頂点を 0.01 だけずらす ---
        Vector3 offset = new Vector3(0, 0, 0.01f);
        Vector3[] v2 = new Vector3[]
        {
            P_v + offset,
            F_v + offset,
            H_v + offset
        };

        // --- 頂点をまとめる ---
        Vector3[] finalVertices = new Vector3[]
        {
            v1[0], v1[1], v1[2], // 0,1,2
            v2[0], v2[1], v2[2]  // 3,4,5
        };

        mesh.Clear();
        mesh.vertices = finalVertices;

        // --- 2つの三角形を配置 ---
        mesh.triangles = new int[]
        {
            0, 1, 2, // 1枚目（元の面）
            3, 5, 4  // 2枚目（0.01手前にずらした面）
        };

        mesh.RecalculateNormals();
    }
}
