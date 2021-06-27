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
			
		}

		public override void LoadContent(ContentManager contentManager)
		{
			AddGameObject(new SplashImage(contentManager.Load<Texture2D>("Barren")));


		}

		public override void UnloadContent(ContentManager contentManager)
		{
			if(Keyboard.GetState().IsKeyDown(Keys.Enter))
			{
				SwitchState(new GameplayState());
			}
		}
	}


}

