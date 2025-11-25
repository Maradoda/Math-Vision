using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ContentPageController : MonoBehaviour
{
    // --- 共通UI ---
    public TMP_Text yearTitleText;
    public TMP_Text questionBodyText;

    // --- 問題ページ専用UI ---
    public Image questionImageDisplay;
    public List<Button> questionButtons;
    public List<TMP_InputField> inputFields;
    public Button scoringButton;
    public List<Image> individualResultImages;
    public Sprite correctSprite;
    public Sprite incorrectSprite;

    // ★★★ 解説ページ専用UI (ここを追加) ★★★
    [Header("Answer Scene UI")]
    public TMP_Text explanationTextDisplay;       // 解説文を表示する場所
    public List<Button> explanationQuestionButtons; // 「問題1」「問題2」ボタン
    public List<Button> explanationStepButtons;     // 「STEP1」「STEP2」ボタン

    // 内部変数：現在どの問題を選択しているか (0=問1, 1=問2...)
    private int currentExplanationIndex = 0;
    private YearQuestionData currentData;

    // --- 起動時に実行 ---
    void Start()
    {
        currentData = GameManager.Instance?.currentYearData;
        if (currentData == null)
        {
            Debug.LogError("Current Year Data is not set in GameManager!");
            return;
        }

        // 年度のタイトルを設定
        if (yearTitleText != null)
        {
            string yearNumber = currentData.yearIdentifier.Substring(1);
            yearTitleText.text = $"令和{yearNumber}年度";
        }
        
        // 問題本文をUIに設定
        if (questionBodyText != null && !string.IsNullOrEmpty(currentData.questionText))
        {
            questionBodyText.text = currentData.questionText;
        }

        // --- 問題ページのセットアップ ---
        if (questionImageDisplay != null) questionImageDisplay.color = Color.clear;
        
        if (questionButtons != null && questionButtons.Count > 0)
            SetupQuestionButtons(currentData);

        if (inputFields != null && inputFields.Count > 0)
            SetupInputFields(currentData);

        if (scoringButton != null)
            scoringButton.onClick.AddListener(CheckAnswers);

        if (individualResultImages != null)
        {
            foreach (var img in individualResultImages) img.gameObject.SetActive(false);
        }

        // ★★★ 解説ページのセットアップ (ここを追加) ★★★
        if (explanationTextDisplay != null)
        {
            SetupExplanationUI();
        }
    }

    // --- 解説ページ用のセットアップ関数 ---
    void SetupExplanationUI()
    {
        // 1. 「問題」ボタンの設定
        for (int i = 0; i < explanationQuestionButtons.Count; i++)
        {
            int index = i; // クロージャ用の一時変数
            explanationQuestionButtons[i].onClick.RemoveAllListeners();
            explanationQuestionButtons[i].onClick.AddListener(() => OnExplanationQuestionSelected(index));
        }

        // 2. 「STEP」ボタンの設定
        for (int i = 0; i < explanationStepButtons.Count; i++)
        {
            int stepIndex = i; // クロージャ用の一時変数
            explanationStepButtons[i].onClick.RemoveAllListeners();
            explanationStepButtons[i].onClick.AddListener(() => OnExplanationStepSelected(stepIndex));
        }

        // 初期化：最初は「問1」が選択されている状態にする
        OnExplanationQuestionSelected(0);
    }

    // 「問題」ボタンが押されたときの処理
    void OnExplanationQuestionSelected(int index)
    {
        currentExplanationIndex = index;
        
        // 問題を切り替えたら、とりあえずSTEP1の解説を表示するか、あるいは空にする
        // ここでは STEP1 (index 0) を自動表示するようにしています
        OnExplanationStepSelected(0);

        Debug.Log($"問題 {index + 1} が選択されました");
    }

    // 「STEP」ボタンが押されたときの処理
    void OnExplanationStepSelected(int stepIndex)
    {
        if (currentData.explanationList == null || currentData.explanationList.Count <= currentExplanationIndex)
        {
            explanationTextDisplay.text = "この問題の解説データがありません。";
            return;
        }

        var targetQuestionData = currentData.explanationList[currentExplanationIndex];

        if (targetQuestionData.stepTexts != null && targetQuestionData.stepTexts.Count > stepIndex)
        {
            // 対応するSTEPの文章を表示
            explanationTextDisplay.text = targetQuestionData.stepTexts[stepIndex];
        }
        else
        {
            explanationTextDisplay.text = "このSTEPの解説はありません。";
        }
    }

    // --- (以下、既存の問題ページ用関数はそのまま) ---

    void SetupQuestionButtons(YearQuestionData data)
    {
        foreach (var btn in questionButtons) btn.gameObject.SetActive(false);

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
                    questionImageDisplay.color = Color.white;
                }
            });
        }
    }

    void SetupInputFields(YearQuestionData data)
    {
        foreach (var field in inputFields) field.gameObject.SetActive(false);

        for (int i = 0; i < data.questionAnswers.Count; i++)
        {
            if (i < inputFields.Count)
            {
                inputFields[i].gameObject.SetActive(true);
                inputFields[i].text = "";
            }
        }
    }

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
                resultImg.sprite = correctSprite;
            else
                resultImg.sprite = incorrectSprite;
        }
    }

    public void OnClickTopButton() { GameManager.Instance?.LoadTopPageScene(); }
    public void OnClickQuestionButton() { GameManager.Instance?.LoadQuestionScene(); }
    public void OnClickAnswerButton() { GameManager.Instance?.LoadAnswerScene(); }
}