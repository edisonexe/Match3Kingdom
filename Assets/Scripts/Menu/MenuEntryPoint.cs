using Menu.Levels;
using SceneLoader;
using VContainer.Unity;

namespace Menu
{
    public class MenuEntryPoint : IInitializable
    {
        private IAsyncSceneLoading _sceneLoading;
        private SetupLevelSequence _setupLevel;

        public MenuEntryPoint(IAsyncSceneLoading sceneLoading, SetupLevelSequence setupLevel)
        {
            _sceneLoading = sceneLoading;
            _setupLevel = setupLevel;
        }

        public async void Initialize()
        {
            await _setupLevel.Setup(1);
            // play music menu
            _sceneLoading.LoadingIsDone(true);
            // await animation
            // button enabled?
        }
    }
}