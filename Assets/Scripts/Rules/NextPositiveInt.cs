
using TwitchChat;

public class NextPositiveInt : Rule
{
    public NextPositiveInt() : base()
    {
        description = "next -> the next one";
    }
    
    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        return answerInfo.answer == answerInfo.current + 1;
    }
}
