using UnityEngine;
using System.Collections;

public class SceneChangeManager : MonoBehaviour
{

    public string ChangeSceneName;

    private AsyncOperation async;

    public void SceneLoad()
    {
        //非同期でシーンのロード開始
        async = Application.LoadLevelAsync(ChangeSceneName);
        StartCoroutine("LoadingScene");
    }

    private IEnumerator LoadingScene()
    {
        while (!async.isDone)
        {
            yield return null;
        }
        yield return async;
    }
}
