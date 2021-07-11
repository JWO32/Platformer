using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Platformer.Event;
using Platformer.Objects;
using System;


namespace Platformer.State
{
	public class GameplayState : BaseGameState
	{

		private const string PlayerFighter = "fighter";
		private const string BackgroundTexture = "Barren";

		public GameplayState()
		{


		}

		public override void HandleInput()
		{
			var state = Keyboard.GetState();

			if(state.IsKeyDown(Keys.Escape))
			{
				NotifyEvent(Events.GAME_QUIT);
			}
		}

		public override void LoadContent(ContentManager contentManager)
		{
			AddGameObject(new SplashImage(LoadTexture(BackgroundTexture)));
			AddGameObject(new PlayerSprite(LoadTexture(PlayerFighter)));
		}

		//public override void UnloadContent(ContentManager contentManager)
		//{
		//	throw new NotImplementedException();
		//}
	}
}