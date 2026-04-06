using UnityEngine;
using System.Collections;
using TMPro;

public class UI : MonoBehaviour
{
    // References to the text elements on screen
    public TMP_Text scoreText;
    public TMP_Text strikesText;
    public TMP_Text pitchCounter;
    public TMP_Text hitQualityText;

    private Vector3 scoreOriginalScale;
    private Color strikesOriginalColor;

    void Start()
    {
        scoreOriginalScale = scoreText.transform.localScale;
        strikesOriginalColor = strikesText.color;
        hitQualityText.text = "";

        UpdateHUD();
    }

    // Call this to refresh the HUD instead of updating every frame
    public void UpdateHUD()
    {
        scoreText.text = "Score: " + Score.Instance.score;
        strikesText.text = "Strikes: " + Score.Instance.strikes;
        pitchCounter.text = "Pitches: " + Score.Instance.pitchCount;
    }

    // Call this from Score.cs on a hit
    public void AnimateHit(float quality)
    {
        UpdateHUD();
        StartCoroutine(PopScale(scoreText));
        StartCoroutine(ShowHitQuality(quality));
    }

    // Call this from Score.cs on a miss
    public void AnimateMiss()
    {
        UpdateHUD();
        StartCoroutine(FlashRed(strikesText));
    }

    private IEnumerator ShowHitQuality(float quality)
    {
        // Set text and color based on quality
        if (quality >= 0.85f)
        {
            hitQualityText.text = "PERFECT!";
            hitQualityText.color = Color.yellow;
        }
        else if (quality >= 0.5f)
        {
            hitQualityText.text = "GREAT!";
            hitQualityText.color = Color.green;
        }
        else
        {
            hitQualityText.text = "GOOD";
            hitQualityText.color = Color.white;
        }

        // Pop the text in
        Vector3 originalScale = hitQualityText.transform.localScale;
        Vector3 bigScale = originalScale * 1.5f;
        float elapsed = 0f;
        float popDuration = 0.1f;

        while (elapsed < popDuration)
        {
            hitQualityText.transform.localScale = Vector3.Lerp(originalScale, bigScale, elapsed / popDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        while (elapsed > 0f)
        {
            hitQualityText.transform.localScale = Vector3.Lerp(originalScale, bigScale, elapsed / popDuration);
            elapsed -= Time.deltaTime;
            yield return null;
        }

        hitQualityText.transform.localScale = originalScale;

        // Hold for a moment then fade out
        yield return new WaitForSeconds(0.8f);

        float fadeDuration = 0.4f;
        elapsed = 0f;
        Color originalColor = hitQualityText.color;

        while (elapsed < fadeDuration)
        {
            hitQualityText.color = Color.Lerp(originalColor, Color.clear, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        hitQualityText.text = "";
        hitQualityText.color = originalColor;
    }

    // Scales the text up then back down
    private IEnumerator PopScale(TMP_Text text)
    {
        Vector3 bigScale = scoreOriginalScale * 1.4f;
        float duration = 0.1f;
        float elapsed = 0f;

        // Scale up
        while (elapsed < duration)
        {
            text.transform.localScale = Vector3.Lerp(scoreOriginalScale, bigScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        // Scale back down
        while (elapsed < duration)
        {
            text.transform.localScale = Vector3.Lerp(bigScale, scoreOriginalScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        text.transform.localScale = scoreOriginalScale;
    }

    // Flashes the text red then back to original color
    private IEnumerator FlashRed(TMP_Text text)
    {
        float duration = 0.15f;
        float elapsed = 0f;

        // Flash to red
        while (elapsed < duration)
        {
            text.color = Color.Lerp(strikesOriginalColor, Color.red, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        // Fade back
        while (elapsed < duration)
        {
            text.color = Color.Lerp(Color.red, strikesOriginalColor, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        text.color = strikesOriginalColor;
    }
}