using EasyBuildSystem.Runtimes.Internal.Part;
using UnityEngine;

public class ExampleAppearanceChanger : MonoBehaviour
{
    private PartBehaviour part;

    private void Awake()
    {
        part = GetComponent<PartBehaviour>();

        InvokeRepeating("UpdateAppearance", 1f, 1f);
    }

    private void UpdateAppearance()
    {
        if (part.AppearanceIndex >= part.Appearances.Count-1)
            part.AppearanceIndex = 0;
        else
            part.AppearanceIndex++;
    }
}