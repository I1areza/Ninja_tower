using Godot;
using System;
[GlobalClass]
public partial class LevelsList : Resource
{
    private int _currentIndex;
    [Export] private PackedScene[] _levelScenes;

    public bool TryGetNextLevel(out PackedScene levelScene)
    {
        if (_currentIndex < _levelScenes.Length-1)
        {
            _currentIndex++;
            levelScene = _levelScenes[_currentIndex];
            return true;
        }
        levelScene = null;
        return false;
    }

    public PackedScene GetCurrentLevel()
    {
        return _levelScenes[_currentIndex];
    }
}
