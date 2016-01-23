using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
using CheckContracts;
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


        private void BuildGrid(GameColorViewModel[] gameColors)
        {
            GameCanvas.Children.Clear();

            var pointsGenerator = new GridGenerator(gameColors.Length, RootContainer.RenderSize.ToSize());

            var path = new Path();

            path.Data = new PathGeometry()
            {
                Figures = CreatePathFigure(pointsGenerator),
            };

            path.Stroke = new SolidColorBrush(Colors.White);
            path.Fill = new SolidColorBrush(Colors.Green);

            GameCanvas.Children.Add(path);

            GameCanvas.Width = pointsGenerator.Size.Width;
            GameCanvas.Height = pointsGenerator.Size.Height;

            GenerateGridBalls(gameColors, (Style) Resources["CellStyle"], pointsGenerator);
        }

        private PathFigureCollection CreatePathFigure(GridGenerator pointsGenerator)
        {
            var result = new PathFigureCollection();

            result.Add(GetExternalGridFigure(pointsGenerator.ExternalGrid));

            foreach (var points in pointsGenerator.InternalPathes)
            {
                result.Add(GetInteralGridFigure(points));
            }

            return result;
        }

        private static PathFigure GetInteralGridFigure(ImmutableArray<PortablePoint> points)
        {
            var figure = GetFigureFromPoints(points);

            figure.IsClosed = false;
            figure.IsFilled = false;

            return figure;
        }

        private static PathFigure GetExternalGridFigure(ImmutableArray<PortablePoint> points)
        {
            var figure = GetFigureFromPoints(points);

            figure.IsClosed = true;
            figure.IsFilled = true;

            return figure;
        }

        private static PathFigure GetFigureFromPoints(ImmutableArray<PortablePoint> points)
        {
            var figure = new PathFigure();

            figure.StartPoint = points.First().ToPoint();

            foreach (var segment in points.Skip(1).Select(p => new LineSegment() { Point = p.ToPoint() }))
            {
                figure.Segments.Add(segment);
            }
            return figure;
        }

        private void BuildTriangle(Grid gameGrid, GameColorViewModel[] gameColors)
        {
            /*enerateGridLayout(gameGrid, gameColors);

            var style = (Style)Resources["CellStyle"];

            GenerateGridBalls(gameGrid, gameColors, style);

            GenerateGridGrid(gameGrid, gameColors);*/
        }

        private void GenerateGridBalls(GameColorViewModel[] gameColors, Style style, GridGenerator gridGenerator)
        {
            var triangleSize = PositionHelper.GetMaxTriangleSize(gameColors.Length);

            var colorsCount = gameColors.Length;
            var size = gridGenerator.EllipseSize;

            for (var index = 0; index < colorsCount; index++)
            {
                var circle = new Ellipse();

                var targetModel = gameColors[index];

                circle.DataContext = targetModel;
                circle.Style = style;
                circle.Margin = new Thickness(0, 0, 0, -.75);
                circle.Width = size.Width;
                circle.Height = size.Height;

                targetModel.PropertyChanged += (_, __) => UpdateView(circle, targetModel);

                var trianglePosition = PositionHelper.GetTrianglePosition(index, triangleSize);
                var position = gridGenerator.GetCenterOfCell(trianglePosition);

                ConfigureAndAddElement(GameCanvas, circle, position);

                UpdateView(circle, targetModel);
            }
        }

        private static void ConfigureAndAddElement(Canvas gameGrid, FrameworkElement circle, PortablePoint positionOfCenter)
        {
            circle.VerticalAlignment = VerticalAlignment.Stretch;
            circle.HorizontalAlignment = HorizontalAlignment.Stretch;

            Validate.Between(positionOfCenter.X, 0, gameGrid.Width);
            Validate.Between(positionOfCenter.Y, 0, gameGrid.Height);

            Canvas.SetLeft(circle, positionOfCenter.X);
            Canvas.SetTop(circle, positionOfCenter.Y);

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

        private void RootContainer_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var viewModel = ViewModels.GameViewModel;

            BuildGrid(viewModel.GameColors);
        }
    }
}
