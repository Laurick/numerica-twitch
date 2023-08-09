public class RothioRule : Rule
{
    public string description;

    public RothioRule() : base()
    {
        description = "go to Rothio's channel and post there";
    }
    
    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        return answerInfo.answer == answerInfo.current + 1 && "RothioTome".Equals(answerInfo.chatter.channel);
    }
}
