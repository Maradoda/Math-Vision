//年度ごとの図形を表示する。

using UnityEngine;

public class ReiwaDisplayController : MonoBehaviour
{
    [Header("各年度オブジェクトをセット (R1～R7)")]
    public GameObject reiwa1;
    public GameObject reiwa2;
    public GameObject reiwa3;
    public GameObject reiwa4;
    public GameObject reiwa5;
    public GameObject reiwa6;
    public GameObject reiwa7;

    void Start()
    {
        // GameManagerのインスタンスから現在の年度データを取得
        var currentData = GameManager.Instance?.currentYearData;

        if (currentData == null)
        {
            Debug.LogError("GameManagerにcurrentYearDataが設定されていません。");
            return;
        }

        // すべて非表示にする
        HideAll();

        // 年度識別子（例："R7"）を取得
        string id = currentData.yearIdentifier;

        // 一致する年度のオブジェクトを表示
        switch (id)
        {
            case "R1":
                if (reiwa1) reiwa1.SetActive(true);
                break;
            case "R2":
                if (reiwa2) reiwa2.SetActive(true);
                break;
            case "R3":
                if (reiwa3) reiwa3.SetActive(true);
                break;
            case "R4":
                if (reiwa4) reiwa4.SetActive(true);
                break;
            case "R5":
                if (reiwa5) reiwa5.SetActive(true);
                break;
            case "R6":
                if (reiwa6) reiwa6.SetActive(true);
                break;
            case "R7":
                if (reiwa7) reiwa7.SetActive(true);
                break;
            default:
                Debug.LogWarning("対応する年度オブジェクトがありません: " + id);
                break;
        }
    }

    // 全オブジェクトを非表示にする関数
    void HideAll()
    {
        if (reiwa1) reiwa1.SetActive(false);
        if (reiwa2) reiwa2.SetActive(false);
        if (reiwa3) reiwa3.SetActive(false);
        if (reiwa4) reiwa4.SetActive(false);
        if (reiwa5) reiwa5.SetActive(false);
        if (reiwa6) reiwa6.SetActive(false);
        if (reiwa7) reiwa7.SetActive(false);
    }
}
