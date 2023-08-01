
using TwitchChat;

public class NextIntInNegative : Rule
{
    public NextIntInNegative() : base()
    {
        description = "next -> the next one but in negative";
    }
    
    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        return answerInfo.answer == -(answerInfo.current + 1);
    }
}
