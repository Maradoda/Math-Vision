using UnityEngine;

public class controlP_AEHDButton : MonoBehaviour
{
    public GameObject P_AEHDObject;     // createP-AEHD
    public GameObject pointPObject;     // 7_P
    public GameObject createPQFObject;  // createPQF
    void Start()
    {
        // ★ シーン開始時に「ボタンを押した状態」にする
        OnClick();
    }

    public void OnClick()
    {
        // ① createPQF を無効化 ＋ 非表示
        if (createPQFObject != null)
        {
            var pqf = createPQFObject.GetComponent<createPQF>();
            if (pqf != null) pqf.enabled = false;

            createPQFObject.SetActive(false);
            Debug.Log("createPQF を無効化＆非表示にしました");
        }

        // ② 点P を AとCの中点に移動
        if (pointPObject != null)
        {
            GridPositionMapper mapper = FindObjectOfType<GridPositionMapper>();
            if (mapper != null)
            {
                Vector3 posA = mapper.GetPosition("7_A");
                Vector3 posC = mapper.GetPosition("7_C");

                Vector3 midpoint = (posA + posC) / 2f;
                pointPObject.transform.position = midpoint;

                Debug.Log("点PをAとCの中点に移動しました");
            }
        }

        // ③ createP-AEHD を有効化 ＋ 表示
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
