using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialShow : MonoBehaviour
{
    [SerializeField] List<VideoClip> videoClips;
    [SerializeField] GameObject TabSuggest;

    [SerializeField] UnityEngine.Video.VideoPlayer videoPlayer;

    int index = 0;

    bool isShowing = false;

    private void Start()
    {
        videoPlayer.clip = videoClips[index];
        TabSuggest.SetActive(!isShowing);
        videoPlayer.SetDirectAudioMute(0, true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isShowing = ! isShowing;
            TabSuggest.SetActive(!isShowing);
            if (isShowing)
            {
                StartFade(1f);
                videoPlayer.SetDirectAudioMute(0, false);
            }
            else
            {
                StartFade(0f);
                videoPlayer.SetDirectAudioMute(0, true);
            }
                
        }
    }
    void PlayCurrent()
    {
        if (videoClips == null || videoClips.Count == 0) return;

        videoPlayer.Stop();
        videoPlayer.clip = videoClips[index];
        videoPlayer.Play();
    }

    public void NextVideo()
    {
        index = (index + 1) % videoClips.Count;
        PlayCurrent();
    }

    public void PrevVideo()
    {
        index--;
        if (index < 0)
            index = videoClips.Count - 1;

        PlayCurrent();
    }

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

    void StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        if (videoPlayer != null)
            videoPlayer.time = 0f;

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
