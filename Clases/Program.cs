using System;

namespace Clases
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            Baraja barajas = new Baraja();
            barajas.MezclarBaraja();

            Console.Write("\nDime el numero de jugadores (2-5): ");
            int numeroJugadores = p.PedirNumero();

            Juego juego = new Juego(numeroJugadores);

            while (true)
            {
                Jugador jugadorGanadorRonda = juego.Batalla();
                Console.WriteLine($"{juego.Jugadores.Count}");

                if (juego.Jugadores.Count == 1)
                {
                    Console.WriteLine($"\nEL JUGADOR GANADOR ES {jugadorGanadorRonda.NombreJugador} CON {jugadorGanadorRonda.Cartas.Count} CARTAS!!\n");
                    Console.ReadKey();
                }
                else
                    Console.WriteLine($"Ganador de la ronda {juego.NumeroRondas++} {jugadorGanadorRonda.NombreJugador}");
              
            }

        }


        public int PedirNumero()
        {
            int numeroJugadores;
            bool numeroValido;

            do
            {
                numeroValido = int.TryParse(Console.ReadLine(), out numeroJugadores);

                if (!numeroValido)
                    Console.Write("\nIntroduce un numero!!! Introduce numero: ");
                else if (numeroJugadores > 5 || numeroJugadores < 0)
                    Console.Write("\nIntroduce un numero entre 2 - 5!! Introduce numero: ");

            } while (!numeroValido || numeroJugadores > 5 || numeroJugadores < 0);

            return numeroJugadores;

        }
    }
}
