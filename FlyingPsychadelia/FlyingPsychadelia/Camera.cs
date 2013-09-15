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

            var newX = (int) position.X - _cameraView.Width/2;
            var newY = (int) position.Y - _cameraView.Height/2;

            if (newX < 0)
                newX = 0;
            if (newY < 0)
                newY = 0;
            if ((newX + _cameraView.Width) > (_map.Width*_map.TileWidth))
                newX = (_map.Width*_map.TileWidth) - _cameraView.Width;
            if ((newY + _cameraView.Height) > (_map.Height*_map.TileHeight))
                newY = (_map.Height*_map.TileHeight) - _cameraView.Height;

            _cameraView.X = newX;
            _cameraView.Y = newY;
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
