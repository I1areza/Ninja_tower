using System;
using Godot;
using projectIgonnafinish.Scripts.Utils;


public interface IScorable
{
    event Action<OnScoreUpdatedEventArgs> ScoreChanged;
}