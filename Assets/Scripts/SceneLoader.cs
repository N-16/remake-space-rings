using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class SceneLoader : MonoBehaviour{
    public void LoadScene(string sceneName, Action onExit = null){
        StartCoroutine(SceneLoadRoutine(sceneName, onExit));
    }
    public void UnloadScene(string sceneName, Action onExit = null){
        StartCoroutine(UnloadSceneLoadRoutine(sceneName, onExit));
    }

    IEnumerator SceneLoadRoutine(string sceneName, Action onExit = null){
        AsyncOperation syncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while(!syncOperation.isDone){
            yield return null;
        }
        onExit?.Invoke();        
    }
    IEnumerator UnloadSceneLoadRoutine(string sceneName, Action onExit = null){
        AsyncOperation syncOperation = SceneManager.UnloadSceneAsync(sceneName);

        while(!syncOperation.isDone){
            yield return null;
        }
        onExit?.Invoke();        
    }
}
