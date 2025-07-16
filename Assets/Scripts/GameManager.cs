using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; // Listを扱うために必要
using System.Linq; // Linqを扱うために必要

// [System.Serializable] をつけることで、Inspector上に表示できるようになる
[System.Serializable]
public class YearQuestionData
{
    // 年度を識別するための文字列（例: "R7", "R6"）
    public string yearIdentifier;
    // その年度で使う問題図形のスプライトのリスト
    public List<Sprite> questionImages;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // 全ての年度の問題データを保持するリスト
    public List<YearQuestionData> allYearData;

    // 現在選択されている年度のデータ
    public YearQuestionData currentYearData { get; private set; }

    // Awakeメソッドは変更なし
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // ★★★ 新しく追加/変更する関数 ★★★
    // 渡された識別子（"R7"など）をもとに、現在の年度データを設定する
    public void SetCurrentYear(string identifier)
    {
        // allYearDataの中から、識別子が一致するものを探す
        currentYearData = allYearData.FirstOrDefault(data => data.yearIdentifier == identifier);

        if (currentYearData != null)
        {
            Debug.Log("Current year set to: " + identifier);
        }
        else
        {
            Debug.LogError("Data for year identifier '" + identifier + "' not found!");
        }
    }

    // --- シーンを読み込むための関数（変更なし） ---
    public void LoadTopPageScene()
    {
        SceneManager.LoadScene("TopPage_scene");
    }

    public void LoadQuestionScene()
    {
        SceneManager.LoadScene("Question_scene");
    }

    public void LoadAnswerScene()
    {
        SceneManager.LoadScene("Answer_scene");
    }
}