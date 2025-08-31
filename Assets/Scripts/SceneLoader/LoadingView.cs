using UnityEngine;

namespace SceneLoader
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScene;

        public void SetActiveScreen(bool value) => _loadingScene.SetActive(value);
    }
}