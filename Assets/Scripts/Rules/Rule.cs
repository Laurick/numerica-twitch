using TwitchChat;

public abstract class Rule
{
    public string description;

    protected Rule()
    {
   
    }
    
    public abstract bool isCorrectAnswer(AnswerInfo answerInfo);
    
}
