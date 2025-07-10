using UnityEngine;
using TMPro; // TextMeshProを扱うために必要

public class ContentPageController : MonoBehaviour
{
    // Inspectorから設定するYear_TitleのTextオブジェクト
    public TMP_Text yearTitleText;

    void Start()
    {
        // GameManagerが存在し、年度データが設定されていれば、それを表示する
        if (GameManager.Instance != null && yearTitleText != null)
        {
            yearTitleText.text = GameManager.Instance.selectedYear;
        }
    }

    // トップページへ戻るボタン
    public void OnClickTopButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadTopPageScene();
        }
    }

    // 問題ページへ移動するボタン
    public void OnClickQuestionButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadQuestionScene();
        }
    }

    // 解説ページへ移動するボタン
    public void OnClickAnswerButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadAnswerScene();
        }
    }
}