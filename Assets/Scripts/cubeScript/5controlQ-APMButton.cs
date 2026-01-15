using UnityEngine;

public class controlP_APMQButton : MonoBehaviour
{
    public GameObject Q_APM_Object;     // MovingPyramidLoop5 が付いたオブジェクト
    public GameObject pointPObject;      // 5_P（必要なら）

    public void OnClick()
    {
        // ② 点P（今回は骨組みなので特別な移動はしない）
        if (pointPObject != null)
        {
            // 必要になったらここに処理を書く
            Debug.Log("点Pは現状位置のまま（R5・未実装）");
        }

        // ③ 三角錐 A-P-M-Q を有効化 ＋ 表示
        if (Q_APM_Object != null)
        {
            var pyramid = Q_APM_Object.GetComponent<MovingPyramidLoop5>();
            if (pyramid != null) pyramid.enabled = true;

            Q_APM_Object.SetActive(true);
            Debug.Log("三角錐 A-P-M-Q（R5）を有効化＆表示しました");
        }
    }
}
