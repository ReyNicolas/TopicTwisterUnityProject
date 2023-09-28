using System;
using System.Collections.Generic;
using DataApplicationsContainer;
using DTOs;
using Presenters;
using Presenters.IViews;
using TMPro;
using UnityEngine;

namespace Views.HomePage
{
    public class RematchNotificationsView : MonoBehaviour, IRematchNotificationsView
    {
        [SerializeField] private GameObject rematchContainerPrefab;
        [SerializeField] private NotificationsController notificationsController;
        [SerializeField] private ErrorWindow errorWindow;
        [SerializeField] private GameObject countObject;
        [SerializeField] private TextMeshProUGUI countText;
        public bool IsDestroyed { get; private set; } = false;
        private List<GameObject> rematchesContainersGOs = new List<GameObject>();
        public event Action<PlayerRematchNotificationDTO> OnAcceptRematch;
        public event Action<PlayerRematchNotificationDTO> OnDeclineRematch;
        public event Action OnUpdateRematch;
        private Coroutine coroutine;

        private void Start()
        {
            notificationsController.OnRematchNotificationsUpdate += InvokeUpdateRematchNotifications;
            IDataApplicationContainer dataApplicationContainer = new DataApplicationContainer();
            new RematchPresenter(this, notificationsController.gatewayProvider, dataApplicationContainer);
        }

        private void InvokeUpdateRematchNotifications()
        {
            OnUpdateRematch?.Invoke();
        }

        private void OnDestroy()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                rematchesContainersGOs.Clear();
            }

            IsDestroyed = true;
        }

        public void SetRematchNotifications(List<PlayerRematchNotificationDTO> rematches)
        {
            DestroyMatchesContainers();
            foreach (var rematch in rematches)
            {
                GameObject rematchContainerGO = Instantiate(rematchContainerPrefab, transform.position, transform.rotation);
                rematchContainerGO.transform.SetParent(transform, false);
                var rematchNotificationScript = rematchContainerGO.GetComponent<RematchNotificationContainer>();
                rematchNotificationScript.OnAcceptRematch += AcceptRematch;
                rematchNotificationScript.OnDeclineRematch += DeclineRematch;
                rematchNotificationScript.Initialize(rematch);
                rematchesContainersGOs.Add(rematchContainerGO);
            }
            SetCount(rematches.Count);
        }
        private void SetCount(int count)
        {
            if (count < 1)
            {
                countText.text = string.Empty;
                countObject.SetActive(false);
                return;
            }
            countObject.SetActive(true);
            countText.text = count.ToString();

        }


        private void DestroyMatchesContainers()
        {
            foreach (var rematchContainerGO in rematchesContainersGOs)
            {
                if (rematchContainerGO != null)
                {
                    Destroy(rematchContainerGO);
                }
            }
            rematchesContainersGOs.Clear();
        }

        private void AcceptRematch(PlayerRematchNotificationDTO rematchNotification)
        {
            OnAcceptRematch?.Invoke(rematchNotification);
        }

        private void DeclineRematch(PlayerRematchNotificationDTO rematchNotification)
        {
            OnDeclineRematch?.Invoke(rematchNotification);
        }

        public void ShowErrorWindow(string message)
        {            
            errorWindow.gameObject.SetActive(true);
            errorWindow.SetMessage(message);
        }
    }
}