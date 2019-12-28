// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManejadoresDeEventos.cs" company="ATPC.co">
//   ATPC © 2012
//   Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NeuronCloud.Atpc.Co.Modelos.ManejadoresDeEventos
{
    public delegate void ParametroTextoEventHandler(object sender, string parametro);

    public delegate void ParametroTextoObjetoEventHandler(object sender, string parametro1, object parametro2);

    public delegate void ParametroTextoTextoEventHandler(object sender, string parametro1, string parametro2);
}
