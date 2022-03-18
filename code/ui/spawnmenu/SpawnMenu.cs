using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Sandbox.UI.Tests;
using assetmanager;

[UseTemplate]
public class SpawnMenu : Panel




{
	public Panel SpawnPanel { get; set; }
	public ToolsMenu ToolPanel { get; set; }

	public SpawnMenu()
	{
		this.BindClass( "hidden", () => !(Input.Down( InputButton.Menu ) || HasFocus) );
		CreateSpawnMenu();
	}

	private Func<Panel> Empty = () => null;

	private void CreateSpawnMenu()
	{
		var tabSplit = SpawnPanel.AddChild<TabSplit>();

		// Prop Tab Creation

		tabSplit.Register("Props").WithPanel( () =>
		{
			CategorySplit catSplit = new();

			catSplit.Register( "Sandbox" ).WithPanel( () => {
				var scrollPanel = MakeScrollPanel();

				scrollPanel.OnCreateCell = ( cell, data ) =>
				{
					string modelName = (string)data;
					var spIcon = new SpawnIcon( GetFileName( modelName ) );

					if ( Texture.Load( FileSystem.Mounted, $"/models/{modelName}_c.png", false ) is Texture tx )
						spIcon.WithIcon( tx );
					else
						spIcon.WithRenderedIcon( $"models/{modelName}" );

					spIcon.WithCallback( ( bool isLeftClick ) =>
					{
						if ( isLeftClick )
						{
							ConsoleSystem.Run( "spawn", $"models/{modelName}" );
							return;
						}

						PopupMenu menu = new( this );
						menu.AddButton( "Copy path", () =>
							Clipboard.SetText( $"models/{modelName}" ) );

						menu.AddButton( "Box Shooter", () => {
							ConsoleSystem.Run( "boxshooter_prop", $"models/{modelName}" );
							ConsoleSystem.Run( "inventory_current", "weapon_tool" );
							ConsoleSystem.Run( "tool_current", "tool_boxgun" );
						} );
					} );


					cell.AddChild(spIcon);
				};

				var files = FileSystem.Mounted.FindFile( "models", "*.vmdl_c.png", true )
					.Concat( FileSystem.Mounted.FindFile( "models", "*.vmdl_c", true ) );

				List<string> alreadyAdded = new();
				foreach ( var file in files)
				{
					if ( string.IsNullOrWhiteSpace( file ) ) continue;
					if ( file.Contains( "clothes" ) ) continue;
					if ( file.Contains( "_lod0" ) ) continue;
					if ( alreadyAdded.Contains( file ) ) continue;

					string filePath = Regex.Match( file, @"^(.*\.vmdl)" ).Groups[1].Value;
					
					if( !alreadyAdded.Contains( filePath ) )
					{
						scrollPanel.AddItem( filePath );
						alreadyAdded.Add( filePath );
					}
				}

				return scrollPanel;
			} ).SetActive();

			foreach ( var kvp in Assets.RegisteredModels() ) 
			{
				catSplit.Register( kvp.Key ).WithPanel( () =>
				{
					var scrollPanel = MakeScrollPanel();

					scrollPanel.OnCreateCell = ( cell, data ) =>
					{
						string modelName = (string)data;
						var spIcon = new SpawnIcon( GetFileName( modelName ) );

						spIcon.WithCallback( (bool isLeftClick) => 
								ConsoleSystem.Run( "SpawnModel", modelName ) );

						cell.AddChild( spIcon );
					};

					foreach ( string path in kvp.Value )
						scrollPanel.AddItem( path );

					return scrollPanel;
				} );
			}


			return catSplit;
		} ).SetActive();

		// Entity Tab

		tabSplit.Register("Entities").WithPanel(() =>
		{
			CategorySplit catSplit = new();

			catSplit.Register("Entites").WithPanel(() => {
				var scrollPanel = MakeScrollPanel();

				scrollPanel.OnCreateCell = (cell, data) =>
				{
					var entry = (LibraryAttribute)data;
					var spIcon = new SpawnIcon(entry.Title)
						.WithIcon($"/entity/{entry.Name}.png");

					spIcon.WithCallback((isLeftClick) =>
					   ConsoleSystem.Run("spawn_entity", entry.Name));

					cell.AddChild(spIcon);
				};


				var ents = Library.GetAllAttributes<Entity>().Where(x => x.Name.StartsWith("ent_")).OrderBy(x => x.Title);
				foreach (var entry in ents) scrollPanel.AddItem(entry);
				return scrollPanel;
			}).SetActive();


			return catSplit;
		}).SetActive();

		// Weapons Tab

		tabSplit.Register("Weapons").WithPanel(() =>
		{
			CategorySplit catSplit = new();

			catSplit.Register("Pistols").WithPanel(() => {
				var scrollPanel = MakeScrollPanel();

				scrollPanel.OnCreateCell = (cell, data) =>
				{
					var entry = (LibraryAttribute)data;
					var spIcon = new SpawnIcon(entry.Title)
						.WithIcon($"/entity/{entry.Name}.png");

					spIcon.WithCallback((isLeftClick) =>
					   ConsoleSystem.Run("spawn_entity", entry.Name));

					cell.AddChild(spIcon);
				};


				var ents = Library.GetAllAttributes<Entity>().Where(x => x.Name.StartsWith("weapon_pistol")).OrderBy(x => x.Title);
				foreach (var entry in ents) scrollPanel.AddItem(entry);
				return scrollPanel;
			}).SetActive();

			catSplit.Register("Rifles").WithPanel(() => {
				var scrollPanel = MakeScrollPanel();

				scrollPanel.OnCreateCell = (cell, data) =>
				{
					var entry = (LibraryAttribute)data;
					var spIcon = new SpawnIcon(entry.Title)
						.WithIcon($"/entity/{entry.Name}.png");

					spIcon.WithCallback((isLeftClick) =>
					   ConsoleSystem.Run("spawn_entity", entry.Name));

					cell.AddChild(spIcon);
				};


				var ents = Library.GetAllAttributes<Entity>().Where(x => x.Name.StartsWith("weapon_rifle")).OrderBy(x => x.Title);
				foreach (var entry in ents) scrollPanel.AddItem(entry);
				return scrollPanel;
			}).SetActive();


			return catSplit;
		}).SetActive();

		// Vehicle Tab

		tabSplit.Register("Vehicles").WithPanel(() =>
		{
			CategorySplit catSplit = new();

			//Default
			catSplit.Register("Default").WithPanel(() => {
				var scrollPanel = MakeScrollPanel();

				scrollPanel.OnCreateCell = (cell, data) =>
				{
					var entry = (LibraryAttribute)data;
					var spIcon = new SpawnIcon(entry.Title)
						.WithIcon($"/entity/{entry.Name}.png");

					spIcon.WithCallback((isLeftClick) =>
					   ConsoleSystem.Run("spawn_entity", entry.Name));

					cell.AddChild(spIcon);
				};


				var ents = Library.GetAllAttributes<Entity>().Where(x => x.Name.StartsWith("vehicle_")).OrderBy(x => x.Title);
				foreach (var entry in ents) scrollPanel.AddItem(entry);
				return scrollPanel;
			}).SetActive();

			//Dodge
			catSplit.Register("TDM - Dodge").WithPanel(() => {
				var scrollPanel = MakeScrollPanel();

				scrollPanel.OnCreateCell = (cell, data) =>
				{
					var entry = (LibraryAttribute)data;
					var spIcon = new SpawnIcon(entry.Title)
						.WithIcon($"/entity/{entry.Name}.png");

					spIcon.WithCallback((isLeftClick) =>
					   ConsoleSystem.Run("spawn_entity", entry.Name));

					cell.AddChild(spIcon);
				};


				var ents = Library.GetAllAttributes<Entity>().Where(x => x.Name.StartsWith("vehicle_dodge")).OrderBy(x => x.Title);
				foreach (var entry in ents) scrollPanel.AddItem(entry);
				return scrollPanel;
			}).SetActive();


			return catSplit;
		}).SetActive();

		// NPC Tab

		tabSplit.Register("NPCs").WithPanel(() =>
		{
			CategorySplit catSplit = new();

			catSplit.Register("Creatures").WithPanel(() => {
				var scrollPanel = MakeScrollPanel();

				scrollPanel.OnCreateCell = (cell, data) =>
				{
					var entry = (LibraryAttribute)data;
					var spIcon = new SpawnIcon(entry.Title)
						.WithIcon($"/entity/{entry.Name}.png");

					spIcon.WithCallback((isLeftClick) =>
					   ConsoleSystem.Run("spawn_entity", entry.Name));

					cell.AddChild(spIcon);
				};


				var ents = Library.GetAllAttributes<Entity>().Where(x => x.Name.StartsWith("npc_")).OrderBy(x => x.Title);
				foreach (var entry in ents) scrollPanel.AddItem(entry);
				return scrollPanel;
			}).SetActive();


			return catSplit;
		}).SetActive();


	}

	private VirtualScrollPanel MakeScrollPanel()
	{
		VirtualScrollPanel canvas = new();
		canvas.Layout.AutoColumns = true;
		canvas.Layout.ItemHeight = 150;
		canvas.Layout.ItemWidth = 150;
		canvas.AddClass( "canvas" );
		return canvas;
	}

	private string GetFileName(string name)
	{
		Match fileMatch = Regex.Match( name, @"(\w+)\.\w+$" );
		return fileMatch.Groups[1].Value;
	}
}
