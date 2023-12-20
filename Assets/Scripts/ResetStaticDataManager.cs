using UnityEngine;

/// <summary>
/// This class is used to reset static data that might get messed up when reloading scenes.
/// </summary>
public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}
