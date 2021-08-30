using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private SceneLoader _sceneLoader;


    public override void InstallBindings()
    {
        var sceneLoader = Container.InstantiatePrefabForComponent<SceneLoader>(_sceneLoader, Vector3.zero, Quaternion.identity, null);
        Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();
    }
}
