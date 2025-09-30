//ボタン押下でcreateP-AEHD.csを有効にMovePointP.csを有効

using UnityEngine;

public class controlP_AEHDButton : MonoBehaviour
{
    public GameObject P_AEHDObject;         // createP-AEHD (MovingPyramidLoop) がアタッチされたオブジェクト
    public GameObject pointPObject;         // 「7_P」オブジェクト（MovePointP がアタッチ）
    public GameObject createPQFObject;      // createPQF がアタッチされたオブジェクト

    public void OnClick()
    {
        // ① createPQF を無効化 ＋ オブジェクト非表示
        if (createPQFObject != null)
        {
            var pqf = createPQFObject.GetComponent<createPQF>();
            if (pqf != null) pqf.enabled = false;

            createPQFObject.SetActive(false);
            Debug.Log("createPQF を無効化＆非表示にしました");
        }

        // ② 点P を A の位置に戻す
        if (pointPObject != null)
        {
            GridPositionMapper mapper = FindObjectOfType<GridPositionMapper>();
            if (mapper != null)
            {
                Vector3 A = mapper.GetPosition("7_A");
                pointPObject.transform.position = A;
                Debug.Log("点Pの位置を A に戻しました");
            }
            else
            {
                Debug.LogWarning("GridPositionMapper が見つかりませんでした");
            }
        }

        // ③ createP-AEHD を有効化 ＋ オブジェクト表示
        if (P_AEHDObject != null)
        {
            var ae = P_AEHDObject.GetComponent<MovingPyramidLoop>();
            if (ae != null) ae.enabled = true;

            P_AEHDObject.SetActive(true);
            Debug.Log("createP-AEHD を有効化＆表示しました");
        }

        // ④ MovePointP を有効化
        if (pointPObject != null)
        {
            var mover = pointPObject.GetComponent<StepMovePointP>();
            if (mover != null)
            {
                mover.enabled = true;
                Debug.Log("StepMovePointP を有効化しました");
            }
        }
    }
}
