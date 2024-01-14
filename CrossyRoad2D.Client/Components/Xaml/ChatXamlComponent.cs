using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace CrossyRoad2D.Client.Components.Xaml
{
    public class ChatXamlComponent : XamlComponent
    {
        public bool TextboxFocused { get; set; } = false;
        public string TextboxContent { get; private set; } = "";

        const int WIDTH = 400;
        const int HEIGHT = 400;
        public const int TEXT_BLOCK_COUNT = 8;

        private bool _textboxFocused = false;
        private IInputElement DefaultFocus { get; init; }
        private TextBox _textBox;
        private List<TextBlock> _textBlocks;

        public ChatXamlComponent() : base("chat.xaml") 
        {
            DefaultFocus = Keyboard.FocusedElement;
            Rectangle.Width = WIDTH;
            Rectangle.Height = HEIGHT;
            _textBox = (_uiElement as FrameworkElement).FindName("textbox") as TextBox;

            _textBlocks = new();
            for(int i = 0; i < TEXT_BLOCK_COUNT; i++)
            {
                _textBlocks.Add((_uiElement as FrameworkElement).FindName($"text{i}") as TextBlock);
            }

            _textBox.PreviewMouseDown += (object sender, MouseButtonEventArgs args) =>
            {
                args.Handled = true;
            };
        }

        public override void OnXamlRender(FrameworkElement frameworkElement)
        {
            if (_textBox != null && _textboxFocused)
            {
                TextboxContent = _textBox.Text;
            }

            if(_textboxFocused != TextboxFocused)
            {
                if (_textBox != null)
                {
                    if(TextboxFocused)
                    {
                        _textBox.Dispatcher.BeginInvoke(DispatcherPriority.Input,
                            new Action(delegate () {
                                _textBox.Focus();
                                Keyboard.Focus(_textBox);
                            }));
                    }
                    else
                    {
                        Keyboard.Focus(DefaultFocus);
                    }
                }

                _textboxFocused = TextboxFocused;
            }
        }

        public void ClearTextboxContent()
        {
            _textBox.Clear();
            TextboxContent = "";
        }

        public void SetChatEntry(int index, string content)
        {
            if(index < 0 || index >= TEXT_BLOCK_COUNT)
            {
                throw new ArgumentOutOfRangeException();
            }

            _textBlocks[index].Text = content;
        }
    }
}
