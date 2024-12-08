using System;

namespace projectIgonnafinish.Scripts.Utils;

public partial class OnScoreUpdatedEventArgs:EventArgs
{
    public int Score { get; private set; }
    public float HeatbarProgress { get; private set; }
    public IScorable Sender { get; private set; }

    public OnScoreUpdatedEventArgs(int score, float heatbarProgress, IScorable sender=null)
    {
        Score = score;
        HeatbarProgress = heatbarProgress;
        Sender = sender;
    }
}