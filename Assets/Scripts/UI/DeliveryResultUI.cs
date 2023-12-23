using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string DeliveryFailedMessage = "Delivery\nFailed";
    private const string DeliverySuccessMessage = "Delivery\nSuccess";
    private const string TriggerPopup = "Popup";
    
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color colorSuccess;
    [SerializeField] private Color colorFailed;
    [SerializeField] private Sprite spriteSuccess;
    [SerializeField] private Sprite spriteFailed;

    private Animator _animator;
    private static readonly int Popup = Animator.StringToHash(TriggerPopup);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        backgroundImage.color = colorFailed;
        iconImage.sprite = spriteFailed;
        messageText.text = DeliveryFailedMessage;
        _animator.SetTrigger(Popup);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        backgroundImage.color = colorSuccess;
        iconImage.sprite = spriteSuccess;
        messageText.text = DeliverySuccessMessage;
        _animator.SetTrigger(Popup);
    }
}
