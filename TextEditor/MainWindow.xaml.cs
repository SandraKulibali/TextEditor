using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = System.Drawing.Color;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        bool flag = true;
        public MainWindow()
        {
            InitializeComponent();
            int counter = 0;
            var fonts = Fonts.SystemFontFamilies.OrderBy(x => x.Source);
            List<Brush> brushes = new List<Brush>() { Brushes.Black, Brushes.Red,Brushes.Yellow,Brushes.Blue,Brushes.Green,Brushes.Purple };
            List<string> colorNames = new List<string>() { "Black", "Red", "Yellow", "Blue", "Green", "Purple" };
            Thickness margin = new System.Windows.Thickness(10, 10, 10, 10);    
            
            foreach(Brush c in brushes)
            {
               
                DockPanel colorItems = new DockPanel();
                Canvas canvas = new Canvas();
                
                Rectangle rect = new() { Width = 10, Height = 10 ,Fill = c, Margin = margin, HorizontalAlignment = HorizontalAlignment.Left};
                TextBlock text = new TextBlock() {Text = colorNames[counter], Margin = margin, HorizontalAlignment = HorizontalAlignment.Right};
                fullColorsList.Items.Add(colorItems);
                colorItems.Children.Add(rect);
                colorItems.Children.Add(text);
                counter++;
                
                
  
            }

            int[] fontSize = { 8, 10, 12, 14, 16, 18, 20, 24, 26, 30 };

            foreach (var font in fonts)
            {
                fontStyleListMenuButton.Items.Add(new ListBoxItem { Content = font.ToString(), FontFamily = font});
                fontStyleList.Items.Add(new ListBoxItem { Content = font.ToString(), FontFamily = font });
                
            }

            foreach (var size in fontSize)
            {
                fontSizeList.Items.Add(new ListBoxItem { Content = size.ToString(), FontSize = size });
            }

        }

        private void newFileButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newFolderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void scissorsButton_Click(object sender, RoutedEventArgs e)
        {
            textArea.Cut();
        }

        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            textArea.Copy();
        }

        private void pasteButton_Click(object sender, RoutedEventArgs e)
        {
            textArea.Paste();
        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            var property = (FontWeight)textArea.Selection.GetPropertyValue(FontWeightProperty);

            if (property.Equals(FontWeights.Bold))
                textArea.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);
            else
                textArea.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);

        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
            if (textArea != null)
            {
                TextRange range = new TextRange(textArea.Selection.Start, textArea.Selection.End);
                var property = range.GetPropertyValue(Inline.TextDecorationsProperty);

                if (property.Equals(TextDecorations.Underline))
                    range.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                else
                    range.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
            var property = (FontStyle)textArea.Selection.GetPropertyValue(FontStyleProperty);

            if (property.Equals(FontStyles.Italic))
                textArea.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);
            else
                textArea.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
        }

        private void alignLeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (!textArea.Selection.IsEmpty)
                textArea.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left);
            else
                textArea.Document.TextAlignment = TextAlignment.Left;
        }

        private void alignRightButton_Click(object sender, RoutedEventArgs e)
        {
            if (!textArea.Selection.IsEmpty)
                textArea.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right);
            else
                textArea.Document.TextAlignment = TextAlignment.Right;
        }

        private void alignCenterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!textArea.Selection.IsEmpty)
                textArea.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Center);
            else
                textArea.Document.TextAlignment = TextAlignment.Center;
        }

        private void justifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!textArea.Selection.IsEmpty)
                textArea.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Justify);
            else
                textArea.Document.TextAlignment = TextAlignment.Justify;

        }

        private void cutMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textArea.Cut();
        }

        private void copyMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textArea.Copy();
        }

        private void pasteMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textArea.Paste();
        }

        private void alignLeftMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (!textArea.Selection.IsEmpty)
                textArea.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left);
            else
                textArea.Document.TextAlignment = TextAlignment.Left;
        }

        private void alignRightMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (!textArea.Selection.IsEmpty)
                textArea.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right);
            else
                textArea.Document.TextAlignment = TextAlignment.Right;
        }

        private void alignCenterMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (!textArea.Selection.IsEmpty)
                textArea.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Center);
            else
                textArea.Document.TextAlignment = TextAlignment.Center;
        }

        private void justifyMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (!textArea.Selection.IsEmpty)
                textArea.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Justify);
            else
                textArea.Document.TextAlignment = TextAlignment.Justify;
        }

        private void fontStyleSelected(object sender, RoutedEventArgs e)
        {
            var fontItems = fontSizeList?.SelectedItem;

            if (!textArea.Selection.IsEmpty && textArea is not null && fontItems is not null)
            {
                System.Windows.Controls.ListBoxItem item = fontItems as System.Windows.Controls.ListBoxItem;
                textArea.Selection.ApplyPropertyValue(FontSizeProperty, item.FontSize);
            }
            else if (textArea is not null && fontItems is not null)
            {
                System.Windows.Controls.ListBoxItem item = fontItems as System.Windows.Controls.ListBoxItem;
                textArea.Selection.ApplyPropertyValue(FontSizeProperty, item.FontSize);
            }
        }

        private void fontSizeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fontSizeItems = fontSizeList?.SelectedItem;

            if (!textArea.Selection.IsEmpty && textArea is not null && fontSizeItems is not null)
            {
                System.Windows.Controls.ListBoxItem item = fontSizeItems as System.Windows.Controls.ListBoxItem;
                textArea.Selection.ApplyPropertyValue(FontSizeProperty, item.FontSize);
            }
        }

        private void fontStyleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fontStyleItems = fontStyleList?.SelectedItem;

            if (!textArea.Selection.IsEmpty && textArea is not null && fontStyleItems is not null)
            {
                System.Windows.Controls.ListBoxItem item = fontStyleItems as System.Windows.Controls.ListBoxItem;
                textArea.Selection.ApplyPropertyValue(FontFamilyProperty, item.FontFamily);
            }
        }


        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (flag)
            //{
            //    var fontSizeItems = fontSizeList?.SelectedItem;
            //    var fontStyleItems = fontStyleList?.SelectedItem;

            //    if (!textArea.Selection.IsEmpty && textArea is not null)
            //    {
            //        if (fontSizeItems is not null || fontStyleItems is not null)
            //        {
            //            System.Windows.Controls.ListBoxItem sizeItem = fontSizeItems as System.Windows.Controls.ListBoxItem;
            //            System.Windows.Controls.ListBoxItem styleItem = fontStyleItems as System.Windows.Controls.ListBoxItem;
            //            textArea.Selection.ApplyPropertyValue(FontSizeProperty, sizeItem.FontSize);
            //            textArea.Selection.ApplyPropertyValue(FontFamilyProperty, styleItem.FontFamily);
            //        }  
            //    }
            //    else if (textArea is not null)
            //    {
            //        System.Windows.Controls.ListBoxItem item = fontSizeItems as System.Windows.Controls.ListBoxItem;
            //        textArea.Selection.ApplyPropertyValue(FontSizeProperty, item.FontSize);
            //    }
               
            //}

            flag = false;
            return;
        }

        private static IEnumerable<T> FindVisualChilds<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChilds<T>(ithChild)) yield return childOfChild;
            }
        }

        private void fullColorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
