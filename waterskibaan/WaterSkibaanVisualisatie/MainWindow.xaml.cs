using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using waterskibaan;
using System.Windows.Shapes;


namespace WaterSkibaanVisualisatie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private Game game = new Game();

        public MainWindow()
        {
            InitializeComponent();
            game.initialize();

            Timer timer = new Timer(1000);
            timer.Elapsed += game.secondPassed;
            timer.Elapsed += screenRefresh;
            timer.Start();
        }

        private void screenRefresh(object sender, ElapsedEventArgs eventArgs)
        {
            this.Dispatcher.Invoke(() =>
            {
                lbl_wachtrijInstructie_number.Content = $"{game.WachtrijInstructie.queue.Count()}";
                lbl_instructiegroep_aantal.Content = $"{game.InstructieGroep.queue.Count()}";
                lbl_wachtrijstarten_aantal.Content = $"{game.WachtrijStarten.queue.Count()}";
                lbl_lijnenAantalInt.Content = game.Waterskibaan.lijnvoorraad.GetAantalLijnen();

                //logger
                lbl_bezoekersAantal.Content = $"{game.logger._bezoekers.Count}";
                lbl_rondesTotaal.Content = $"{game.logger.GetTotaleRondes()}";
                lbl_rood.Content = $"{game.logger.BezoekersInRood}";
                lbl_highScore.Content = $"{game.logger.GetHighScore()}";

                TekenWachtrij(game.WachtrijInstructie.queue, cnvs_WachtrijInstructie);
                TekenWachtrij(game.InstructieGroep.queue, cnvs_instructieGroep);
                TekenWachtrij(game.WachtrijStarten.queue, cnvs_wachtrijStarten);

                cnvs_water.Children.Clear();

                //cnvs_water.Children.Add(canvasMoves);
                //TekenMoves();

                TekenKabel();

            });
        }

        public void TekenKabel()
        {

            int x = 350, y = 500;

            for (int i = 1; i <= 10; i++)
            {
                Label positie = new Label();

                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.Black;
                positie.Content = i;

                x = (i <= 5 || i == 10 ? x + 80 : x - 80);
                if (i == 5)
                {
                    x -= 80;
                    y += 80;
                }

                if (i == 10)
                {
                    y -= 80;
                    x -= 80;
                }

                Lijn lijn = GetLijnVanPositie(i - 1);
                if (lijn != null)
                    PlaatsSporter(x, (i >= 5 && i != 10 ? y + 35 : y - 30), lijn);

                Canvas.SetLeft(positie, x);
                Canvas.SetTop(positie, y);
                cnvs_water.Children.Add(positie);
            }
        }

        public void TekenWachtrij(Queue<Sporter> wachtrij, Canvas canvas)
        {
            int x = 0;
            int y = 5;

            canvas.Children.Clear();

            for (int i = 1; i < wachtrij.Count + 1; i++)
            {
                {
                    Ellipse circle = new Ellipse();
                    System.Drawing.Color kleding = wachtrij.ElementAt(i - 1).KledingKleur;
                    Color convertedKleur = Color.FromArgb(kleding.A, kleding.R, kleding.G, kleding.B);


                    {
                        circle.Fill = new SolidColorBrush(convertedKleur);
                        circle.Width = 20;
                        circle.Height = 20;
                        circle.Stroke = new SolidColorBrush(Colors.Black);
                    }

                    Canvas.SetLeft(circle, x);
                    Canvas.SetTop(circle, y);
                    canvas.Children.Add(circle);

                    if (x >= 500)
                    {
                        y += 40;
                        x = 0;
                    }
                    else x += 30;
                }
            }
        }

        private Lijn GetLijnVanPositie(int l)
        {
            foreach (Lijn lijn in game.Waterskibaan.kabel.lijnen)
            {
                if (lijn.PositieOpDeKabel == l) return lijn;
            }
            return null;
        }

        public void PlaatsSporter(int x, int y, Lijn l)
        {
            Ellipse circle = new Ellipse();
            System.Drawing.Color kleding = l.huidigeSporter.KledingKleur;
            Color convertedKleur = Color.FromArgb(kleding.A, kleding.R, kleding.G, kleding.B);

            {
                circle.Fill = new SolidColorBrush(convertedKleur);
                circle.Width = 20;
                circle.Height = 20;
                circle.Stroke = new SolidColorBrush(Colors.Black);
            }

            Canvas.SetLeft(circle, x);
            Canvas.SetTop(circle, y);
            cnvs_water.Children.Add(circle);
        }



    }
}
