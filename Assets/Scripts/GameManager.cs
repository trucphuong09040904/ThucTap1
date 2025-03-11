using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;
    public Image ImageHP;
    public GameObject scoreUITextGO;

    public RectTransform countdownPanel; // Thanh ngang mở rộng
    public Image countdownImage;
    public Sprite[] countdownSprites; // 3 ảnh đếm ngược

    void Start()
    {
        StartCoroutine(ExpandPanelAndCountdown());
    }

    IEnumerator ExpandPanelAndCountdown()
    {
        Time.timeScale = 0f; // Dừng game khi đếm ngược

        // Hiệu ứng mở rộng thanh ngang
        float duration = 0.5f;
        float targetHeight = 300f; // Độ cao của thanh ngang sau khi mở rộng
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float newHeight = Mathf.Lerp(0, targetHeight, elapsedTime / duration);
            countdownPanel.sizeDelta = new Vector2(countdownPanel.sizeDelta.x, newHeight);
            yield return null;
        }

        // Đếm ngược 3, 2, 1
        if (countdownImage != null && countdownSprites.Length > 0)
        {
            for (int i = 0; i < countdownSprites.Length; i++)
            {
                countdownImage.sprite = countdownSprites[i];
                yield return new WaitForSecondsRealtime(1f);
            }
            countdownImage.gameObject.SetActive(false);
            countdownPanel.gameObject.SetActive(false); // Ẩn panel sau khi đếm xong
        }

        Time.timeScale = 1f; // Bắt đầu game sau khi đếm ngược xong
    }

    public void UpdateHP(float currentHP, float maxHP)
    {
        if (ImageHP != null)
        {
            ImageHP.fillAmount = currentHP / maxHP;
        }
        else
        {
            Debug.LogError("⚠ Lỗi: ImageHP chưa được gán trong GameManager!");
        }
    }

    public void Over()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        scoreUITextGO.GetComponent<GameScore>().Score = 0;
    }

    public void quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
