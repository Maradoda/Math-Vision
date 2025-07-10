using UnityEngine;

public class TopPageController : MonoBehaviour
{
    // 年度選択ボタンが押されたときに呼ばれる関数
    public void SetSelectedYear(string year)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.selectedYear = year;
            Debug.Log("Selected Year: " + year); // 正しく設定されたか確認用
        }
    }

    // 問題ボタンが押されたときに呼ばれる関数
    public void OnClickQuestionButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadQuestionScene();
        }
    }

    // 解説ボタンが押されたときに呼ばれる関数
    public void OnClickAnswerButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadAnswerScene();
        }
    }
}