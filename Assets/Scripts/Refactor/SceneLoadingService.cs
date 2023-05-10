using System;
using System.Collections;
using Refactor.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Refactor
{
    public class SceneLoadingService
    {
        private ICoroutineRunner _coroutineRunner;
        private LoadingCurtain _curtain;
        
        public void Init(LoadingCurtain curtain, ICoroutineRunner coroutineRunner)
        {
            _curtain = curtain;
            _coroutineRunner = coroutineRunner;
        }

        public void SetView(LoadingCurtain view)
        {
            _curtain = view;
        }

        public void Load(string name, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }

        private IEnumerator LoadScene(string nextScene, Action OnLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                OnLoaded?.Invoke();
                yield break;
            }

            var waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
            {
                _curtain.UpdateProgress(waitNextScene.progress);

                yield return null;
            }

            OnLoaded?.Invoke();
        }

        
    }
}