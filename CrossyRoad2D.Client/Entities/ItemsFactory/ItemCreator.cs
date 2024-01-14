using CrossyRoad2D.Client.Entities.Items;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Common.Models;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using Apple = CrossyRoad2D.Client.Entities.Items.Apple;
using Item = CrossyRoad2D.Client.Entities.Items.Item;

namespace CrossyRoad2D.Client.Entities.ItemsFactory
{
    public class ItemCreator : Creator
    {
        private string[] itemXamlArray = { $"pack://application:,,,/Resources/apple.xaml", $"pack://application:,,,/Resources/potion.xaml", $"pack://application:,,,/Resources/coin.xaml", $"pack://application:,,,/Resources/badCoin.xaml", $"pack://application:,,,/Resources/heart.xaml" };
        private string[] itemTypeArray = { "Apple" , "Potion", "Coin", "BadCoin", "Heart" };
        private static Hashtable hashItemImage = new Hashtable();

        public override Item factorySpawnItem(string userInput, Position position)
        {
            var itemFile = hashItemImage[userInput];
            Item item = null;

            if (userInput.Equals(itemTypeArray[0]))
            {
                if (itemFile == null)
                {
                    itemFile = ReadUi(itemXamlArray[0]);
                    hashItemImage.Add(userInput, itemFile);
                }

                item = new Apple((UIElement)itemFile, position);
            }
            if (userInput.Equals(itemTypeArray[1]))
            {
                if (itemFile == null)
                {
                    itemFile = ReadUi(itemXamlArray[1]);
                    hashItemImage.Add(userInput, itemFile);
                }

                item = new Potion((UIElement)itemFile, position);
            } 
            if (userInput.Equals(itemTypeArray[2]))
            {
                if (itemFile == null)
                {
                    itemFile = ReadUi(itemXamlArray[2]);
                    hashItemImage.Add(userInput, itemFile);
                }

                item = new Coin((UIElement)itemFile, position);
            } 
            if (userInput.Equals(itemTypeArray[3]))
            {
                if (itemFile == null)
                {
                    itemFile = ReadUi(itemXamlArray[3]);
                    hashItemImage.Add(userInput, itemFile);
                }

                item = new BadCoin((UIElement)itemFile, position);
            }
            if (userInput.Equals(itemTypeArray[4]))
            {
                if (itemFile == null)
                {
                    itemFile = ReadUi(itemXamlArray[4]);
                    hashItemImage.Add(userInput, itemFile);
                }

                item = new Heart((UIElement)itemFile, position);
            }

            return item;
        }

        private UIElement ReadUi(string filepath)
        {
            var stream = Application.GetResourceStream(new Uri(filepath)).Stream;
            using StreamReader reader = new(stream);
            string xamlContent = reader.ReadToEnd();
            return XamlReader.Parse(xamlContent) as UIElement;
        }
    }
}
