using System;
using System.Collections.Generic;
using TMPro;
using TwitchChat;
using UnityEngine;
using Random = UnityEngine.Random;

public class CounterTwitchGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ruleTMP;
    [SerializeField] private TextMeshProUGUI usernameTMP;
    [SerializeField] private TextMeshProUGUI numberTMP;
    [SerializeField] private TextMeshProUGUI maxScoreTMP;
    [SerializeField] private TextMeshProUGUI currentScoreTMP;

    // keeps the current session score
    private int currentScore;
    
    // keeps the current number in count
    private int currentNumber;

    private string lastUsername = string.Empty;

    private int currentMaxScore;
    private readonly string maxScoreKey = "maxScore";

    private string currentMaxScoreUsername = "RothioTome";
    private readonly string maxScoreUsernameKey = "maxScoreUsername";

    private string lastUserIdVIPGranted;
    private readonly string lastUserIdVIPGrantedKey = "lastVIPGranted";

    private string nextPotentialVIP;

    [SerializeField] private GameObject startingCanvas;

    private List<Rule> rules;
    private Rule activeRule;

    private float lastMessageTimestamp;
    
    private void Start()
    {
        Application.targetFrameRate = 30;

        TwitchController.onTwitchMessageReceived += OnTwitchMessageReceived;
        TwitchController.onChannelJoined += OnChannelJoined;

        currentMaxScore = PlayerPrefs.GetInt(maxScoreKey);
        currentMaxScoreUsername = PlayerPrefs.GetString(maxScoreUsernameKey, currentMaxScoreUsername);
        lastUserIdVIPGranted = PlayerPrefs.GetString(lastUserIdVIPGrantedKey, string.Empty);

        ResetGame();
        UpdateMaxScoreUI();
        UpdateCurrentScoreUI(lastUsername, currentScore.ToString());
        PrintActiveRule();
        rules = new List<Rule>
        {
            new NextPositiveInt(),
            new PreviousPositiveInt(),
            new NextIntInNegative(),
            new ButtonicaRule(),
            new FiveMutipleRule(),
            new SuscriptorRule(),
            new StreamerRule(),
            new SameInt(),
            new NextRandomInt(),
            new SlowRule(),
            new QuickRule()
        };
    }

    private void PrintActiveRule()
    {
        ruleTMP.SetText(activeRule.description);
    }

    private void OnDestroy()
    {
        TwitchController.onTwitchMessageReceived -= OnTwitchMessageReceived;
        TwitchController.onChannelJoined -= OnChannelJoined;
    }

    private void OnTwitchMessageReceived(Chatter chatter)
    {
        int response = 0;
        if (activeRule.type == Rule.NUMBER)
        {
            if (!int.TryParse(chatter.message, out response)) return;
        } 
        else // text type
        {
            response = currentNumber + 1;
        }
        
        string displayName = chatter.IsDisplayNameFontSafe() ? chatter.tags.displayName : chatter.login;

        //if (lastUsername.Equals(displayName)) return;
        
        AnswerInfo answerInfo = new AnswerInfo(response, currentNumber, chatter, Time.time-lastMessageTimestamp);
        lastMessageTimestamp = Time.time;
        if (activeRule.isCorrectAnswer(answerInfo))
        {
            HandleCorrectResponse(displayName, chatter, activeRule.getNextNumber());
            int number = activeRule.ExecutePreConditions(answerInfo);
            UpdateNumber(number == Int32.MinValue ? currentNumber : number);
        }
        else HandleIncorrectResponse(displayName, chatter);
    }

    private void HandleCorrectResponse(string displayName, Chatter chatter, int response)
    {
        currentNumber = response;
        currentScore++;
        UpdateCurrentScoreUI(displayName, currentScore.ToString());

        lastUsername = displayName;
        if (currentScore > currentMaxScore)
        {
            SetMaxScore(displayName, currentScore);
            HandleVIPStatusUpdate(chatter);
        }

        activeRule = getNextRule();
        PrintActiveRule();
    }
    
    private Rule getNextRule()
    {
        int percentage = Random.Range(0, 100);
        var index = percentage switch
        {
            >= 45 and < 55 => 10,
            >= 55 and < 60 => 1,
            >= 60 and < 65 => 2,
            >= 65 and < 70 => 3,
            >= 70 and < 75 => 4,
            >= 75 and < 80 => 5,
            >= 80 and < 85 => 6,
            >= 85 and < 90 => 7,
            >= 90 and < 95 => 8,
            >= 95 and < 99 => 9,
            _ => 0
        };
        return rules[index];
    }

    private void HandleIncorrectResponse(string displayName, Chatter chatter)
    {
        if (currentNumber != 0)
        {
            DisplayShameMessage(displayName);

            if (TwitchOAuth.Instance.IsVipEnabled())
            {
                if (lastUserIdVIPGranted.Equals(chatter.tags.userId))
                {
                    RemoveLastVIP();
                }

                HandleNextPotentialVIP();
            }

            HandleTimeout(chatter);
            ResetGame();
            UpdateMaxScoreUI();
            UpdateNumber(0);
            PrintActiveRule();
        }
    }

    private void HandleNextPotentialVIP()
    {
        if (!string.IsNullOrEmpty(nextPotentialVIP))
        {
            if (nextPotentialVIP == "-1")
            {
                RemoveLastVIP();
            }
            else
            {
                if (!string.IsNullOrEmpty(lastUserIdVIPGranted))
                {
                    RemoveLastVIP();
                }

                GrantVIPToNextPotentialVIP();
            }

            nextPotentialVIP = string.Empty;
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

    private void HandleVIPStatusUpdate(Chatter chatter)
    {
        if (TwitchOAuth.Instance.IsVipEnabled())
        {
            if (!chatter.tags.HasBadge("vip"))
            {
                nextPotentialVIP = chatter.tags.userId;
            }
            else if (chatter.tags.userId == lastUserIdVIPGranted)
            {
                nextPotentialVIP = "";
            }
            else
            {
                nextPotentialVIP = "-1";
            }
        }
    }

    private void RemoveLastVIP()
    {
        TwitchOAuth.Instance.SetVIP(lastUserIdVIPGranted, false);
        lastUserIdVIPGranted = "";
        PlayerPrefs.SetString(lastUserIdVIPGrantedKey, lastUserIdVIPGranted);
    }

    private void GrantVIPToNextPotentialVIP()
    {
        TwitchOAuth.Instance.SetVIP(nextPotentialVIP, true);
        lastUserIdVIPGranted = nextPotentialVIP;
        PlayerPrefs.SetString(lastUserIdVIPGrantedKey, lastUserIdVIPGranted);
    }

    private void DisplayShameMessage(string displayName)
    {
        usernameTMP.SetText($"<color=#00EAC0>Shame on </color>{displayName}<color=#00EAC0>!</color>");
    }

    private void OnChannelJoined()
    {
        startingCanvas.SetActive(false);
    }

    public void ResetHighScore()
    {
        SetMaxScore("RothioTome", 0);
        RemoveLastVIP();
        ResetGame();
    }

    private void SetMaxScore(string username, int score)
    {
        currentMaxScore = score;
        currentMaxScoreUsername = username;
        PlayerPrefs.SetString(maxScoreUsernameKey, username);
        PlayerPrefs.SetInt(maxScoreKey, score);
        UpdateMaxScoreUI();
    }

    private void UpdateMaxScoreUI()
    {
        string scoreText = $"HIGH SCORE: {currentMaxScore}\nby <color=#00EAC0>";

        if (TwitchOAuth.Instance.IsVipEnabled() &&
            (!string.IsNullOrEmpty(nextPotentialVIP) || !string.IsNullOrEmpty(lastUserIdVIPGranted)))
        {
            scoreText += $"<sprite=0>{currentMaxScoreUsername}</color>";
        }
        else
        {
            scoreText += currentMaxScoreUsername;
        }

        maxScoreTMP.SetText(scoreText);
    }

    private void UpdateCurrentScoreUI(string username, string score)
    {
        usernameTMP.SetText(username);
        currentScoreTMP.SetText($"CURRENT: {score}");
    }

    private void UpdateNumber(int number)
    {
        numberTMP.SetText(number.ToString());
    }
    
    private void ResetGame()
    {
        activeRule = new FirstRule();
        lastUsername = "";
        currentScore = 0;
        currentNumber = 0;
        numberTMP.SetText(currentScore.ToString());
    }
}