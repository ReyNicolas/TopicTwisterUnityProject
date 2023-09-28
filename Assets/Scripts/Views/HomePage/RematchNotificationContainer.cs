using DTOs;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RematchNotificationContainer : MonoBehaviour
{
    public event Action<PlayerRematchNotificationDTO> OnAcceptRematch;
    public event Action<PlayerRematchNotificationDTO> OnDeclineRematch;
    [SerializeField] private Button AcceptButton;
    [SerializeField] private Button DeclineButton;
    [SerializeField] private TextMeshProUGUI rivalText;  
    PlayerRematchNotificationDTO playerRematchNotification;


    public void Initialize(PlayerRematchNotificationDTO playerRematchNotification)
    {
        this.playerRematchNotification = playerRematchNotification;
        rivalText.text = playerRematchNotification.rivalID;
    }

    public void PressedAcceptButton()
    {
        OnAcceptRematch?.Invoke(playerRematchNotification);
        LockButtons();
    }

    public void PressedDeclineButton()
    {
        OnDeclineRematch?.Invoke(playerRematchNotification);
        LockButtons();
    }

    private void LockButtons()
    {
        AcceptButton.interactable = false;
        DeclineButton.interactable = false;
    }
}
