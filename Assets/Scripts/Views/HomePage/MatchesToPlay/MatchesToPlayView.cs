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
    public class MatchesToPlayView : MonoBehaviour, IMatchesToPlayView
    {
        [SerializeField]private GameObject matchContainerPrefab;
        [SerializeField] private NotificationsController notificationsController;
        [SerializeField] private ErrorWindow errorWindow;
        
        private List<GameObject> matchesToPlayContainersGOs= new List<GameObject>();
        public event Action OnUpdateActualMatches;
        public event Action<MatchInfoDTO> OnPlayTurn;
        public bool IsDestroyed { get; private set; } = false;
        
        private void Start()
        {
            notificationsController.OnMatchesToPlayUpdate += InvokeUpdateMatches;
            IDataApplicationContainer dataApplicationContainer = new DataApplicationContainer();
            new MatchesToPlayPresenter(this,notificationsController.gatewayProvider, dataApplicationContainer,new ChangeSceneCommand("STARTTURN"),new ChangeSceneCommand("ENDROUND"));
        }


        private void OnDestroy()
        {
            matchesToPlayContainersGOs.Clear();
        
            IsDestroyed = true;
        }

        public void SetMatchesToPlay(List<MatchInfoDTO> matchesInfos)
        {
            DestroyMatchesContainers(); 
            matchesInfos.Sort((mi1, mi2) => mi2.IsPlayerTurn.CompareTo(mi1.IsPlayerTurn));

            foreach(var matchInfo in matchesInfos)
            {
                GameObject matchToPlayContainerGO = Instantiate(matchContainerPrefab, transform.position, transform.rotation);
                matchToPlayContainerGO.transform.SetParent(transform, false);
                var matchToPlayContainerScript = matchToPlayContainerGO.GetComponent<MatchToPlayContainer>();
                matchToPlayContainerScript.OnPlayTurn += PlayThisMatch;
                matchToPlayContainerScript.Initialize(matchInfo);
                matchesToPlayContainersGOs.Add(matchToPlayContainerGO);
            }
            
        }

        private void DestroyMatchesContainers()
        {
        
            foreach (var matchContainerGO in matchesToPlayContainersGOs)
            {
                if (matchContainerGO != null) 
                {
                    Destroy(matchContainerGO);
                }
            }
            matchesToPlayContainersGOs.Clear();
        }

        public void InvokeUpdateMatches()
        {
            OnUpdateActualMatches?.Invoke();
        }

        private void PlayThisMatch(MatchInfoDTO matchInfo)
        {
            OnPlayTurn?.Invoke(matchInfo);
        }

        public void ShowErrorWindow(string message)
        {
            errorWindow.gameObject.SetActive(true);
            errorWindow.SetMessage(message);            
        }
    }
}