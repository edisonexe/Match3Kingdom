using DG.Tweening;
using Save;
using SceneLoader;
using UnityEngine;
using VContainer.Unity;

namespace Boot
{
    public class BootEntryPoint : IInitializable
    {
        private IAsyncSceneLoading _sceneLoading;
        private SaveProgress _saveProgress;
        
        public BootEntryPoint(IAsyncSceneLoading sceneLoading, SaveProgress saveProgress)
        {
            _sceneLoading = sceneLoading;
            _saveProgress = saveProgress;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);
            _saveProgress.LoadData();
            await _sceneLoading.LoadAsync(Scenes.MENU);
        }
    }
}