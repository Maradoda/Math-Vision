using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ContentPageController : MonoBehaviour
{
    // --- 共通UI ---
    public TMP_Text yearTitleText;

    // --- 問題ページ専用UI ---
    public Image questionImageDisplay;
    public List<Button> questionButtons;
    public List<TMP_InputField> inputFields;
    public Button scoringButton;
    public List<Image> individualResultImages; // 個別の採点結果を表示するImageのリスト
    public Sprite correctSprite;
    public Sprite incorrectSprite;

    // --- 起動時に実行 ---
    void Start()
    {
        // GameManagerと現在の年度データを取得
        var currentData = GameManager.Instance?.currentYearData;
        if (currentData == null)
        {
            Debug.LogError("Current Year Data is not set in GameManager!");
            return;
        }

        // 年度のタイトルを設定
        if (yearTitleText != null)
        {
            string yearNumber = currentData.yearIdentifier.Substring(1); // "R"を取り除く
            yearTitleText.text = $"令和{yearNumber}年度";
        }

        // 問題の画像表示エリアを初期状態では非表示（透明）にする
        if (questionImageDisplay != null)
        {
            questionImageDisplay.color = Color.clear;
        }

        // 問題ページのUIセットアップ
        if (questionButtons != null && questionButtons.Count > 0)
        {
            SetupQuestionButtons(currentData);
        }

        if (inputFields != null && inputFields.Count > 0)
        {
            SetupInputFields(currentData);
        }

        // 採点ボタンにイベントを登録
        if (scoringButton != null)
        {
            scoringButton.onClick.AddListener(CheckAnswers);
        }

        // 初期状態では個別の結果画像を非表示に
        if (individualResultImages != null)
        {
            foreach (var img in individualResultImages)
            {
                img.gameObject.SetActive(false);
            }
        }
    }

    // 問題ボタンをセットアップする
    void SetupQuestionButtons(YearQuestionData data)
    {
        foreach (var btn in questionButtons)
        {
            btn.gameObject.SetActive(false);
        }

        for (int i = 0; i < data.questionImages.Count; i++)
        {
            if (i >= questionButtons.Count) break;

            Button button = questionButtons[i];
            Sprite imageToShow = data.questionImages[i];

            button.gameObject.SetActive(true);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                if (questionImageDisplay != null)
                {
                    questionImageDisplay.sprite = imageToShow;
                    // 透明にした色を白（不透明）に戻して表示
                    questionImageDisplay.color = Color.white;
                }
            });
        }
    }

    // 入力フィールドをセットアップする
    void SetupInputFields(YearQuestionData data)
    {
        foreach (var field in inputFields)
        {
            field.gameObject.SetActive(false);
        }

        for (int i = 0; i < data.questionAnswers.Count; i++)
        {
            if (i < inputFields.Count)
            {
                inputFields[i].gameObject.SetActive(true);
                inputFields[i].text = "";
            }
        }
    }

    // 答えを個別にチェックする
    public void CheckAnswers()
    {
        var currentData = GameManager.Instance?.currentYearData;
        if (currentData == null) return;

        for (int i = 0; i < currentData.questionAnswers.Count; i++)
        {
            if (i >= inputFields.Count || i >= individualResultImages.Count) break;

            string userAnswer = inputFields[i].text.Trim();
            string correctAnswer = currentData.questionAnswers[i];
            Image resultImg = individualResultImages[i];

            resultImg.gameObject.SetActive(true);

            if (userAnswer == correctAnswer)
            {
                resultImg.sprite = correctSprite;
            }
            else
            {
                resultImg.sprite = incorrectSprite;
            }
        }
    }

    // --- シーン遷移 ---
    public void OnClickTopButton()
    {
        GameManager.Instance?.LoadTopPageScene();
    }

    public void OnClickQuestionButton()
    {
        GameManager.Instance?.LoadQuestionScene();
    }

    public void OnClickAnswerButton()
    {
        GameManager.Instance?.LoadAnswerScene();
    }
}