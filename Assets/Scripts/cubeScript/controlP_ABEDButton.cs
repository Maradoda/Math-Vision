using UnityEngine;

public class controlP_ABEDButton : MonoBehaviour
{
    public GameObject P_ABED_Object;     // createP-ABED (MovingPyramidLoop6) がアタッチされたオブジェクト
    public GameObject pointPObject;      // 「6_P」オブジェクト（MovePointP がアタッチ）
    public GameObject createPQF6Object;  // createPQF6 がアタッチされたオブジェクト

    public void OnClick()
    {
        // PQF6 を無効化
        if (createPQF6Object != null)
        {
            var pqf = createPQF6Object.GetComponent<createPQF6>();
            if (pqf != null) pqf.enabled = false;
            createPQF6Object.SetActive(false);
        }

        // P-ABED を有効化
        if (P_ABED_Object != null)
        {
            var abed = P_ABED_Object.GetComponent<MovingPyramidLoop6>();
            if (abed != null) abed.enabled = true;

            P_ABED_Object.SetActive(true);
        }
    }
}
