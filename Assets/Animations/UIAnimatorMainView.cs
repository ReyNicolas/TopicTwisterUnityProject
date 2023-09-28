using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimatorMainView : MonoBehaviour
{
    [SerializeField] NotificationsController notificationsController;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject star;
    [SerializeField] GameObject rematch;
    [SerializeField] GameObject RematchNotificationView;
    private bool moveStart;

    private void Start()
    {
        notificationsController.OnPlayerStatsUpdate += MoveStart;
        notificationsController.OnRematchNotificationsUpdate += MoveRematch;
        //StartCoroutine(AnimateMainView());
    }

    public void OpenRematchNotificationView()
    {
        LeanTween.scale(RematchNotificationView, Vector2.one, 0.2f).setDelay(0.2f);
    }

    public void CloseRematchNotificationView()
    {
        LeanTween.scale(RematchNotificationView, Vector2.zero, 0.2f);
    }
    IEnumerator AnimateMainView()
    {
        while (true)
        {
            moveStart = !moveStart;
            if (moveStart)
            {
                MoveStart();
            }

            ScaleStartButton();
            yield return new WaitForSeconds(1f);
        }
    }

    private void ScaleStartButton()
    {
        LeanTween.scale(startButton, Vector2.one * 1.1f, 0.5f);
        LeanTween.scale(startButton, Vector2.one, 0.5f).setDelay(0.5f);
    }

    private void MoveStart()
    {
        LeanTween.scale(star, Vector2.one * 1.3f, 0.5f);
        LeanTween.scale(star, Vector2.one, 0.5f).setDelay(0.5f);
        LeanTween.rotateZ(star, -30, 0.3f);
        LeanTween.rotateZ(star, 20, 0.2f).setDelay(0.3f);
        LeanTween.rotateZ(star, 15, 0.3f).setDelay(0.5f);
        LeanTween.rotateZ(star, -5, 0.2f).setDelay(0.8f);
    }

    private void MoveRematch()
    {
        LeanTween.scale(rematch, new Vector2(1.2f, 0.7f), 0.5f);
        LeanTween.scale(rematch, new Vector2(1f, 1f), 0.5f).setDelay(0.5f);
    }
}

