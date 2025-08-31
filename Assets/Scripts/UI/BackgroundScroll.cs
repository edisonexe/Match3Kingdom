using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(RawImage))]
    public class BackgroundScroll : MonoBehaviour
    {
        [SerializeField] private float _scrollSpeed = 0.007f;
        [SerializeField] private float _xDirection = 1f;
        [SerializeField] private float _yDirection = 0f;
        private RawImage _bgImage;
        private Vector2 _uvRectSize;

        private async void Awake()
        {
            _bgImage = GetComponent<RawImage>();
            _uvRectSize = _bgImage.uvRect.size;
            await Scroll().SuppressCancellationThrow();
        }

        private async UniTask Scroll()
        {
            while (destroyCancellationToken.IsCancellationRequested == false)
            {
                _bgImage.uvRect = new Rect(
                    _bgImage.uvRect.position +
                    new Vector2(_xDirection * _scrollSpeed, _yDirection * _scrollSpeed) *
                    Time.deltaTime, _uvRectSize);
                await UniTask.Yield(PlayerLoopTiming.Update, destroyCancellationToken);
            }
        }
    }
}