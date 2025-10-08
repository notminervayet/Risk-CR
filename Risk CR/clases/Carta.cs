namespace Risk_CR
{
    public enum TipoCarta
    {
        Infanteria,
        Caballeria,
        Artilleria,
    }

    public class Carta
    {
        public string Territorio { get; set; }
        public TipoCarta Tipo { get; set; }

        public Carta(string territorio, TipoCarta tipo)
        {
            //asigna territorio y tipo
            Territorio = territorio;
            Tipo = tipo;
        }

       
    }
}
