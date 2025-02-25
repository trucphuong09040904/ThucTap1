using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor; // Import thư viện để thoát Play Mode trong Editor
#endif

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;
    public Image ImageHP;
    public GameObject scoreUITextGO;

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
        EditorApplication.ExitPlaymode(); // Thoát Play Mode trong Unity Editor
#else
        Application.Quit(); // Thoát game khi build
#endif
    }
}
