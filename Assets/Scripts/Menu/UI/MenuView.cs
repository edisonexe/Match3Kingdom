using System;
using System.Collections.Generic;
using System.Threading;
using Animations;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Menu.UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private RectTransform _leftTower;
        [SerializeField] private RectTransform _rightTower;
        [SerializeField] private RectTransform _wall;
        [SerializeField] private RectTransform _logo;
        [SerializeField] private List<GameObject> _levelButtons = new List<GameObject>();
        private CancellationTokenSource _cts;
        //audio
        private IAnimation _animation;


        public async UniTask StartAnimation()
        {
            _cts = new CancellationTokenSource();
            // play audio
            _animation.MoveUI(_leftTower.gameObject, new Vector3(-814, -70, 0), 0.3f, Ease.InOutBack);
            _animation.MoveUI(_rightTower.gameObject, new Vector3(575, -549, 0), 0.4f, Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.4f), _cts.IsCancellationRequested);
            _animation.MoveUI(_wall.gameObject, new Vector3(0, 41, 0), 0.3f, Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), _cts.IsCancellationRequested);
            _animation.MoveUI(_logo.gameObject, new Vector3(-130, 622, 0), 0.7f, Ease.OutBounce);
            await UniTask.Delay(TimeSpan.FromSeconds(0.7f), _cts.IsCancellationRequested);
            foreach (var button in _levelButtons)
            {
                //play sound
                button.SetActive(true);
                await _animation.Reveal(button, 0.1f);
            }

            
        }
        
        [Inject] private void Construct(IAnimation animation)
        {
            _animation = animation;
        }
    }
}