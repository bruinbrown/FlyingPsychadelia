using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingPsychadelia
{
    public class Camera
    {
        private readonly static  Camera _instance = new Camera();
        public static Camera Instance { get { return _instance; } }

        private Rectangle _cameraView;
        private Map _map;

        public Rectangle CameraView { get { return _cameraView; } }

        private Camera()
        {
        }

        public void SetCamera(Vector2 position)
        {
            CheckMapHasBeenSet();



            _cameraView.X = (int) position.X - _cameraView.Width / 2;
            _cameraView.Y = (int) position.Y - _cameraView.Height / 2;
        }

        public void MoveCamera(Vector2 dMove)
        {
            CheckMapHasBeenSet();

            //Calculate camera blocking etc here

            _cameraView.X += (int)dMove.X;
            _cameraView.Y += (int)dMove.Y;
        }

        public void SetMap(Map map, Vector2 playerStartLocation, Viewport screen)
        {
            _map = map;
            _cameraView = new Rectangle((int)playerStartLocation.X, (int)playerStartLocation.Y, screen.Width, screen.Height);
        }

        private void CheckMapHasBeenSet()
        {
            if(_map == null)
                throw new Exception("You forgot to set the map up");
        }
    }
}
