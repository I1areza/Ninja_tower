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

    public int GetCurrentLevelIndex()
    {
        return _currentIndex;
    }

    public bool TryGetLevelByIndex(int index, out PackedScene levelScene)
    {
        if (index < 0 || index >= _levelScenes.Length)
        {
            levelScene = null;
            return false;
        }

        _currentIndex = index;
        levelScene = _levelScenes[index];
        return true;
    }
    public PackedScene GetCurrentLevel()
    {
        return _levelScenes[_currentIndex];
    }

    public int GetLevelCount()
    {
        return _levelScenes.Length;
    }
    
}
