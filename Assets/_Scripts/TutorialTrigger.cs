using System.Collections;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] RectTransform canvasTransform;
    [SerializeField] float fadeDuration = 0.5f;
    [SerializeField] Vector3 hiddenScale = Vector3.zero;
    [SerializeField] Vector3 shownScale = Vector3.one;

    Coroutine fadeCoroutine;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        canvasTransform.localScale = hiddenScale;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tags.Player)) return;
        StartFade(1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tags.Player)) return;
        StartFade(0f);
    }

    void StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeAndScale(targetAlpha));
    }

    IEnumerator FadeAndScale(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        Vector3 startScale = canvasTransform.localScale;

        Vector3 targetScale = targetAlpha > 0 ? shownScale : hiddenScale;

        float time = 0f;

        if (targetAlpha > 0)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            canvasTransform.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasTransform.localScale = targetScale;

        if (targetAlpha == 0)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
