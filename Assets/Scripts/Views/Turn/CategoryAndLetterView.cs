using System.Collections;
using System.Collections.Generic;
using DataApplicationsContainer;
using Presenters;
using Providers;
using TMPro;
using UnityEngine;


namespace Views
{
    public class CategoryAndLetterView : MonoBehaviour, IStartOfTurnView
    {
        [SerializeField] private List<TextMeshProUGUI> categoriesTexts;
        [SerializeField] private TextMeshProUGUI letterText;
        [SerializeField] private float timeToChangeView=3f;
        [SerializeField] private List<GameObject> prefabsOfViews;
        [SerializeField] private ErrorWindow errorWindow;
        private StartOfTurnPresenter presenter;
        private int currentPrefabIndex;
    
        void Start()
        {
            presenter = new StartOfTurnPresenter();
            presenter.Initialize(this, new GatewayProvider(),new DataApplicationContainer(), timeToChangeView);
            ActivateCurrentPrefab();
        }

        
        public void ShowCategoriesAndLetter(List<string> categoriesNames, char letter)
        {
            for (int i = 0; i < categoriesNames.Count; i++)
            {
                categoriesTexts[i].text = categoriesNames[i];
            }

            letterText.text = letter.ToString();
        }
        

        public void WaitThisTimeToChangeToTurn(float time)
        {
            StartCoroutine(ChangeToTurn(time));
        }

        IEnumerator ChangeToTurn(float time)
        {
            yield return new WaitForSeconds(time);
            TogglePrefabs();
            

        }
        private void ActivateCurrentPrefab()
        {
            for (int i = 0; i < prefabsOfViews.Count; i++)
            {
                prefabsOfViews[i].SetActive(i == currentPrefabIndex);
            }
        }
        private void TogglePrefabs()
        {
            prefabsOfViews[currentPrefabIndex].SetActive(false);
            currentPrefabIndex = (currentPrefabIndex + 1) % prefabsOfViews.Count;
            prefabsOfViews[currentPrefabIndex].SetActive(true);
        }

        public void ShowErrorWindow(string message)
        {
            errorWindow.gameObject.SetActive(true);
            errorWindow.SetMessage(message);
        }
    }
}
