using Cysharp.Threading.Tasks;

namespace SceneLoader
{
    public interface IAsyncSceneLoading
    {
        UniTask LoadAsync(string sceneName);
        UniTask UnloadAsync(string sceneName);
        void LoadingIsDone(bool value);
    }
}