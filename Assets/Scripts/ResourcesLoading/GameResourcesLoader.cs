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
            _cts  = new CancellationTokenSource();
            CurrentTileSet = new List<TileConfig>();
            await LoadSet();
            await LoadTilePrefabs();
            BlankConfig = await Loader<TileConfig>("BlankTile");
            await LoadBackgroundSprites();
            _cts.Cancel();
        }

        private async UniTask<T> Loader<T>(string key)
        {
            var assetHandler = Addressables.LoadAssetAsync<T>(key);
            var asset = await assetHandler.ToUniTask();
            return assetHandler.Status == AsyncOperationStatus.Succeeded ? asset : default;
        }
        
        private async UniTask LoadSet()
        {
            _cts  = new CancellationTokenSource();
            switch (_gameData.CurrentLevel.TileSets)
            {
                case(TileSets.Kingdom):
                    var tileSets = await Loader<TileSetConfig>("Kingdom");
                    CurrentTileSet = tileSets.Set;
                    break;
                case(TileSets.Gem):
                    tileSets = await Loader<TileSetConfig>("Gem");
                    CurrentTileSet = tileSets.Set;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _cts.Cancel();
        }

        private async UniTask LoadTilePrefabs()
        {
            TilePrefab = await Loader<GameObject>("TilePrefab");
            BgTilePrefab = await Loader<GameObject>("BackgroundTile");
            FXPrefab = await Loader<GameObject>("FXPrefab");
        }
        
        private async UniTask LoadBackgroundSprites()
        {
            _cts  = new CancellationTokenSource();
            DarkTile = await Loader<Sprite>("DarkBG");
            LightTile = await Loader<Sprite>("LightBG");
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

        public void Dispose()
        {
            _cts?.Dispose();
            ReleaseAll();
        }
    }
}