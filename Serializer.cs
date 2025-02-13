using Godot;

public partial class Serializer : Node
{
	[Export] private GameManager gameManager;
	[Export] SceneChanger sceneChanger;
	private readonly string filePath = "user://savegame.save";
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void Save()
	{
		using var saveFile = FileAccess.Open(filePath, FileAccess.ModeFlags.Write);
		var jsonData = new Godot.Collections.Dictionary<string, Variant>();
		jsonData.Add("score", gameManager.Score);
		jsonData.Add("level", sceneChanger.CurrentSceneIndex);
		saveFile.StoreLine(Json.Stringify(jsonData));
	}
}
