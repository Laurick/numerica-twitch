using System;
using System.Collections.Generic;
using TMPro;
using TwitchChat;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CounterTwitchGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usernameTMP;
    [SerializeField] private TextMeshProUGUI currentScoreTMP;
    [SerializeField] private TextMeshProUGUI maxScoreTMP;
    [SerializeField] private TextMeshProUGUI timeTMP;
    [SerializeField] private Image timeImage;
    [SerializeField] private AnimationCurve timeCurve;
    
    private int currentScore;

    private int maxUsersPermited;
    private Queue<Chatter> lastChatters = new();
    private Chatter lastChatter;

    private int currentMaxScore;
    private readonly string maxScoreKey = "maxScore";
    
    private float minTime;
    private readonly string minTimeKey = "minTime";
    
    private float maxTimeRound;
    private float timeRound;
    private const float FirstRoundTime = 30.5f;

    private float sessionTime = 0;
    
    private const int maxGameScore = 100;
    private const float noneTime = -1f;
    
    [SerializeField] private GameObject startingCanvas;

    private void Start()
    {
        Application.targetFrameRate = 30;

        TwitchController.onTwitchMessageReceived += OnTwitchMessageReceived;
        TwitchController.onChannelJoined += OnChannelJoined;

        currentMaxScore = PlayerPrefs.GetInt(maxScoreKey);
        minTime = PlayerPrefs.GetFloat(minTimeKey, noneTime);

        UpdateMaxScoreUI();
        UpdateCurrentScoreUI("Ildesir", currentScore.ToString());
        ResetGame();
    }

    public void Update()
    {
        if (currentScore == 0)
        {
            setTextTime(0);
            return;
        }
        
        sessionTime += Time.deltaTime;
        setTextTime(sessionTime);
        
        timeRound -= Time.deltaTime;
        timeImage.fillAmount = (float)(timeRound / maxTimeRound);

        if (timeRound < 3)
        {
            float newAlpha = timeRound % 1;
            timeImage.color = new Color(timeImage.color.r,timeImage.color.g,timeImage.color.b,newAlpha);
        }

        if (timeRound <= 0)
        {
            string displayName = getDisplayName(lastChatter);
            HandleIncorrectResponse(displayName, lastChatter);
        }
    }

    private void setTextTime(double timeInSeconds)
    {
        string formatedTime = formateTime(timeInSeconds);
        timeTMP.SetText(formatedTime);
    }

    public string formateTime(double timeInSeconds)
    {
        if (Math.Abs(noneTime - timeInSeconds) < 0.01)
            return "Infinito";
        int minutes = (int)(timeInSeconds / 60);
        int seconds = (int)(timeInSeconds % 60);
        int milliseconds = (int)(timeInSeconds*1000-seconds*1000);
        return $"{minutes:D2}:{seconds:D2}.{milliseconds}";
    }
    
    private string getDisplayName(Chatter chatter)
    {
        return chatter.IsDisplayNameFontSafe() ? chatter.tags.displayName : chatter.login;
    }

    private void OnDestroy()
    {
        TwitchController.onTwitchMessageReceived -= OnTwitchMessageReceived;
        TwitchController.onChannelJoined -= OnChannelJoined;
    }

    private void OnTwitchMessageReceived(Chatter chatter)
    {
        if (!int.TryParse(chatter.message, out int response)) return;

        string displayName = getDisplayName(chatter);

        if (lastChatters.Contains(chatter)) return;

        if (response == currentScore + 1) HandleCorrectResponse(displayName, chatter);
        else HandleIncorrectResponse(displayName, chatter);
    }

    private void HandleCorrectResponse(string displayName, Chatter chatter)
    {
        float delta = FirstRoundTime*timeCurve.Evaluate(currentScore/100f);
        maxTimeRound = (FirstRoundTime - delta)+0.5f;
        timeRound = maxTimeRound;
        currentScore++;
        UpdateCurrentScoreUI(displayName, currentScore.ToString());

        if (lastChatters.Count == maxUsersPermited)
            lastChatters.Dequeue();
        lastChatters.Enqueue(chatter);
        lastChatter = chatter;
        timeImage.color = new Color(timeImage.color.r,timeImage.color.g,timeImage.color.b,1);
        
        if (currentScore > currentMaxScore)
        {
            SetMaxScore(currentScore, sessionTime);
        }

        if (currentScore == maxGameScore)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        if (sessionTime < minTime)
            PlayerPrefs.SetFloat(minTimeKey, sessionTime);
        usernameTMP.SetText($"Bien hecho :D<br>¿Podréis hacerlo mejor?");
        ResetGame();
    }

    private void HandleIncorrectResponse(string displayName, Chatter chatter)
    {
        if (currentScore != 0)
        {
            DisplayShameMessage(displayName);

            HandleTimeout(chatter);
            UpdateMaxScoreUI();
            ResetGame();
        }
    }
    
    private void HandleTimeout(Chatter chatter)
    {
        if (TwitchOAuth.Instance.IsModImmunityEnabled())
        {
            if (!chatter.HasBadge("moderator"))
            {
                TwitchOAuth.Instance.Timeout(chatter.tags.userId, currentScore);
            }
        }
        else
        {
            TwitchOAuth.Instance.Timeout(chatter.tags.userId, currentScore);
        }
    }

    private void DisplayShameMessage(string displayName)
    {
        usernameTMP.SetText($"{displayName}<color=#65451F> tómate otro café!</color>");
    }

    private void OnChannelJoined()
    {
        startingCanvas.SetActive(false);
    }

    public void ResetHighScore()
    {
        SetMaxScore(0, noneTime);
        ResetGame();
    }

    private void SetMaxScore(int score,float time)
    {
        currentMaxScore = score;
        PlayerPrefs.SetInt(maxScoreKey, score);
        minTime = time;
        PlayerPrefs.SetFloat(minTimeKey, time);
        UpdateMaxScoreUI();
    }

    private void UpdateMaxScoreUI()
    {
        string scoreText = $"PUNTUACIÓN: {currentMaxScore}<br>TIEMPO: {formateTime(minTime)}";
        maxScoreTMP.SetText(scoreText);
    }

    private void UpdateCurrentScoreUI(string username, string score)
    {
        usernameTMP.SetText(username);
        currentScoreTMP.SetText(score);
    }

    private void ResetGame()
    {
        lastChatters.Clear();
        lastChatter = null;
        maxUsersPermited = Random.Range(3, 10);
        currentScore = 0;
        currentScoreTMP.SetText(currentScore.ToString());
        sessionTime = 0;
    }
}