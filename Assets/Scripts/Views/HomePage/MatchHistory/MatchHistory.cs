using System;
using System.Collections.Generic;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Presenters;
using Presenters.IViews;
using UnityEngine;

namespace Views.HomePage
{
    public class MatchHistory : MonoBehaviour, IMatchHistoryView
    {
        [SerializeField]private GameObject matchContainerPrefab;
        [SerializeField] private NotificationsController notificationsController;
        public bool IsDestroyed { get; private set; } = false;
        private List<GameObject> matchesContainersGOs= new List<GameObject>();
        public event Action<MatchResultDTO> OnViewMatchResult;
        public event Action<MatchResultDTO> OnPlayerRematch;
        public event Action OnUpdateMatchHistory;
        private Coroutine coroutine;

        private void Start()
        {
            notificationsController.OnMatchesHistorialUpdate += InvokeUpdateMatches;
            IDataApplicationContainer dataApplicationContainer = new DataApplicationContainer();
            new MatchHistoryPresenter(this, notificationsController.gatewayProvider, dataApplicationContainer
                ,new ChangeSceneCommand("ENDMATCH"));
        }


        private void OnDestroy()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                matchesContainersGOs.Clear();
            }

            IsDestroyed = true;
        }

        public void SetMatchesResults(List<MatchResultDTO> matchesResults)
        {
            DestroyMatchesContainers();
            foreach(var matchResult in matchesResults)
            {
                GameObject matchContainerGO = Instantiate(matchContainerPrefab, transform.position, transform.rotation);
                matchContainerGO.transform.SetParent(transform, false);
                var matchResultScript = matchContainerGO.GetComponent<MatchResultContainer>();
                matchResultScript.OnViewResult += ViewThisMatchResult; 
                matchResultScript.OnRematch += SendRematch; 
                matchResultScript.Initialize(matchResult);
                matchesContainersGOs.Add(matchContainerGO);
            }
            
        }

        public void InvokeUpdateMatches()
        {
            OnUpdateMatchHistory?.Invoke();
        }

        private void DestroyMatchesContainers()
        {
            foreach (var matchContainerGO in matchesContainersGOs)
            {
                if (matchContainerGO != null) 
                {
                    Destroy(matchContainerGO);
                }
            }
            matchesContainersGOs.Clear();
        }
        
        private void ViewThisMatchResult(MatchResultDTO matchResult)
        {
            OnViewMatchResult?.Invoke(matchResult);
        }

        private void SendRematch(MatchResultDTO matchResult)
        {
            OnPlayerRematch?.Invoke(matchResult);
        }
    }
}