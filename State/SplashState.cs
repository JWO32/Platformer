using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Objects;

namespace Platformer.State
{
	public class SplashState : BaseGameState
	{
		public SplashState()
		{


		}

		public override void HandleInput()
		{
			var state = Keyboard.GetState();

			if(state.IsKeyDown(Keys.Enter))
			{
				SwitchState(new GameplayState());
			}
		}

		public override void LoadContent(ContentManager contentManager)
		{
			AddGameObject(new SplashImage(LoadTexture("splash")));


		}

		//public override void UnloadContent()
		//{
		//	if(Keyboard.GetState().IsKeyDown(Keys.Enter))
		//	{
		//		SwitchState(new GameplayState());
		//	}
		//}
	}


}

