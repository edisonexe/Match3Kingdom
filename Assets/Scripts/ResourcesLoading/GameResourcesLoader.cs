using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Game.Tiles;
using Level;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ResourcesLoading
{
    public class GameResourcesLoader : IDisposable
    {
        public GameObject TilePrefab {get; private set;}
        public TileConfig BlankConfig {get; private set;}
        public GameObject BgTilePrefab {get; private set;}
        public Sprite DarkTile {get; private set;}
        public Sprite LightTile {get; private set;}
        public GameObject FXPrefab {get; private set;}
        public List<TileConfig> CurrentTileSet {get; private set;}
        private GameData _gameData;
        private CancellationTokenSource _cts;
        
        
        private AsyncOperationHandle<TileSetConfig>? _setHandle;
        private AsyncOperationHandle<GameObject>? _tileHandle;
        private AsyncOperationHandle<GameObject>? _bgTileHandle;
        private AsyncOperationHandle<GameObject>? _fxHandle;
        private AsyncOperationHandle<TileConfig>? _blankHandle;
        private AsyncOperationHandle<Sprite>? _darkHandle;
        private AsyncOperationHandle<Sprite>? _lightHandle;
        
        public GameResourcesLoader(GameData gameData) => _gameData = gameData;

        public async UniTask Load()
        {
            CurrentTileSet = new List<TileConfig>();
            if (_gameData.CurrentLevel.TileSets == TileSets.Kingdom) 
                await LoadSet("Kingdom");
            if (_gameData.CurrentLevel.TileSets == TileSets.Gem) 
                await LoadSet("Gem");
            await LoadTilePrefabs();
            await LoadBlankTile();
            await LoadBackgroundSprites();

        }

        private async UniTask LoadSet(string key)
        {
            _cts  = new CancellationTokenSource();
            var set = Addressables.LoadAssetAsync<TileSetConfig>(key);
            await set.ToUniTask();
            if (set.Status == AsyncOperationStatus.Succeeded)
            {
                CurrentTileSet = set.Result.Set;
                // Addressables.Release(set);
            }
            _cts.Cancel();
        }

        private async UniTask LoadTilePrefabs()
        {
            _cts  = new CancellationTokenSource();
            var tile = Addressables.LoadAssetAsync<GameObject>("TilePrefab");
            var bgTile = Addressables.LoadAssetAsync<GameObject>("BackgroundTile");
            var fx = Addressables.LoadAssetAsync<GameObject>("FXPrefab");
            await tile.ToUniTask();
            await bgTile.ToUniTask();
            await fx.ToUniTask();
            if (tile.Status == AsyncOperationStatus.Succeeded && bgTile.Status == AsyncOperationStatus.Succeeded && 
                fx.Status == AsyncOperationStatus.Succeeded)
            {
                TilePrefab = tile.Result;
                BgTilePrefab = bgTile.Result;
                FXPrefab = fx.Result;
                // Addressables.Release(tile);
                // Addressables.Release(bgTile);
                // Addressables.Release(fx);
            }
            _cts.Cancel();
        }
        
        private async UniTask LoadBlankTile()
        {
            _cts  = new CancellationTokenSource();
            var blank = Addressables.LoadAssetAsync<TileConfig>("BlankTile");
            await blank.ToUniTask();
            if (blank.Status == AsyncOperationStatus.Succeeded)
            {
                BlankConfig = blank.Result;
                // Addressables.Release(BlankConfig);
            }
            _cts.Cancel();
        }
        
        private async UniTask LoadBackgroundSprites()
        {
            _cts  = new CancellationTokenSource();
            var darkSprite = Addressables.LoadAssetAsync<Sprite>("DarkBG");
            var lightSprite = Addressables.LoadAssetAsync<Sprite>("LightBG");
            await darkSprite.ToUniTask();
            await lightSprite.ToUniTask();
            if (darkSprite.Status == AsyncOperationStatus.Succeeded && 
                lightSprite.Status == AsyncOperationStatus.Succeeded)
            {
                DarkTile = darkSprite.Result;
                LightTile = lightSprite.Result;
                // Addressables.Release(DarkTile);
                // Addressables.Release(LightTile);
            }
            _cts.Cancel();
        }
        
        public void ReleaseAll()
        {
            if (_setHandle is { } sh && sh.IsValid()) Addressables.Release(sh);
            if (_tileHandle is { } th && th.IsValid()) Addressables.Release(th);
            if (_bgTileHandle is { } bgh && bgh.IsValid()) Addressables.Release(bgh);
            if (_fxHandle is { } fxh && fxh.IsValid()) Addressables.Release(fxh);
            if (_blankHandle is { } blh && blh.IsValid()) Addressables.Release(blh);
            if (_darkHandle is { } dh && dh.IsValid()) Addressables.Release(dh);
            if (_lightHandle is { } lh && lh.IsValid()) Addressables.Release(lh);
        }

        public void Dispose() => ReleaseAll();
    }
}