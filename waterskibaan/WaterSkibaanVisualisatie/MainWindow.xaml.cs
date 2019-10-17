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
                lbl_bezoekersAantal.Content = $"{game.logger.GetTotaleBezoekers()}";
                lbl_rondesTotaal.Content = $"{game.logger.GetTotaleRondes()}";
                lbl_rood.Content = $"{game.logger.BezoekersInRood}";
                lbl_highScore.Content = $"{game.logger.GetHighScore()}";

                TekenWachtrij(game.WachtrijInstructie.queue, cnvs_WachtrijInstructie);
                TekenWachtrij(game.InstructieGroep.queue, cnvs_instructieGroep);
                TekenWachtrij(game.WachtrijStarten.queue, cnvs_wachtrijStarten);

                cnvs_water.Children.Clear();

                //cnvs_water.Children.Add(canvasMoves);
                TekenMoves();

                TekenKabel();
                vanLichtNaarDonker();

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
            Rectangle rec = new Rectangle();
            Rectangle rec2 = new Rectangle();
            Rectangle skie = new Rectangle();
            Rectangle skie2 = new Rectangle();
            rec.Fill = new SolidColorBrush(Colors.Black);
            rec.Width = 3;
            rec.Height = 10;
            rec2.Fill = new SolidColorBrush(Colors.Black);
            rec2.Width = 10;
            rec2.Height = 3;

            System.Drawing.Color kleding = l.huidigeSporter.KledingKleur;
            Color convertedKleur = Color.FromArgb(kleding.A, kleding.R, kleding.G, kleding.B);

            {
                skie.Fill = new SolidColorBrush(convertedKleur);
                skie.Height = 5;
                skie.Width = 30;
                skie.Stroke = new SolidColorBrush(Colors.Black);

                skie2.Fill = new SolidColorBrush(convertedKleur);
                skie2.Height = 5;
                skie2.Width = 30;
                skie2.Stroke = new SolidColorBrush(Colors.Black);

                circle.Fill = new SolidColorBrush(convertedKleur);
                circle.Width = 20;
                circle.Height = 20;
                circle.Stroke = new SolidColorBrush(Colors.Black);
            }

            Canvas.SetLeft(circle, x);
            Canvas.SetTop(circle, y);

            Canvas.SetLeft(skie, x);
            Canvas.SetTop(skie, y - 3);
            cnvs_water.Children.Add(skie);
            Canvas.SetLeft(skie2, x);
            Canvas.SetTop(skie2, y + 17);
            cnvs_water.Children.Add(skie2);
            cnvs_water.Children.Add(circle);


            Canvas.SetTop(rec, y - 15);
            Canvas.SetLeft(rec, x + 10);
            Canvas.SetTop(rec2, y - 7);
            Canvas.SetLeft(rec2, x + 7);


            if (l.huidigeSporter.AantalRondenNogTeGaan == 1)
            {
                cnvs_water.Children.Add(rec);
            }

            else if(l.huidigeSporter.AantalRondenNogTeGaan > 1)
            {
                cnvs_water.Children.Add(rec);
                cnvs_water.Children.Add(rec2);
            }
        }

        public void TekenMoves()
        {
            allMoves.Items.Clear();
            game.logger.UniekeMoves(game?.Waterskibaan?.kabel?.lijnen).ForEach(naam => allMoves.Items.Add(naam));
        }


        public void vanLichtNaarDonker()
        {

            cnvs_lightToDark.Children.Clear();
            int x = 0, y = 5;

            foreach (Sporter sp in game.logger.GetLichsteKleuren())
            {

                Ellipse circle = new Ellipse();
                System.Drawing.Color kleding = sp.KledingKleur;
                Color convertedKleur = Color.FromArgb(kleding.A, kleding.R, kleding.G, kleding.B);

                {
                    circle.Fill = new SolidColorBrush(convertedKleur);
                    circle.Width = 20;
                    circle.Height = 20;
                    circle.Stroke = new SolidColorBrush(Colors.Black);
                }

                Canvas.SetLeft(circle, x);
                Canvas.SetTop(circle, y);

                cnvs_lightToDark.Children.Add(circle);
                x += 30;
            }
        }



    }
}
