using System.Collections.Generic;

namespace Clases
{
    public class Jugador
    {
        private string nombreJugador;
        private List<Carta> cartas = new List<Carta>();
        private Carta cartaEnMano;

        public Jugador() { }
        public Jugador(string nombreJugador)
        {
            this.nombreJugador = nombreJugador;
        }

        public string NombreJugador { get { return nombreJugador; } set { nombreJugador = value; } }
        public List<Carta> Cartas { get { return cartas; } set { cartas = value; } }
        public Carta CartaEnMano { get {  return cartaEnMano; } set { cartaEnMano = value; } }

        public bool CojerPrimeraCarta()
        {
            if (cartas.Count == 0)
                return false;


            cartaEnMano = cartas[0];
            cartas.RemoveAt(0);
            return true;
        }

        public override string ToString()
        {
            return $"{NombreJugador} tiene {cartaEnMano.ToString()}";
        }
    }
}
