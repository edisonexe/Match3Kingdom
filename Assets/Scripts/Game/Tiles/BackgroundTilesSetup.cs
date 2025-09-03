using System;
using System.Threading;
using Animations;
using Cysharp.Threading.Tasks;
using ResourcesLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Game.Tiles
{
    public class BackgroundTilesSetup : IDisposable
    {
        private readonly GameResourcesLoader _resourcesLoader;
        private CancellationTokenSource _cts;
        private IAnimation _animation;
        private IObjectResolver _objectResolver;

        public BackgroundTilesSetup(GameResourcesLoader resourcesLoader, IObjectResolver objectResolver,
            IAnimation animation)
        {
            _resourcesLoader = resourcesLoader;
            _objectResolver = objectResolver;
            _animation = animation;
        }

        public async UniTask SetupBackground(Transform parent, bool[,] blanks, int width, int height)
        {
            _cts = new CancellationTokenSource();
            for (int  x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (blanks[x, y]) continue;
                    GameObject bgTile = CreateBackgroundTile(new Vector3(x,y,0.1f), parent);
                    if (x % 2 == 0 && y % 2 == 0 || x % 2 != 0 && y % 2 != 0)
                        bgTile.GetComponent<SpriteRenderer>().sprite = _resourcesLoader.DarkTile;
                    else
                        bgTile.GetComponent<SpriteRenderer>().sprite = _resourcesLoader.LightTile;

                    var duration = Random.Range(0.8f, 1.5f);
                    _animation.Reveal(bgTile, duration);
                }
            }
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), _cts.IsCancellationRequested);
            _cts.Cancel();
        }
        
        public void Dispose()
        {
            _cts?.Dispose();
            _objectResolver?.Dispose();
        }

        private GameObject CreateBackgroundTile(Vector3 position, Transform parent) => 
            _objectResolver.Instantiate(_resourcesLoader.BgTilePrefab, position, Quaternion.identity, parent);
    }
}