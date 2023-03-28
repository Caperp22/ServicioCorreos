namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Builder
{
    public interface IComprobanteJson
    {
        void DatosGenerales();
        void CUDE();
        void Receptor();
        void Eventos();
        void Reiniciar(string Identificacion);
    }
}
