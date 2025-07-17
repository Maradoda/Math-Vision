using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

// 年度ごとのデータをまとめるクラス
[System.Serializable]
public class YearQuestionData
{
    public string yearIdentifier;
    public List<Sprite> questionImages;
    public List<string> questionAnswers; // 各問題の答え
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // 全ての年度のデータを保持するリスト
    public List<YearQuestionData> allYearData;

    // 現在選択されている年度のデータ
    public YearQuestionData currentYearData { get; private set; }

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
    
    // 識別子をもとに、現在の年度データを設定する
    public void SetCurrentYear(string identifier)
    {
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

    // --- シーンを読み込むための関数 ---
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