using UnityEngine;
using UnityEngine.UI;

public class ReiwaController : MonoBehaviour
{
    [Header("対象ボタンをセット")]
    public Button button1;   // P系ボタン
    public Button button2;   // PQF系ボタン

    [Header("令和7年のスクリプト")]
    public controlP_AEHDButton controlP_AEHD_R7;
    public ControlPQFButton controlPQF_R7;

    [Header("令和6年のスクリプト")]
    public controlP_ABEDButton controlP_ABED_R6;
    public ControlViewTop controlViewTop_R6;

    [Header("令和5年（未実装）")]
    public StepMovePointPQ_ByVariables StepMovePointPQ_R5;
    public controlP_APMQButton controlP_APMQ_R5;

    void Start()
    {
        // GameManager から年度データ取得
        var currentData = GameManager.Instance?.currentYearData;

        if (currentData == null)
        {
            Debug.LogError("GameManager.currentYearData が設定されていません。");
            return;
        }

        // 年度ID取得
        string id = currentData.yearIdentifier;
        Debug.Log("年度ID = " + id);

        // ボタンのリスナーを初期化
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        // 年度別分岐
        switch (id)
        {
            case "R7":
                Debug.Log("→ 令和7年モード");
                button1.onClick.AddListener(controlP_AEHD_R7.OnClick);
                button2.onClick.AddListener(controlPQF_R7.OnClick);
                break;

            case "R6":
                Debug.Log("→ 令和6年モード");
                button1.onClick.AddListener(controlViewTop_R6.OnClick);
                button2.onClick.AddListener(controlP_ABED_R6.OnClick);
                break;

            case "R5":
                Debug.Log("→ 令和5年モード");
                button1.onClick.AddListener(StepMovePointPQ_R5.OnClick);
                button2.onClick.AddListener(controlP_APMQ_R5.OnClick);
                break;

            default:
                Debug.LogWarning("未知の年度ID: " + id);
                break;
        }
    }
}
