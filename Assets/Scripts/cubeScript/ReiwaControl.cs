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
    public ControlPQF6Button controlPQF_R6;

    [Header("令和5年（未実装）")]
    public MonoBehaviour controlP_R5_placeholder;
    public MonoBehaviour controlQ_R5_placeholder;

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
                button1.onClick.AddListener(controlP_ABED_R6.OnClick);
                button2.onClick.AddListener(controlPQF_R6.OnClick);
                break;

            case "R5":
                Debug.Log("→ 令和5年モード（未実装）");
                button1.onClick.AddListener(() => Debug.Log("R5: Button1（未実装）"));
                button2.onClick.AddListener(() => Debug.Log("R5: Button2（未実装）"));
                break;

            default:
                Debug.LogWarning("未知の年度ID: " + id);
                break;
        }
    }
}

