using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PuissanceQuatre.Model
{
    internal class Party
    {
        public Grid grid { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Party()
        {
            this.grid = new Grid();
            this.Player1 = new Player(1, "player1");
            this.Player2 = new Player(2, "player2");
            this.DisplayRules();
            this.Start();
        }



        public void Start()
        {

            bool win = false;
            int turn = 1;
            while (!win)
            {
                Player playerTurn = turn % 2 == 0 ? Player2 : Player1;  
                DisplayRoundDetail(turn, playerTurn);
                int position = ManageSelection();
                grid.PlaceToken(playerTurn.color, position);
                
                if (turn >= 7)
                {
                    win = grid.CheckAlignement(playerTurn.color);
                    if(win)
                    {
                        DisplayWin(playerTurn);
                        DisplayTable();
                    }
                }

                turn++;
            }
        }

        

        public int ManageSelection()
        {
            Console.WriteLine($"les choix possible sont entre 1 et {grid.WidthSize}");
            
            do
            {
                Console.Write("selectionnez votre colonne : ");
                var saisie = Console.ReadLine();
                var success = int.TryParse(saisie, out var choice);
                choice--; // l'utilisateur saisie ente 1 et <Max>, reduire de 1 pour l'utilisation des tableau.

                if (success && choice >= 0 && choice < grid.WidthSize && grid.CheckPlacementInCollonne(choice)) 
                    return choice;

                Console.WriteLine("erreur de saisie");
            } while (true);
        }

        #region gestion d'affichage d'information

        private void DisplayWin(Player player)
        {
            Console.WriteLine($"\nVictoire de {player.name}\n");
        }

        private void DisplayTable()
        {
            this.DisplayLine();

            for (int i = 0; i < grid.HeightSize; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < grid.WidthSize; j++)
                {
                    SetConsoleColor(grid.Tab[i, j]);
                    Console.Write($" {grid.Tab[i, j]} ");
                    SetConsoleColor(0);
                }
                Console.Write("|\n");
                this.DisplayLine();
            }
            Console.WriteLine();
        }

        private void SetConsoleColor(int color)
        {
            switch (color)
            {
                case 0: Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 1: Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 2: Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
        }

        private void DisplayRoundDetail(int turn, Player player)
        {
            Console.Write($"tour {turn}, tour du joueur ");
            SetConsoleColor(player.color);
            Console.Write(player.name);
            SetConsoleColor(0);
            Console.WriteLine() ;
            DisplayTable();
        }
        private void DisplayLine()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("---");
            for (int i = 0; i < grid.HeightSize; i++)
            {
                sb.Append("---");
            }
            sb.Append("---");
            Console.WriteLine(sb.ToString());
            
        }
        public void DisplayRules()
        {
            Console.WriteLine("Debut d'une nouvelle partie\n\n");

            Console.WriteLine("le jeu ce joue à deux joueurs au tour par tour.");
            Console.WriteLine("le but est d'empiler des pions de couleur dans des colonne.");
            Console.WriteLine("le premier joueur à reussir a aligner 4 de ces pions à gagnée");
            Console.WriteLine("les pions peuvent etre alignés de manière vertical, horizontal ou diagonal\n\n");

            Console.WriteLine("à chaque tours vous verrez le tableau et pourrez choisir dans quel collonne insérer le jeton");
            Console.WriteLine($"sur cette partie, il y a {grid.WidthSize} colonnes");
            Console.WriteLine($"vous devrez donc saisir dans la colonne un nombre entre 1 et {grid.WidthSize} pour jouer");
        }
        #endregion
    }
}
