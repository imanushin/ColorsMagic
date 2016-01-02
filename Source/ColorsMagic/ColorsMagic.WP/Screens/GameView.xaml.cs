using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using ColorsMagic.Common.GameModel;

namespace ColorsMagic.WP.Screens
{
    public sealed partial class GameView
    {
        public GameView()
        {
            this.InitializeComponent();
        }
        
        private void GameView_OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = ViewModels.GameViewModel;

            BuildTriangle(GameGrid, viewModel.GameColors);
        }

        private void BuildTriangle(Grid gameGrid, GameColorViewModel[] gameColors)
        {
            GenerateGridLayout(gameGrid, gameColors);

            var style = (Style)Resources["CellStyle"];

            GenerateGridBalls(gameGrid, gameColors, style);

            GenerateGridGrid(gameGrid, gameColors);
        }

        private void GenerateGridGrid(Grid gameGrid, GameColorViewModel[] gameColors)
        {
            throw new NotImplementedException();
        }

        private void GenerateGridBalls(Grid gameGrid, GameColorViewModel[] gameColors, Style style)
        {
            gameGrid.Children.Clear();

            var triangleSize = PositionHelper.GetMaxTriangleSize(gameColors.Length);

            var colorsCount = gameColors.Length;

            for (var index = 0; index < colorsCount; index++)
            {
                var position = PositionHelper.GetTrianglePosition(index);

                var rowIndex = position.Row;
                var columnIndex = position.Column * 2 - 1 + (triangleSize - position.Row);
                
                var circle = new Ellipse();
                
                circle.VerticalAlignment = VerticalAlignment.Stretch;
                circle.HorizontalAlignment = HorizontalAlignment.Stretch;

                var targetModel = gameColors[index];

                circle.DataContext = targetModel;
                circle.Style = style;

                targetModel.PropertyChanged += (_, __) => UpdateView(circle, targetModel);

                Grid.SetRow(circle, rowIndex);
                Grid.SetColumn(circle, columnIndex);
                Grid.SetColumnSpan(circle, 2);

                UpdateView(circle, targetModel);

                gameGrid.Children.Add(circle);
            }
        }

        private void UpdateView(Ellipse circle, GameColorViewModel targetModel)
        {
            circle.Fill = new SolidColorBrush(targetModel.Color);
        }

        private static void GenerateGridLayout(Grid gameGrid, GameColorViewModel[] gameColors)
        {
            gameGrid.RowDefinitions.Clear();
            gameGrid.ColumnDefinitions.Clear();

            var triangleSize = PositionHelper.GetMaxTriangleSize(gameColors.Length);

            var newRows = Enumerable.Range(0, triangleSize).Select(i => CreateNewRow()).ToArray();

            foreach (var row in newRows)
            {
                gameGrid.RowDefinitions.Add(row);
            }

            var newColumns = Enumerable.Range(0, triangleSize * 2).Select(i => CreateNewColumn()).ToArray();

            foreach (var column in newColumns)
            {
                gameGrid.ColumnDefinitions.Add(column);
            }

            var width = gameGrid.ColumnDefinitions.Count * 10;
            var height = gameGrid.RowDefinitions.Count * 20;

            gameGrid.MaxWidth = width;
            gameGrid.MinWidth = width;
            gameGrid.MaxHeight = height;
            gameGrid.MinHeight = height;
        }

        private static ColumnDefinition CreateNewColumn()
        {
            return new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
        }

        private static RowDefinition CreateNewRow()
        {
            return new RowDefinition()
            {
                Height = new GridLength(1, GridUnitType.Star)
            };
        }
    }
}
