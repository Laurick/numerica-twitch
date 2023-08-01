
using TwitchChat;

public class PreviousPositiveInt : Rule
{
    
    public PreviousPositiveInt() : base()
    {
        description = "next -> the previous one";
    }

    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        return answerInfo.answer == answerInfo.current - 1;
    }
}
