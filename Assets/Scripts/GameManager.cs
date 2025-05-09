using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Global")]
    public AudioClip victorySound;
    public AudioClip countdownSound;
    public AudioClip errorSound;
    public AudioClip stopSound;
    public VideoPlayer cinemaVideo;
    public TMP_Text statusText;

    [Header("Timer UI")]
    public TMP_Text timerText;
    public TMP_Text secondaryTimerText;

    [Header("Association")]
    public TMP_Text scoreLinkText;
    public bool associationCompleted = false;

    [Header("Enregistrement")]
    public TMP_Text scorePropText;
    public bool registerCompleted = false;

    private AudioSource audioSource;

    // Scores
    private int linkScore = 0;
    private int propScore = 0;
    private const int maxLinkScore = 4;
    private const int maxPropScore = 1;

    // Timer
    private float timer = 0f;
    private bool timerRunning = false;
    private Coroutine timerCoroutine;
    private GameObject[] gameStartObjects;
    private bool gameStartHidden = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (cinemaVideo == null)
        {
            GameObject cinemaObj = GameObject.FindGameObjectWithTag("cinema-video");
            if (cinemaObj != null)
                cinemaVideo = cinemaObj.GetComponent<VideoPlayer>();
        }

        gameStartObjects = GameObject.FindGameObjectsWithTag("GameStart");

        foreach (GameObject obj in gameStartObjects)
            obj.SetActive(true);

        UpdateTimerText();
        UpdateLinkUI();
        UpdatePropUI();
    }

    // ========== SCORE ==========

    public void AddLinkScore()
    {
        linkScore++;
        UpdateLinkUI();

        if (!associationCompleted && linkScore >= maxLinkScore)
        {
            associationCompleted = true;
            Debug.Log("Association terminée !");
            StartCoroutine(PlayVictorySoundDelayed(2f));
        }
    }

    public void AddPropScore()
    {
        propScore++;
        UpdatePropUI();

        if (!registerCompleted && propScore >= maxPropScore)
        {
            registerCompleted = true;
            Debug.Log("Enregistrement complété !");
            StartCoroutine(PlayVictorySoundDelayed(2f));
        }
    }

    private void UpdateLinkUI()
    {
        if (scoreLinkText != null)
            scoreLinkText.text = "Score : " + linkScore + " / " + maxLinkScore;
    }

    private void UpdatePropUI()
    {
        if (scorePropText != null)
            scorePropText.text = "Score : " + propScore + " / " + maxPropScore;
    }

    private IEnumerator PlayVictorySoundDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (victorySound != null && audioSource != null)
        {
            audioSource.loop = false;
            audioSource.PlayOneShot(victorySound);
        }
    }

    // ========== TIMER ==========

    public void StartCountdownFromButton()
    {
        if (!timerRunning)
            StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        if (audioSource != null && countdownSound != null)
            audioSource.PlayOneShot(countdownSound);

        for (int i = 3; i > 0; i--)
        {
            string countText = $"Début dans\n{i}...";
            if (timerText != null) timerText.text = countText;
            if (secondaryTimerText != null) secondaryTimerText.text = countText;
            yield return new WaitForSeconds(1f);
        }

        StartTimer();

        if (!gameStartHidden && gameStartObjects != null)
        {
            foreach (GameObject obj in gameStartObjects)
                obj.SetActive(false);
            gameStartHidden = true;
        }
    }

    public void StartTimer()
    {
        if (!timerRunning)
        {
            timerRunning = true;
            if (cinemaVideo != null)
                cinemaVideo.Play();

            timerCoroutine = StartCoroutine(RunTimer());
        }
    }

    private IEnumerator RunTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timer += 1f;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        string formatted = string.Format("Temps :\n{0:00}:{1:00}",
            Mathf.FloorToInt(timer / 60), Mathf.FloorToInt(timer % 60));

        if (timerText != null) timerText.text = formatted;
        if (secondaryTimerText != null) secondaryTimerText.text = formatted;
    }

public void StopTimer()
{
    if (!associationCompleted || !registerCompleted)
    {
        string message = "";

        if (!associationCompleted)
            message += "Jeu Association non validé.\n";
        if (!registerCompleted)
            message += "Jeu Enregistrement non validé.";

        if (statusText != null)
            statusText.text = message;

        if (audioSource != null && errorSound != null)
            audioSource.PlayOneShot(errorSound);

        return;
    }

    if (timerRunning && timerCoroutine != null)
    {
        StopCoroutine(timerCoroutine);
        timerRunning = false;

        if (audioSource != null && stopSound != null)
            audioSource.PlayOneShot(stopSound);

        if (statusText != null)
            statusText.text = "Jeu terminé avec succès !";
    }
}

}
