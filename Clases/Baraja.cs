using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clases
{
    public class Baraja
    {
        private string tipoDeBaraja;
        private List<Carta> cartas = new List<Carta>();

        Random rnd = new Random();

        public Baraja()
        {
            List<string> simbolos = new List<string> { "Corazon", "Diamante", "Trebol", "Picas" };
            List<string> letra = new List<string> { "Jack", "Reina", "Rey" };
            this.tipoDeBaraja = "Poker";


            foreach (var simbolo in simbolos)
            {
                int contadorLetra = 0;
                for (int i = 1; i <= 13; i++)
                {
                    if (i <= 10)
                        this.cartas.Add(new Carta(simbolo, i));
                    else
                        this.cartas.Add(new Carta(letra[contadorLetra++]+" de "+simbolo, i));
                }
            }
        }

        public string TipoDeBaraja { get { return tipoDeBaraja; } set { tipoDeBaraja = value; } }
        public List<Carta> Cartas { get { return cartas; } set { cartas = value; } }

        public Carta CojerUnaCarta()
        {
            if (this.cartas.Count != 0)
            {
                Carta ultimaCarta = CojerCartaN(cartas.Count - 1);
                cartas.RemoveAt(cartas.Count -1);
                return ultimaCarta;
            }
            else
                return null;
        }

        public Carta CojerCartaN(int numero)
        {
            if (this.cartas.Count == 0)
                return null;

            Carta carta = cartas[numero];
            return carta;

        }


        public Carta CojerCartaAlAzar()
        {
            if (this.cartas.Count != 0)
            {
                int numeroAleatorio = rnd.Next(cartas.Count);
                cartas.RemoveAt(numeroAleatorio);
                return CojerCartaN(numeroAleatorio);
            }
            else
                return null;
        }

        public void MezclarBaraja()
        {
            if (this.cartas.Count != 0)
            {
                cartas = cartas.OrderBy(carta => rnd.Next()).ToList();
            }
            else
                Console.WriteLine("No hay cartas!!");
        }

        public void RepartirCartas(List<Jugador> jugadores)
        {
            int cartasPorJugador = cartas.Count / jugadores.Count;

            foreach (var jugador in jugadores)
            {
                for (int i = 0; i < cartasPorJugador; i++)
                {
                    jugador.Cartas.Add(CojerUnaCarta());
                }
            }
        }
        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            foreach (var carta in Cartas)
            {
                text.AppendLine(carta.ToString());
            }

            return text.ToString();
        }
    }
}
