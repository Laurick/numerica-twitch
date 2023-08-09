using TwitchChat;

public class SuscriptorRule : Rule
{
    public string description;

    public SuscriptorRule() : base()
    {
        description = "suscriptors only";
    }
    
    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        return answerInfo.answer == answerInfo.current + 1 && answerInfo.chatter.HasBadge("suscriptor");
    }
}
