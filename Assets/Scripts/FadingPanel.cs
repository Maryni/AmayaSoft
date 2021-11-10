using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FadingPanel : MonoBehaviour
{
    #region private variables

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private float duration;

    [SerializeField]
    private bool needFadeIn;

    [SerializeField]
    private bool needFadeOut;

    [SerializeField]
    private bool needDisable;

    private Tween fadeTween;
    private Coroutine fadeCoroutine;

    #endregion private variables

    #region Unity functions

    private void OnValidate()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        if (fadeCoroutine == null)
        {
            fadeCoroutine = StartCoroutine(Fading());
        }
    }

    #endregion Unity functions

    #region public void

    public void FadeIn(float duration)
    {
        Fade(1f, duration, () =>
              {
                  canvasGroup.interactable = true;
                  canvasGroup.blocksRaycasts = true;
              });
    }

    public void FadeOut(float duration)
    {
        Fade(0f, duration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            if (needDisable)
            {
                gameObject.SetActive(false);
            }
        });
    }

    #endregion public void

    #region private void

    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }
        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }

    private IEnumerator Fading()
    {
        if (needFadeIn)
        {
            FadeIn(duration);
        }
        else if (needFadeOut)
        {
            FadeOut(duration);
        }

        fadeCoroutine = null;
        yield return 0;
    }

    #endregion private void
}