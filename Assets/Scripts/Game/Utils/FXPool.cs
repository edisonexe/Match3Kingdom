using System.Collections.Generic;
using ResourcesLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Utils
{
    public class FXPool
    {
        private readonly List<GameObject> _fxPool = new List<GameObject>();
        private GameObject _fxPrefab;
        private IObjectResolver _objectResolver;
        private readonly GameResourcesLoader _resourcesLoader;

        public FXPool(IObjectResolver objectResolver, GameResourcesLoader resourcesLoader)
        {
            _objectResolver = objectResolver;
            _resourcesLoader = resourcesLoader;
        }

        public GameObject GetFXFromPool(Vector3 position, Transform parent)
        {
            for (int i = 0; i < _fxPool.Count; i++)
            {
                if(_fxPool[i].activeInHierarchy) continue;
                _fxPool[i].gameObject.transform.position = position;
                _fxPool[i].SetActive(true);
                return _fxPool[i];
            }

            var fx = CreateFX(position, parent);
            fx.SetActive(true);
            return fx;
        }

        private GameObject CreateFX(Vector3 position, Transform parent)
        {
            var fx = _objectResolver.Instantiate(_resourcesLoader.FXPrefab, position + Vector3.forward, 
                Quaternion.identity, parent);
            _fxPool.Add(fx);
            return fx;
        }
    }
}