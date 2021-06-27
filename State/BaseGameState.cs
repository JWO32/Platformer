using System;
using System.Collections.Generic;
using System.Linq;
using Platformer.Objects;
using Platformer.Event;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;

namespace Platformer.State
{
	public abstract class BaseGameState
	{
		private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();
		private const String FallBackTexture = "Empty";
		private ContentManager contentManager;

		public event EventHandler<Events> OnEventNotification;

		public BaseGameState()
		{


		}

		public abstract void LoadContent(ContentManager contentManager);
		public void UnloadContent()
		{
			this.contentManager.Unload();
		}
		public abstract void HandleInput();

		public event EventHandler<BaseGameState> OnStateSwitched;

		public void Initialise(ContentManager contentManager)
		{
			this.contentManager = contentManager;
		}

		protected void SwitchState(BaseGameState gameState)
		{
			OnStateSwitched?.Invoke(this, gameState);
		}

		protected void AddGameObject(BaseGameObject gameObject)
		{
			_gameObjects.Add(gameObject);
		}

		public void Render(SpriteBatch spriteBatch)
		{
			foreach(var gameObject in _gameObjects.OrderBy(a=> a.zIndex))
			{
				gameObject.Render(spriteBatch);
			}

		}

		protected void NotifyEvent(Events eventType, object argument = null)
		{
			OnEventNotification?.Invoke(this, eventType);

			foreach(var gameObject in _gameObjects)
			{
				gameObject.OnNotify(eventType);
			}
		}

		protected Texture2D LoadTexture(String textureName)
		{
			var texture = contentManager.Load<Texture2D>(textureName);

			return texture ?? contentManager.Load<Texture2D>(FallBackTexture);
		}

	}
}

