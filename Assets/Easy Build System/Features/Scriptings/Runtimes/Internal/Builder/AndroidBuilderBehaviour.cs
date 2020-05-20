using EasyBuildSystem.Runtimes.Extensions;
using EasyBuildSystem.Runtimes.Internal.Builder;
using EasyBuildSystem.Runtimes.Internal.Managers;
using UnityEngine;

[AddComponentMenu("Easy Build System/Features/Builders Behaviour/Android Builder Behaviour")]
public class AndroidBuilderBehaviour : BuilderBehaviour
{
    public override void Start()
    {
        base.Start();

        if (BuildManager.Instance == null)
            return;

        if (BuildManager.Instance.PartsCollection != null)
            SelectPrefab(BuildManager.Instance.PartsCollection.Parts[0]);
    }
}