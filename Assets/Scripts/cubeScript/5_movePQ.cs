using UnityEngine;

public class StepMovePointPQ_ByVariables : MonoBehaviour
{
    public enum MoveType { P, Q }
    public MoveType moveType;

    public float speed = 1f; // 1cm / 秒

    private GridPositionMapper mapper;

    private Vector3[] path;
    private int edgeIndex = 0;
    private float t = 0f;

    public controlP_APMQButton controlP_APMQ_R5;

    void Start()
    {
        enabled = false;

        mapper = FindObjectOfType<GridPositionMapper>();
        if (mapper == null)
        {
            Debug.LogError("GridPositionMapper が見つかりません");
            return;
        }

        Vector3 A = mapper.GetPositionAuto("A");
        Vector3 B = mapper.GetPositionAuto("B");
        Vector3 C = mapper.GetPositionAuto("C");
        Vector3 D = mapper.GetPositionAuto("D");

        if (moveType == MoveType.P)
        {
            path = new Vector3[] { A, B, C };
            transform.position = A;
        }
        else
        {
            path = new Vector3[] { C, D, A };
            transform.position = C;
        }
    }

    // ★ ボタン用
    public void OnClick()
    {
        Debug.Log($"R5 : 点{moveType} 移動開始");

        // PQF / APMQ を停止
        if (controlP_APMQ_R5 != null)
        {
            var pqf = controlP_APMQ_R5.GetComponent<MovingPyramidLoop5>();
            if (pqf != null) pqf.enabled = false;

            controlP_APMQ_R5.gameObject.SetActive(false);
        }

        // 初期化
        edgeIndex = 0;
        t = 0f;

        enabled = true;
    }

    void Update()
    {
        Vector3 start = path[edgeIndex];
        Vector3 end   = path[edgeIndex + 1];

        float edgeLength = Vector3.Distance(start, end);

        t += Time.deltaTime * speed / edgeLength;

        transform.position = Vector3.Lerp(start, end, t);

        if (t >= 1f)
        {
            t = 0f;
            edgeIndex++;

            if (edgeIndex >= path.Length - 1)
                edgeIndex = 0;
        }
    }
}
