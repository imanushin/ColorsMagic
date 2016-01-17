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
using Path = Windows.UI.Xaml.Shapes.Path;

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

            BuildGrid(viewModel.GameColors);
        }

        private void BuildGrid(GameColorViewModel[] gameColors)
        {
            GameCanvas.Children.Clear();

            var pointsGenerator = new GridGenerator(gameColors.Length);

            var path = new Path();

            path.Data = new PathGeometry()
            {
                Figures = CreatePathFigure(pointsGenerator),
            };

            path.Stroke = new SolidColorBrush(Colors.White);
            path.Fill = new SolidColorBrush(Colors.Green);

            GameCanvas.Children.Add(path);

            GameCanvas.Width = pointsGenerator.Width;
            GameCanvas.Height = pointsGenerator.Height;
        }

        private PathFigureCollection CreatePathFigure(GridGenerator pointsGenerator)
        {
            var result = new PathFigureCollection();

            var figure = new PathFigure();

            var points = pointsGenerator.ExternalGrid;

            figure.StartPoint = points.First().ToPoint();

            foreach (var segment in points.Skip(1).Select(p => new LineSegment() { Point = p.ToPoint() }))
            {
                figure.Segments.Add(segment);
            }

            figure.IsClosed = true;
            figure.IsFilled = true;

            result.Add(figure);

            return result;
        }

        private void BuildTriangle(Grid gameGrid, GameColorViewModel[] gameColors)
        {
            /*enerateGridLayout(gameGrid, gameColors);

            var style = (Style)Resources["CellStyle"];

            GenerateGridBalls(gameGrid, gameColors, style);

            GenerateGridGrid(gameGrid, gameColors);*/
        }

        private void GenerateGridGrid(Grid gameGrid, GameColorViewModel[] gameColors)
        {
            var triangleSize = PositionHelper.GetMaxTriangleSize(gameColors.Length);
            /*
            for (int row = 0; row < triangleSize; row++)
            {
                var line = new Rectangle();
                line.Height = 1;
                line.HorizontalAlignment = HorizontalAlignment.Stretch;
                line.VerticalAlignment = VerticalAlignment.Bottom;
                line.Stroke = new SolidColorBrush(Colors.White);
                line.StrokeThickness = 1;

                Grid.SetRow(line, row);
                Grid.SetColumn(line, triangleSize - row - 1);
                Grid.SetColumnSpan(line, row * 2 + 2);

                gameGrid.Children.Add(line);
            }*/
        }

        private void GenerateGridBalls(Grid gameGrid, GameColorViewModel[] gameColors, Style style)
        {
            gameGrid.Children.Clear();

            var triangleSize = PositionHelper.GetMaxTriangleSize(gameColors.Length);

            var colorsCount = gameColors.Length;

            for (var index = 0; index < colorsCount; index++)
            {
                var circle = new Ellipse();

                var targetModel = gameColors[index];

                circle.DataContext = targetModel;
                circle.Style = style;
                circle.Margin = new Thickness(0, 0, 0, -.75);

                targetModel.PropertyChanged += (_, __) => UpdateView(circle, targetModel);

                ConfigureAndAddElement(gameGrid, circle, index, triangleSize);
                ConfigureAndAddElement(gameGrid, CreateHex(), index, triangleSize);

                UpdateView(circle, targetModel);
            }
        }

        private static void ConfigureAndAddElement(Grid gameGrid, FrameworkElement circle, int index, int triangleSize)
        {
            circle.VerticalAlignment = VerticalAlignment.Stretch;
            circle.HorizontalAlignment = HorizontalAlignment.Stretch;

            var position = PositionHelper.GetTrianglePosition(index);

            var rowIndex = position.Row;
            var columnIndex = position.Column * 2 - 1 + (triangleSize - position.Row);

            Grid.SetRow(circle, rowIndex);
            Grid.SetColumn(circle, columnIndex);
            Grid.SetColumnSpan(circle, 2);

            gameGrid.Children.Add(circle);
        }

        private static FrameworkElement CreateHex()
        {
            return new Hexagon()
            {
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 0.3,
                Margin = new Thickness(0, -3, 0, -4)
            };
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
