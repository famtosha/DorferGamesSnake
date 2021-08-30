using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneLoader : MonoBehaviour
{
    private readonly int _isOpenedHash = Animator.StringToHash("Hide");

    private Animator _animator;
    private AsyncOperation _loadOperation;

    public event Action SceneLoaded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void LoadScene(int buildIndex)
    {
        if (_loadOperation != null)
        {
            Debug.LogWarning($"Scene already loading");
            return;
        }
        _loadOperation = SceneManager.LoadSceneAsync(buildIndex);
        BlockLoad();
        OpenLoadScreen();
        _loadOperation.completed += OnSceneLoaded;
    }


    private void OnSceneLoaded(AsyncOperation obj)
    {
        CloseLoadScreen();
        _loadOperation.completed -= OnSceneLoaded;
        _loadOperation = null;
    }

    private void OpenLoadScreen()
    {
        _animator.SetBool(_isOpenedHash, true);
        StartCoroutine(WaitLoadAnimation());
        IEnumerator WaitLoadAnimation()
        {
            yield return new WaitForSeconds(0.5f);
            ResumeLaod();
            SceneLoaded?.Invoke();
        }
    }

    public void BlockLoad()
    {
        _loadOperation.allowSceneActivation = false;
    }

    public void ResumeLaod()
    {
        _loadOperation.allowSceneActivation = true;
    }

    public void ReloadActiveScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        LoadScene(currentScene.buildIndex);
    }

    public void LoadNextLevel()
    {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            LoadScene(currentScene.buildIndex + 1);
        }
        else
        {
            LoadScene(0);
        }
    }

    private void CloseLoadScreen()
    {
        _animator.SetBool(_isOpenedHash, false);
    }
}
