namespace Clases
{
    public class Carta
    {
        private string symbolo;
        private int valor;
        public Carta(string symbolo, int valor)
        {
            this.symbolo = symbolo;
            this.valor = valor;
        }

        public string Symbolo { get { return symbolo; } set {  symbolo = value; } }
        public int Valor { get {  return valor; } set {  valor = value; } }

        public override string ToString() 
        {
            return $"Simbolo = {symbolo} y Valor = {valor}";
        }
    }
}
