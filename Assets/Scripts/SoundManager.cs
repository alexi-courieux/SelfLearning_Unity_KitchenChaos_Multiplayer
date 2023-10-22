using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefs;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    { 
        var counter = (TrashCounter) sender;
        PlaySound(audioClipRefs.trash, counter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        var counter = (BaseCounter) sender;
        PlaySound(audioClipRefs.objectDrop, counter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        var position = Player.Instance.transform.position;
        PlaySound(audioClipRefs.objectPickup, position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        var cuttingCounter = (CuttingCounter) sender;
        PlaySound(audioClipRefs.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        var position = DeliveryCounter.Instance.transform.position;
        PlaySound(audioClipRefs.deliveryFail, position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        var position = DeliveryCounter.Instance.transform.position;
        PlaySound(audioClipRefs.deliverySuccess, position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlaySound(IReadOnlyList<AudioClip> audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Count)], position, volume);
    }
    
    public void PlayFootstepsSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipRefs.footsteps, position, volume);
    }
}
