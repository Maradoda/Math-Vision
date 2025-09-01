// Assets/Editor/BuildScript.cs
using UnityEditor;
using UnityEngine;
using System;
using System.Linq; // LINQを使うために必要

public class BuildScript
{
    public static void PerformAndroidBuild()
    {
        // --- 1. Build Settingsから有効なシーンをすべて取得 ---
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled) // チェックが入っているシーンのみを対象にする
            .Select(scene => scene.path)   // シーンのパスを取得する
            .ToArray();                     // 配列に変換する

        // もしビルドすべきシーンが一つもなければ、エラーを出して終了
        if (scenes.Length == 0)
        {
            Debug.LogError("No scenes were found in the build settings. Please add at least one scene to the build settings.");
            EditorApplication.Exit(1); // CIジョブを失敗させる
            return;
        }

        Debug.Log("Building scenes: " + string.Join(", ", scenes));


        // --- 2. ビルド設定（キーストアなど） ---
        // (この部分は以前のコードと同じ)
        string keystorePass = Environment.GetEnvironmentVariable("KEYSTORE_PASS");
        string keyAliasName = Environment.GetEnvironmentVariable("KEY_ALIAS_NAME");
        string keyAliasPass = Environment.GetEnvironmentVariable("KEY_ALIAS_PASS");

        if (!string.IsNullOrEmpty(keystorePass))
        {
            PlayerSettings.Android.keystoreName = "MyProject.keystore";
            PlayerSettings.Android.keystorePass = keystorePass;
            PlayerSettings.Android.keyaliasName = keyAliasName;
            PlayerSettings.Android.keyaliasPass = keyAliasPass;
        }

        EditorUserBuildSettings.buildAppBundle = false;
        string outputPath = "Builds/maradoda.apk";
        

        // --- 3. ビルドの実行 ---
        var buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes, // ここで自動取得したシーンの配列を使う
            locationPathName = outputPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded!");
        }
        else if (summary.result == BuildResult.Failed)
        {
            Debug.LogError("Build failed");
            EditorApplication.Exit(1);
        }
    }
}