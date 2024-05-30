using System.Collections.Generic;
using System.Linq;

namespace Clases
{
    internal class Juego
    {
        private List<Jugador> jugadores = new List<Jugador>();
        private Baraja baraja = new Baraja();
        private int numeroRondas = 1;


        public List<Jugador> Jugadores { get { return jugadores; } set { jugadores = value; } }
        public Baraja Baraja { get { return baraja; } set { baraja = value; } }

        public int NumeroRondas { get { return numeroRondas; } set { numeroRondas = value; } }


        public Juego(int numeroJugadores)
        {

            // Crear jugadores
            for (int i = 1; i <= numeroJugadores; i++)
            {
                jugadores.Add(new Jugador($"Jugador {i}"));
            }

            this.baraja.MezclarBaraja();

            // Repartir Cartas
            int cartasPorJugador = baraja.Cartas.Count / jugadores.Count;

            foreach (Jugador jugador in jugadores)
            {
                for (int i = 0; i < cartasPorJugador; i++)
                {
                    jugador.Cartas.Add(baraja.CojerUnaCarta());
                }
            }

            // Jugadores pillan primer carta
            foreach (Jugador jugador in jugadores)
            {
                jugador.CojerPrimeraCarta();
            }
        }

        public Jugador Batalla()
        {
            // Ordenar lista de manera descendiente
            List<Jugador> jugadoresMayorAMenor = jugadores
                                                    .OrderByDescending(jugador => jugador.CartaEnMano.Valor)
                                                    .ToList();

            // Pillar primer jugador de la lista (El Mas grande)
            Jugador primerJugador = jugadoresMayorAMenor.First();

            //Comprobar si hay jugadores con el mismo valor de carta
            List<Jugador> jugadoresEmpate = jugadoresMayorAMenor
                                                    .Where(jugador => jugador.CartaEnMano.Valor == primerJugador.CartaEnMano.Valor)
                                                    .ToList();

            if (jugadoresEmpate.Count > 1)
            {

                while (true)
                {

                    // JUGADORES EN EMPATE PERO CON 0 CARTAS EN EL PAQUETE
                    int numeroJugadorSinCartasEnPaquete = jugadoresEmpate.Where(jugador => jugador.Cartas.Count == 0).ToList().Count();

                    // En caso de que los jugadores no tengan cartas en sus paquetes, se elegira el ganador por el nombre
                    if (numeroJugadorSinCartasEnPaquete == jugadoresEmpate.Count)
                    {
                        Jugador ganador = jugadoresEmpate.OrderBy(jugador => jugador.NombreJugador).First();

                        JugadoresVuelvenCojenPrimerCarta(ganador);

                        return ganador;
                    }

                    // PROBLEMA
                    // JUGADOR 1 TIENE 0 CARTAS Y TIENE UN 13
                    // JUGADOR 2 TIENE 2 CARTAS Y TIENE DOS 13
                    


                    // JUGADORES EN DESEMPATE CON CARTAS EN EL PAQUETE
                    foreach (Jugador jugador in jugadoresEmpate)
                    {
                        jugador.Cartas.Add(jugador.CartaEnMano); // Guardar carta en el paquete
                        jugador.CojerPrimeraCarta();
                    }

                    // Ordenar Descendientemente por valor de la carta
                    List<Jugador> jugadoresDesempatando = jugadoresEmpate
                                                                 .OrderByDescending(jugador => jugador.CartaEnMano.Valor)
                                                                .ToList();

                    // Numero de jugadores con el valor de carta iguales
                    int numeroJugadoresEmpate = jugadoresDesempatando
                                                        .Where(jugador => jugador.CartaEnMano.Valor == jugadoresDesempatando.First().CartaEnMano.Valor)
                                                        .ToList()
                                                        .Count();


                    // Si no hay empate retornar el ganador
                    if (numeroJugadoresEmpate == 1)
                    {
                        Jugador jugadorGanador = jugadoresDesempatando.First();

                        JugadoresVuelvenCojenPrimerCarta(jugadorGanador);

                        return jugadoresDesempatando.First();
                    }
                }
            }
            else
            {
                JugadoresVuelvenCojenPrimerCarta(primerJugador);

                return primerJugador;
            }
                
        }

        public void JugadoresVuelvenCojenPrimerCarta(Jugador jugadorGanador)
        {


            Carta cartaEnMano = jugadores.Where(jugador => jugador == jugadorGanador).ToList()[0].CartaEnMano;

            // Guardar la carta en mano en el paquete
            jugadores.Where(jugador => jugador == jugadorGanador).ToList()[0].Cartas.Add(cartaEnMano);

            // Pillar la primera carta
            jugadores.Where(jugador => jugador == jugadorGanador).ToList()[0].CojerPrimeraCarta();

            // Jugadores que han perdido, pilla su primera carta
            for (int i = 0; i < jugadores.Count; i++)
            {
                if (jugadores[i] != jugadorGanador)
                {
                    bool paquete = jugadores[i].CojerPrimeraCarta();

                    if (!paquete)
                        jugadores.Remove(jugadores[i]);
                }
            }
        }
    }
}
