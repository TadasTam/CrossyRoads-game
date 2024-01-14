using CrossyRoad2D.Client.Entities;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Commands
{
    public abstract class MoveCommand : IUndoableCommand
    {
        private double _offsetX;
        private double _offsetY;

        protected MoveCommand(double offsetX, double offsetY)
        {
            _offsetX = offsetX;
            _offsetY = offsetY;
        }

        /// <returns>was successfully executed</returns>
        public bool Execute()
        {
            bool wasSuccessful = false;

            IIterator iterator = EntityCollection.Instance.createIterator(EntityType.Player);
            while(iterator.hasMore())
            {
                PlayerEntity player = (PlayerEntity)iterator.getNext();

                if (player.IsOfCurrentClient)
                {
                    if (player.Move(_offsetX, _offsetY))
                    {
                        wasSuccessful = true;
                    }
                }
            }

            return wasSuccessful;
        }

        public void Undo()
        {
            IIterator iterator = EntityCollection.Instance.createIterator(EntityType.Player);
            while (iterator.hasMore())
            {
                PlayerEntity player = (PlayerEntity)iterator.getNext();

                if (player.IsOfCurrentClient)
                {
                    player.Move(-_offsetX, -_offsetY, ignoreInterval: true);
                }
            }
        }
    }
}
