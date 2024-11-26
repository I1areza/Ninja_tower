using Godot;
using System.Collections.Generic;
using System.Linq;


public partial class EnemyContainer : Node
{
	
	private List<Enemy> _listOfEnemies;
    public int EnemyCount 
    {
        get { return _listOfEnemies.Count;}
    }
    
    public Enemy[] Enemies 
    {
        get { return _listOfEnemies.ToArray<Enemy>(); }
    }

	public override void _Ready()
	{
        _listOfEnemies = GetEnemyList();
    }
	private List<Enemy> GetEnemyList() 
	{
		var list = new List<Enemy>();
        var listOfAllCHildren = GetChildren().ToArray();
        foreach (var child in listOfAllCHildren)
        {
            if (child is Enemy)
            {
                list.Add(child as Enemy);
            }
        }
		return list;
    }
}
