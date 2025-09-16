//ボタンを押したらcreatePQFを有効MOVERPを無効化

using UnityEngine;

public class ControlPQFButton : MonoBehaviour
{
    public GameObject pointPObject; // 「7_P」オブジェクト
    public GameObject pqfObject;    // createPQF がアタッチされたオブジェクト

    public GameObject p_aehdObject;

    public void OnClick()
    {
        if (pointPObject != null)
        {
            // StepMovePointP を無効化
            var mover = pointPObject.GetComponent<StepMovePointP>();
            if (mover != null)
            {
                mover.enabled = false;
                Debug.Log("MoverPointP を無効化しました");
            }

            // GridPositionMapper から A と C の座標を取得
            GridPositionMapper mapper = FindObjectOfType<GridPositionMapper>();
            if (mapper != null)
            {
                Vector3 A = mapper.GetPosition("7_A");
                Vector3 C = mapper.GetPosition("7_C");

                // P = 5(C - A)/6 + A の計算
                Vector3 P = (5f / 6f) * (C - A) + A;

                // 点Pに代入
                pointPObject.transform.position = P;
                Debug.Log("点Pの位置を計算値に設定: " + P.ToString("F2"));
            }
            else
            {
                Debug.LogWarning("GridPositionMapper が見つかりませんでした");
            }
        }

        //createP_AEHDを無効
        if (p_aehdObject != null)
        {
            var p_aehd = p_aehdObject.GetComponent<MovingPyramidLoop>();
            if (p_aehd != null)
            {
                p_aehd.enabled = false;
                Debug.Log("createP-AEHD を無効化しました");
            }
        }

        // createPQF を有効化 ＋ オブジェクト表示
        if (pqfObject != null)
        {
            var pqf = pqfObject.GetComponent<createPQF>();
            if (pqf != null) pqf.enabled = true;

            pqfObject.SetActive(true);
            Debug.Log("createPQF を有効化＆表示しました");
        }
    }
}
