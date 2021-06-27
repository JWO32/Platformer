using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Platformer.Event;
using System;


namespace Platformer.State
{
	public class GameplayState : BaseGameState
	{
		public GameplayState()
		{


		}

		public override void HandleInput()
		{
			if(GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
			{
				NotifyEvent(Events.GAME_QUIT);
			}


		}

		public override void LoadContent(ContentManager contentManager)
		{
			throw new NotImplementedException();
		}

		public override void UnloadContent(ContentManager contentManager)
		{
			throw new NotImplementedException();
		}
	}
}