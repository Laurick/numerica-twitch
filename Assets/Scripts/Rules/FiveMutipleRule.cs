
public class FiveMutipleRule : Rule
{
    public FiveMutipleRule() : base()
    {
        description = "Next in succeesion is multiple of 5";
    }
    
    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        int answer = (answerInfo.current+5) / 5;
        return answerInfo.answer == answer;
    }
}