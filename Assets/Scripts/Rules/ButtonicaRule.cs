using System.Linq;

public class ButtonicaRule : Rule
{
    private static readonly string[] validStrings = { "click", "clock", "tap", "pat", "press"};

    public ButtonicaRule() : base()
    {
        description = "Did you play buttonica?";
    }
    
    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        return validStrings.Contains(answerInfo.chatter.message);
    }
    
}
