using UnityEngine;

public class ControlPQF6Button : MonoBehaviour
{
    public GameObject P_ABED_Object;   // R6のP-ABED
    public GameObject createPQF6Object;
    public GameObject pointPObject;

    public void OnClick()
    {
        // P-ABED 無効化
        if (P_ABED_Object != null)
        {
            var abed = P_ABED_Object.GetComponent<MovingPyramidLoop6>();
            if (abed != null) abed.enabled = false;
            P_ABED_Object.SetActive(false);
        }

        // PQF6 有効化
        if (createPQF6Object != null)
        {
            var pqf6 = createPQF6Object.GetComponent<createPQF6>();
            if (pqf6 != null) pqf6.enabled = true;
            createPQF6Object.SetActive(true);
        }
    }
}

